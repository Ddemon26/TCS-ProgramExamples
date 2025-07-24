using System;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;
namespace TCS.StudioUtils {
    public class TestFoo {
        [Range(0, 100)]
        public int someValue = 42;
        public string someString = "Hello, World!";
        public bool someBool = true;
    }

    public class CustomToolWindow : PopupWindowContent {
        readonly TestFoo m_testFoo;
        const float kFrameWidth = 1f;
        public CustomToolWindow(TestFoo testFoo) => m_testFoo = testFoo;
        public override Vector2 GetWindowSize() {
            return new Vector2(200, 200);
        }

        public override void OnGUI(Rect rect) {
            if (m_testFoo == null)
                return;
            if (Event.current.type == EventType.Layout)
                return;
            Draw(rect);
            if (Event.current.type == EventType.MouseMove)
                Event.current.Use();
            if (Event.current.type != EventType.KeyDown || Event.current.keyCode != KeyCode.Escape)
                return;
            editorWindow.Close();
            GUIUtility.ExitGUI();
        }

        void Draw(Rect rect) {
            if (m_testFoo == null)
                return;
            Rect rect1 = new Rect(1f, 1f, rect.width - 2f, 18f);
            EditorGUI.LabelField(rect1, "Some Value");
            rect1.y += 18f;
            m_testFoo.someValue = EditorGUI.IntField(rect1, m_testFoo.someValue);
            rect1.y += 18f;
            EditorGUI.LabelField(rect1, "Some String");
            rect1.y += 18f;
            m_testFoo.someString = EditorGUI.TextField(rect1, m_testFoo.someString);
            rect1.y += 18f;
            EditorGUI.LabelField(rect1, "Some Bool");
            rect1.y += 18f;
            m_testFoo.someBool = EditorGUI.Toggle(rect1, m_testFoo.someBool);
            rect1.y += 18f;
        }
    }

    [EditorToolbarElement(ID, typeof(SceneView))]
    public class CustomToolbarDropdown : EditorToolbarDropdownToggle, IAccessContainerWindow {
        public const string ID = "CustomToolbarDropdown";

        SceneView sceneView => containerWindow as SceneView; //this is commented out, so it doesn't work

        public EditorWindow containerWindow { get; set; }

        TestFoo m_testFoo = new TestFoo();

        public CustomToolbarDropdown() {
            icon = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D;
            name = "SceneviewFx";
            tooltip = L10n.Tr("Toggle skybox, fog, and various other effects.");
            dropdownClicked += (Action)(() => UnityEditor.PopupWindow.Show(worldBound, (PopupWindowContent)new CustomToolWindow(m_testFoo)));
            this.RegisterValueChangedCallback<bool>((EventCallback<ChangeEvent<bool>>)(evt => OnToggleChanged(evt.newValue)));
            RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(OnAttachedToPanel));
            RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(OnDetachFromPanel));
            // SceneViewToolbarStyles.AddStyleSheets((VisualElement) this);
        }
        void OnToggleChanged(bool evtNewValue) {
            //sceneView.sceneViewState.fxEnabled = evtNewValue;
            Debug.Log($"Toggle changed: {evtNewValue}");
        }

        void OnAttachedToPanel(AttachToPanelEvent evt) {
            // Perform actions when the element is attached to the panel
            Debug.Log("Element attached to panel");
        }

        void OnDetachFromPanel(DetachFromPanelEvent evt) {
            // Perform actions when the element is detached from the panel
            Debug.Log("Element detached from panel");
        }

        void SceneViewOndrawGizmosChanged(bool enabled) {
            // UpdateProceduralRecoil the value based on the drawGizmos state
            value = enabled;
            Debug.Log($"Gizmos visibility changed: {enabled}");
        }
    }
}

/*internal static class SceneViewToolbarStyles {
    private const string k_StyleSheet = "StyleSheets/SceneViewToolbarElements/SceneViewToolbarElements.uss";
    private const string k_StyleLight = "StyleSheets/SceneViewToolbarElements/SceneViewToolbarElementsLight.uss";
    private const string k_StyleDark = "StyleSheets/SceneViewToolbarElements/SceneViewToolbarElementsDark.uss";
    private static StyleSheet s_Style;
    private static StyleSheet s_Skin;

    internal static void AddStyleSheets(VisualElement ve) {
        if ((Object)SceneViewToolbarStyles.s_Skin == (Object)null)
            SceneViewToolbarStyles.s_Skin = !EditorGUIUtility.isProSkin ? EditorGUIUtility.Load("StyleSheets/SceneViewToolbarElements/SceneViewToolbarElementsLight.uss") as StyleSheet : EditorGUIUtility.Load("StyleSheets/SceneViewToolbarElements/SceneViewToolbarElementsDark.uss") as StyleSheet;
        if ((Object)SceneViewToolbarStyles.s_Style == (Object)null)
            SceneViewToolbarStyles.s_Style = EditorGUIUtility.Load("StyleSheets/SceneViewToolbarElements/SceneViewToolbarElements.uss") as StyleSheet;
        ve.styleSheets.Add(SceneViewToolbarStyles.s_Style);
        ve.styleSheets.Add(SceneViewToolbarStyles.s_Skin);
    }
}

[EditorBrowsable(EditorBrowsableState.Never)]
internal abstract class PopupWindowBase : EditorWindow {
    private static double s_LastClosedTime;
    private static Rect s_LastActivatorRect;

    private static bool ShouldShowWindow(Rect activatorRect) {
        if (EditorApplication.timeSinceStartup - PopupWindowBase.s_LastClosedTime < 0.2 && !(activatorRect != PopupWindowBase.s_LastActivatorRect))
            return false;
        PopupWindowBase.s_LastActivatorRect = activatorRect;
        return true;
    }

    public static T Show<T>(VisualElement trigger, Vector2 size) where T : EditorWindow {
        return PopupWindowBase.Show<T>(GUIUtility.GUIToScreenRect(trigger.worldBound), size);
    }

    public static T Show<T>(Rect activatorRect, Vector2 size) where T : EditorWindow {
        T[] objectsOfTypeAll = Resources.FindObjectsOfTypeAll<T>();
        if (((IEnumerable<T>)objectsOfTypeAll).Any<T>()) {
            foreach (T obj in objectsOfTypeAll)
                obj.Close();
            return default(T);
        }

        if (!PopupWindowBase.ShouldShowWindow(activatorRect))
            return default(T);
        T instance = ScriptableObject.CreateInstance<T>();
        instance.hideFlags = HideFlags.DontSave;
        instance.ShowAsDropDown(activatorRect, size);
        return instance;
    }

    private void OnEnableINTERNAL() {
        AssemblyReloadEvents.beforeAssemblyReload += new AssemblyReloadEvents.AssemblyReloadCallback(((EditorWindow)this).Close);
    }

    private void OnDisableINTERNAL() {
        PopupWindowBase.s_LastClosedTime = EditorApplication.timeSinceStartup;
        AssemblyReloadEvents.beforeAssemblyReload -= new AssemblyReloadEvents.AssemblyReloadCallback(((EditorWindow)this).Close);
    }
}*/
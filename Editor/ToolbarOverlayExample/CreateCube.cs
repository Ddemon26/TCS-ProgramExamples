/*using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

/// <summary>
/// The CreateCube class is an editor toolbar button that allows the user to instantiate a cube in the Unity SceneView.
/// </summary>
/// <remarks>
/// This class derives from EditorToolbarButton and provides a button with a text label, icon, and tooltip. When the button is clicked,
/// it invokes the OnClick method to perform its action. This toolbar element can be added to custom toolbars in Unity's editor.
/// </remarks>
[EditorToolbarElement(id, typeof(SceneView))]
class CreateCube : EditorToolbarButton //, IAccessContainerWindow
{
    // This ID is used to populate toolbar elements.

    /// <summary>
    /// Identifier for the "Create Cube" button in the toolbar.
    /// </summary>
    /// <remarks>
    /// This ID is used by the EditorToolbarElement attribute to register the button within the SceneView toolbar.
    /// </remarks>
    public const string id = "ExampleToolbar/Button";

    // IAccessContainerWindow provides a way for toolbar elements to access the `EditorWindow` in which they exist.
    // Here we use `containerWindow` to focus the camera on our newly instantiated objects after creation.
    //public EditorWindow containerWindow { get; set; }

    // Because this is a VisualElement, it is appropriate to place initialization logic in the constructor.
    // In this method you can also register to any additional events as required. In this example there is a tooltip, an icon, and an action.

    /// <summary>
    /// Editor toolbar button that instantiates a cube in the scene when clicked.
    /// </summary>
    public CreateCube() {

        // A toolbar element can be either text, icon, or a combination of the two. Keep in mind that if a toolbar is
        // docked horizontally the text will be clipped, so usually it's a good idea to specify an icon.

        text = "Create Cube";
        icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/CreateCubeIcon.png");
        tooltip = "Instantiate a cube in the scene.";
        clicked += OnClick;
    }

    // This method will be invoked when the `Create Cube` button is clicked.

    /// <summary>
    /// Instantiates a new cube primitive in the scene when the "Create Cube" button is clicked.
    /// Also registers the creation action for undo functionality.
    /// </summary>
    void OnClick() {
        var newObj = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;

        // When writing editor tools don't forget to be a good citizen and implement Undo!

        Undo.RegisterCreatedObjectUndo(newObj.gameObject, "Create Cube");

        //if (containerWindow is SceneView view)
        //    view.FrameSelected();

    }
}*/
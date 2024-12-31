/*using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

/// <summary>
/// The DropdownToggleExample class is an implementation of a dropdown toggle in the Unity Editor toolbar.
/// It demonstrates how to create a toolbar element that can be toggled on or off, and provides a dropdown
/// menu to change settings when clicked.
/// </summary>
/// <remarks>
/// When the toggle is activated, a color rectangle is displayed in the top left of the Scene view.
/// The dropdown button allows the user to open a popup menu to change the color of the rectangle.
/// </remarks>
[EditorToolbarElement(id, typeof(SceneView))]
class DropdownToggleExample : EditorToolbarDropdownToggle, IAccessContainerWindow {
    /// <summary>
    /// Represents the unique identifier for the dropdown toggle element used in the ExampleToolbar within the SceneView.
    /// </summary>
    /// <remarks>
    /// This ID is used to register the DropdownToggleExample class as an EditorToolbarElement in Unity's SceneView
    /// toolbar, allowing it to be displayed as part of the toolbar overlay.
    /// </remarks>
    public const string id = "ExampleToolbar/DropdownToggle";

    // This property is specified by IAccessContainerWindow and is used to access the Overlay's EditorWindow.

    /// <summary>
    /// Gets or sets the EditorWindow instance associated with the overlay's container.
    /// This property is specified by the IAccessContainerWindow interface and is used to access the EditorWindow
    /// in which the overlay currently resides.
    /// </summary>
    public EditorWindow containerWindow { get; set; }
    /// <summary>
    /// Index indicating the currently selected color from the predefined colors array.
    /// The index ranges from 0 to the length of the colors array minus one, inclusive.
    /// </summary>
    static int colorIndex = 0;
    /// <summary>
    /// An array of predefined colors used to draw a color swatch in the Scene view.
    /// </summary>
    static readonly Color[] colors = { Color.red, Color.green, Color.cyan };
    /// <summary>
    /// Represents a toolbar dropdown toggle element that allows the user to display and change a color rectangle
    /// in the top left of the Scene view in Unity. This toggle can be turned on or off, and the dropdown allows
    /// selection of different colors.
    /// </summary>
    public DropdownToggleExample() {
        text = "Color Bar";
        tooltip = "Display a color rectangle in the top left of the Scene view. Toggle on or off, and open the dropdown" +
                  "to change the color.";

        // When the dropdown is opened, ShowColorMenu is invoked and we can create a popup menu.

        dropdownClicked += ShowColorMenu;

        // Subscribe to the Scene view OnGUI callback so that we can draw our color swatch.

        SceneView.duringSceneGui += DrawColorSwatch;
    }

    /// <summary>
    /// Draws a color swatch on the Scene view. The color of the swatch
    /// can be toggled through a dropdown menu.
    /// </summary>
    /// <param name="view">The SceneView in which to draw the color swatch.</param
    void DrawColorSwatch(SceneView view) {

        // Test that this callback is for the Scene View that we're interested in, and also check if the toggle is on
        // or off (value).

        if (view != containerWindow || !value) {
            return;
        }

        Handles.BeginGUI();
        GUI.color = colors[colorIndex];
        GUI.DrawTexture(new Rect(8, 8, 120, 24), Texture2D.whiteTexture);
        GUI.color = Color.white;
        Handles.EndGUI();
    }

    // When the dropdown button is clicked, this method will create a popup menu at the mouse cursor position.

    /// <summary>
    /// Displays a context menu at the mouse cursor position when the dropdown button is clicked.
    /// The menu allows the user to select a color from predefined options (Red, Green, Blue).
    /// </summary>
    void ShowColorMenu() {
        var menu = new GenericMenu();
        menu.AddItem(new GUIContent("Red"), colorIndex == 0, () => colorIndex = 0);
        menu.AddItem(new GUIContent("Green"), colorIndex == 1, () => colorIndex = 1);
        menu.AddItem(new GUIContent("Blue"), colorIndex == 2, () => colorIndex = 2);
        menu.ShowAsContext();
    }
}*/
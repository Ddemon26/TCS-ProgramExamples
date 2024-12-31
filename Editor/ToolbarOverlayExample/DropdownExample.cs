/*using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

/// <summary>
/// The DropdownExample class represents a dropdown element in a toolbar within Unity's SceneView.
/// </summary>
/// <remarks>
/// This class demonstrates the implementation of a dropdown button for a toolbar overlay in Unity's editor.
/// The button is labeled "Axis" and shows a dropdown menu when clicked.
/// </remarks>
[EditorToolbarElement(id, typeof(SceneView))]
class DropdownExample : EditorToolbarDropdown {
    /// <summary>
    /// Unique identifier for the EditorToolbarDropdown element in the ExampleToolbar.
    /// </summary>
    /// <remarks>
    /// This ID is used in Unity's EditorToolbarElement attribute to specify the location and behavior of the
    /// dropdown element within the SceneView toolbar overlay.
    /// </remarks>
    public const string id = "ExampleToolbar/Dropdown";

    /// <summary>
    /// Represents the selected choice in the dropdown menu.
    /// </summary>
    /// <remarks>
    /// This variable holds the current selection made in the dropdown menu
    /// within the SceneView toolbar. The options available are "X", "Y", and "Z".
    /// It updates based on user interaction with the dropdown items.
    /// </remarks>
    static string dropChoice = null;

    /// <summary>
    /// Represents a dropdown element in the Unity Editor Toolbar for choosing axis (X, Y, Z).
    /// </summary>
    public DropdownExample() {
        text = "Axis";
        clicked += ShowDropdown;
    }

    /// <summary>
    /// Displays a dropdown menu with options to choose an axis (X, Y, or Z).
    /// Updates the dropdown text and selected choice based on user selection.
    /// </summary>
    void ShowDropdown() {
        var menu = new GenericMenu();
        menu.AddItem
        (
            new GUIContent("X"), dropChoice == "X", () => {
                text = "X";
                dropChoice = "X";
            }
        );
        menu.AddItem
        (
            new GUIContent("Y"), dropChoice == "Y", () => {
                text = "Y";
                dropChoice = "Y";
            }
        );
        menu.AddItem
        (
            new GUIContent("Z"), dropChoice == "Z", () => {
                text = "Z";
                dropChoice = "Z";
            }
        );
        menu.ShowAsContext();
    }
}*/
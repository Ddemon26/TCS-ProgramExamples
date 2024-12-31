/*using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Represents a toggle button in the toolbar within Unity's SceneView.
/// </summary>
/// <remarks>
/// The ToggleExample class demonstrates how to create a toggle button
/// that can be added to a toolbar overlay in Unity's SceneView.
/// This example uses the EditorToolbarElement attribute to register
/// the toggle button, which responds to changes in state (on/off).
/// </remarks>
[EditorToolbarElement(id, typeof(SceneView))]
class ToggleExample : EditorToolbarToggle {
    /// <summary>
    /// Constant string identifier for the "ToggleExample" Editor toolbar element.
    /// Used to register and access the toggle button within Unity's SceneView toolbar.
    /// </summary>
    public const string id = "ExampleToolbar/Toggle";
    /// <summary>
    /// Represents a custom toolbar toggle element for the Unity Editor.
    /// </summary>
    /// <remarks>
    /// This toggle switches between "ON" and "OFF" states and updates its text label accordingly.
    /// It also logs the toggle state to the console.
    /// </remarks>
    public ToggleExample() {
        text = "Toggle OFF";
        this.RegisterValueChangedCallback(Test);
    }

    /// <summary>
    /// Handles the toggle button change event, updating the text and logging the state.
    /// </summary>
    /// <param name="evt">The change event containing the new toggle state.</param>
    void Test(ChangeEvent<bool> evt) {
        if (evt.newValue) {
            Debug.Log("ON");
            text = "Toggle ON";
        }
        else {
            Debug.Log("OFF");
            text = "Toggle OFF";
        }
    }
}*/
/*using UnityEngine;
using UnityEditor.Overlays;
using UnityEditor;

/// <summary>
/// The EditorToolbarExample class provides an example of creating a toolbar overlay in Unity's SceneView.
/// </summary>
/// <remarks>
/// This class demonstrates how to implement a toolbar overlay consisting of multiple elements: a create button, a toggle button, a dropdown button,
/// and a combination of a dropdown and toggle button. These elements are created as standalone pieces and collected to form a strip of elements in the toolbar.
/// </remarks>
[Overlay(typeof(SceneView), "ElementToolbars Example")]
// IconAttribute provides a way to define an icon for when an Overlay is in collapsed form. If not provided, the name initials are used.
[Icon("Assets/unity.png")]

// Toolbar Overlays must inherit `ToolbarOverlay` and implement a parameter-less constructor. The contents of a toolbar are populated with string IDs, which are passed to the base constructor. IDs are defined by EditorToolbarElementAttribute.
public class EditorToolbarExample : ToolbarOverlay {
    // ToolbarOverlay implements a parameterless constructor, passing the EditorToolbarElementAttribute ID.
    // This is the only code required to implement a toolbar Overlay. Unlike panel Overlays, the contents are defined
    // as standalone pieces that will be collected to form a strip of elements.

    /// <summary>
    /// The EditorToolbarExample class provides an example of creating a toolbar overlay in Unity's SceneView.
    /// </summary>
    /// <remarks>
    /// This class demonstrates how to implement a toolbar overlay consisting of multiple elements: a create button, a toggle button, a dropdown button,
    /// and a combination of a dropdown and toggle button. These elements are created as standalone pieces and collected to form a strip of elements in the toolbar.
    /// </remarks>
    EditorToolbarExample() : base
    (
        CreateCube.id,
        ToggleExample.id,
        DropdownExample.id,
        DropdownToggleExample.id
    ) { }
}*/
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(InventorySystem))]
public class InventorySystemEditor : Editor
{
    private InventorySystem targetInventorySystem;
    private int selectedItemIndex = 0; // 0 means "No Item Selected"

    // Fields for creating a new item
    private string newItemName = "New Item";
    private Sprite newItemIcon;
    private int newItemWidth = 1;
    private int newItemHeight = 1;

    private Vector2 scrollPosition;

    public override void OnInspectorGUI()
    {
        targetInventorySystem = (InventorySystem)target;
        serializedObject.Update();

        DrawInstructions();
        DrawDefaultInspector(); // Draw default fields: initialRows, initialColumns, availableItems

        if (targetInventorySystem.inventory == null)
        {
            DrawInventoryInitializationSection();
        }
        else
        {
            DrawCurrentActionInfo();
            DrawItemSelectionUI();
            DrawManageItemsUI();
            DrawResizeControls();
            DrawClearInventoryButton();
            EditorGUILayout.Space();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            DrawInventoryGrid();
            EditorGUILayout.EndScrollView();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawInstructions()
    {
        EditorGUILayout.HelpBox(
            "INSTRUCTIONS\n\n" +
            "1) Add items to 'Available Items' above or use 'Add New Item' section.\n" +
            "2) Select an item from the dropdown below.\n" +
            "   - Selecting an item: Click empty cells to place it.\n" +
            "   - Selecting 'No Item Selected': Click occupied cells to remove items.\n" +
            "3) Use 'Add Row'/'Add Column' to resize.\n" +
            "4) 'Clear Inventory' to remove all items.\n\n",
            MessageType.Info
        );
    }

    private void DrawInventoryInitializationSection()
    {
        EditorGUILayout.HelpBox("No inventory initialized. Click below to create the inventory.", MessageType.Warning);
        if (GUILayout.Button("Initialize Inventory"))
        {
            targetInventorySystem.inventory = new InventorySystem.Inventory(
                targetInventorySystem.initialRows,
                targetInventorySystem.initialColumns
            );
            EditorUtility.SetDirty(targetInventorySystem);
        }
    }

    private void DrawCurrentActionInfo()
    {
        EditorGUILayout.Space();
        if (selectedItemIndex == 0)
        {
            EditorGUILayout.HelpBox("Current Action: REMOVING items.\nClick an occupied cell to remove the item.", MessageType.None);
        }
        else
        {
            EditorGUILayout.HelpBox("Current Action: PLACING items.\nClick an empty cell to place the selected item.", MessageType.None);
        }
    }

    private void DrawItemSelectionUI()
    {
        EditorGUILayout.LabelField("Select Item to Place or Remove:", EditorStyles.boldLabel);

        List<string> itemNames = new List<string> { "No Item Selected" };
        foreach (var item in targetInventorySystem.availableItems)
        {
            itemNames.Add(item.itemName);
        }

        selectedItemIndex = EditorGUILayout.Popup("Item:", selectedItemIndex, itemNames.ToArray());

        if (selectedItemIndex > 0)
        {
            var item = targetInventorySystem.availableItems[selectedItemIndex - 1];
            EditorGUILayout.LabelField("Selected Item Info:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Name: " + item.itemName);
            EditorGUILayout.LabelField("Dimensions: " + item.width + "x" + item.height);
            if (item.itemIcon != null)
                EditorGUILayout.ObjectField("Icon:", item.itemIcon, typeof(Sprite), false);

            // Option to remove this selected item from the list
            EditorGUILayout.Space();
            if (GUILayout.Button("Remove Selected Item from List"))
            {
                Undo.RecordObject(targetInventorySystem, "Remove Item");
                targetInventorySystem.availableItems.RemoveAt(selectedItemIndex - 1);
                selectedItemIndex = 0;
                EditorUtility.SetDirty(targetInventorySystem);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No item selected. You are in REMOVAL mode.", MessageType.Info);
        }
    }

    private void DrawManageItemsUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Add New Item", EditorStyles.boldLabel);
        newItemName = EditorGUILayout.TextField("Name", newItemName);
        newItemIcon = (Sprite)EditorGUILayout.ObjectField("Icon", newItemIcon, typeof(Sprite), false);
        newItemWidth = Mathf.Max(1, EditorGUILayout.IntField("Width", newItemWidth));
        newItemHeight = Mathf.Max(1, EditorGUILayout.IntField("Height", newItemHeight));

        if (GUILayout.Button("Add New Item to Available List"))
        {
            Undo.RecordObject(targetInventorySystem, "Add New Item");
            InventorySystem.Item newItem = new InventorySystem.Item()
            {
                itemName = newItemName,
                itemIcon = newItemIcon,
                width = newItemWidth,
                height = newItemHeight
            };
            targetInventorySystem.availableItems.Add(newItem);
            EditorUtility.SetDirty(targetInventorySystem);
        }
    }

    private void DrawResizeControls()
    {
        if (targetInventorySystem.inventory == null) return;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Inventory Size Management", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Current Size:", targetInventorySystem.inventory.Rows + " rows x " + targetInventorySystem.inventory.Columns + " columns");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Row"))
        {
            Undo.RecordObject(targetInventorySystem, "Add Row");
            targetInventorySystem.inventory.Resize(targetInventorySystem.inventory.Rows + 1, targetInventorySystem.inventory.Columns);
            EditorUtility.SetDirty(targetInventorySystem);
        }
        if (GUILayout.Button("Add Column"))
        {
            Undo.RecordObject(targetInventorySystem, "Add Column");
            targetInventorySystem.inventory.Resize(targetInventorySystem.inventory.Rows, targetInventorySystem.inventory.Columns + 1);
            EditorUtility.SetDirty(targetInventorySystem);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawClearInventoryButton()
    {
        if (targetInventorySystem.inventory == null) return;

        EditorGUILayout.Space();
        if (GUILayout.Button("Clear Inventory"))
        {
            Undo.RecordObject(targetInventorySystem, "Clear Inventory");
            targetInventorySystem.inventory.ClearAll();
            EditorUtility.SetDirty(targetInventorySystem);
        }
    }

    private void DrawInventoryGrid()
    {
        if (targetInventorySystem.inventory == null) return;

        EditorGUILayout.LabelField("Inventory Grid:", EditorStyles.boldLabel);
        for (int y = 0; y < targetInventorySystem.inventory.Rows; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < targetInventorySystem.inventory.Columns; x++)
            {
                var item = targetInventorySystem.inventory.GetItemAt(x, y);
                GUIContent content;

                if (item != null && item.itemIcon != null)
                {
                    content = new GUIContent(item.itemIcon.texture, item.itemName);
                }
                else
                {
                    string label = item == null ? "Empty" : item.itemName;
                    content = new GUIContent(label);
                }

                if (GUILayout.Button(content, GUILayout.Width(60), GUILayout.Height(60)))
                {
                    OnGridCellClicked(x, y);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void OnGridCellClicked(int x, int y)
    {
        var inv = targetInventorySystem.inventory;
        var cellItem = inv.GetItemAt(x, y);

        Undo.RecordObject(targetInventorySystem, "Modify Inventory");

        if (selectedItemIndex == 0)
        {
            // Removal mode
            if (cellItem != null)
            {
                inv.RemoveItem(cellItem);
            }
        }
        else
        {
            // Placement mode
            if (cellItem == null)
            {
                var newItem = targetInventorySystem.availableItems[selectedItemIndex - 1];
                bool placed = inv.PlaceItem(newItem, x, y);
                if (!placed)
                {
                    EditorUtility.DisplayDialog(
                        "Placement Failed",
                        "Cannot place the item here.\n" +
                        "Possible reasons:\n" +
                        "- Not enough space for the item's dimensions.\n" +
                        "- Slots are occupied by another item.\n" +
                        "Try a different cell or resize the inventory.",
                        "OK"
                    );
                }
            }
            else
            {
                EditorUtility.DisplayDialog(
                    "Slot Occupied",
                    "This slot is already occupied. Remove the current item first by selecting 'No Item Selected' to enter removal mode.",
                    "OK"
                );
            }
        }

        EditorUtility.SetDirty(targetInventorySystem);
        Repaint();
    }
}
#endif
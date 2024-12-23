using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    // ----------------------------------
    // Item Class
    // ----------------------------------
    [System.Serializable]
    public class Item
    {
        public string itemName = "New Item";
        public Sprite itemIcon;
        public int width = 1;   // horizontal slots this item covers
        public int height = 1;  // vertical slots this item covers

        public override string ToString()
        {
            return string.Format("{0} ({1}x{2})", itemName, width, height);
        }
    }

    // ----------------------------------
    // Inventory Class
    // ----------------------------------
    [System.Serializable]
    public class Inventory
    {
        [SerializeField] private int rows;
        [SerializeField] private int columns;
        [SerializeField] private Item[,] inventoryGrid;

        public int Rows { get { return rows; } }
        public int Columns { get { return columns; } }

        public Inventory(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            inventoryGrid = new Item[rows, columns];
        }

        public void Resize(int newRows, int newColumns)
        {
            Item[,] newGrid = new Item[newRows, newColumns];
            int copyRows = Mathf.Min(newRows, rows);
            int copyCols = Mathf.Min(newColumns, columns);

            // Copy old data into new grid
            for (int y = 0; y < copyRows; y++)
            {
                for (int x = 0; x < copyCols; x++)
                {
                    newGrid[y, x] = inventoryGrid[y, x];
                }
            }

            rows = newRows;
            columns = newColumns;
            inventoryGrid = newGrid;
        }

        public bool CanPlaceItem(Item item, int startX, int startY)
        {
            if (startX + item.width > columns || startY + item.height > rows)
                return false;

            for (int x = 0; x < item.width; x++)
            {
                for (int y = 0; y < item.height; y++)
                {
                    if (inventoryGrid[startY + y, startX + x] != null)
                        return false; // Slot already occupied
                }
            }
            return true;
        }

        public bool PlaceItem(Item item, int startX, int startY)
        {
            if (!CanPlaceItem(item, startX, startY))
                return false;

            for (int x = 0; x < item.width; x++)
            {
                for (int y = 0; y < item.height; y++)
                {
                    inventoryGrid[startY + y, startX + x] = item;
                }
            }
            return true;
        }

        public void RemoveItem(Item item)
        {
            if (item == null) return;
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (inventoryGrid[y, x] == item)
                        inventoryGrid[y, x] = null;
                }
            }
        }

        public Item GetItemAt(int x, int y)
        {
            if (x < 0 || x >= columns || y < 0 || y >= rows)
                return null;
            return inventoryGrid[y, x];
        }

        public void ClearAll()
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    inventoryGrid[y, x] = null;
                }
            }
        }
    }

    [Header("Inventory Settings")]
    public int initialRows = 5;
    public int initialColumns = 5;

    [Tooltip("List of available items you can place in the inventory. Configure their sizes and icons here.")]
    public List<Item> availableItems = new List<Item>();

    [HideInInspector] public Inventory inventory;

    void Awake()
    {
        if (inventory == null)
            inventory = new Inventory(initialRows, initialColumns);
    }
}
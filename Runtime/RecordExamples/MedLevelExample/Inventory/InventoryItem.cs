namespace TCS {
    public record InventoryItem {
        public string ItemName { get; init; }
        public int Quantity { get; init; }
        public bool IsEquipped { get; init; }

        // Method to update item quantity
        public InventoryItem AddQuantity(int amount) {
            return this with { Quantity = this.Quantity + amount };
        }

        // Method to equip the item
        public InventoryItem Equip() {
            return this with { IsEquipped = true };
        }

        // Method to unequip the item
        public InventoryItem Unequip() {
            return this with { IsEquipped = false };
        }
    }
}
namespace TCS {
    public record InventoryItem {
        public string ItemName { get; init; }
        public string Description { get; init; }
        public int Quantity { get; init; }

        public InventoryItem Add(int count) {
            return this with { Quantity = this.Quantity + count };
        }
    }
}
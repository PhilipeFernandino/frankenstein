namespace Systems.Inventory_System
{

    public interface IInventoryItem
    {
        public InventorySlot CurrentSlot { get; }

        public InventoryItemData ItemData { get; }

        public void SetupInventorySlot(InventorySlot inventorySlot);

        public int Stack { get; set; }
    }

}
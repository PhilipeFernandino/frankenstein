using Coimbra.Services;

namespace Systems.Inventory_System
{
    [DynamicService]
    public interface IInventoryManagerService : IService
    {
        public void CreateWithDrag(ItemData itemData, in int stack);
        public void AddItemsToInventory(ItemData itemData, in int stack, out int totalAmountStacked);
        public void AddToSlot(InventorySlot slot, ItemData itemData, in int stack, out int amountAdded);

        public bool IsStorageUIActive { get; }
    }
}
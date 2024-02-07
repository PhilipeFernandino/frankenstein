namespace Systems.Inventory_System
{
    public readonly partial struct HotbarChanged : Coimbra.Services.Events.IEvent
    {
        public readonly InventorySlot InventorySlot;

        public HotbarChanged(InventorySlot inventorySlot)
        {
            InventorySlot = inventorySlot;
        }
    }
}
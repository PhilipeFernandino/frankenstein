using System.Collections.Generic;

namespace Systems.Inventory_System
{
    public readonly partial struct HotbarChanged : Coimbra.Services.Events.IEvent
    {
        public readonly HashSet<InventorySlot> DirtySlots;

        public HotbarChanged(HashSet<InventorySlot> inventorySlot)
        {
            DirtySlots = inventorySlot;
        }
    }
}
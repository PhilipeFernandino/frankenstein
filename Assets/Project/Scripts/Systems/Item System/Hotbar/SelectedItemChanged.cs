using Coimbra.Services.Events;

namespace Systems.Item_System
{
    public readonly partial struct SelectedItemChanged : IEvent
    {
        public readonly InventoryItem Item;

        public SelectedItemChanged(InventoryItem item)
        {
            Item = item;
        }
    }
}
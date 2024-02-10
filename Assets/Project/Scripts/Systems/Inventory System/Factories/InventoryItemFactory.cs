using Coimbra;
using Coimbra.Services;
using System.Collections;
using UnityEngine;

namespace Systems.Inventory_System
{
    public class InventoryItemFactory : Actor, IInventoryItemFactoryService
    {
        [SerializeField] private InventoryItem _prefab;

        public InventoryItem Create(ItemData itemData, Transform parentOnDrag)
        {
            var inventoryItem = Instantiate(_prefab);
            inventoryItem.Setup(itemData, parentOnDrag);
            return inventoryItem;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            ServiceLocator.Set<IInventoryItemFactoryService>(this);
        }
    }

    [DynamicService]
    public interface IInventoryItemFactoryService : IService
    {
        public InventoryItem Create(ItemData itemData, Transform parentOnDrag);
    }
}


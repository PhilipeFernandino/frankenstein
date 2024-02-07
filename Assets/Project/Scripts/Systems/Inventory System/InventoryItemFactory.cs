using Coimbra;
using Coimbra.Services;
using System.Collections;
using UnityEngine;

namespace Systems.Inventory_System
{
    public class InventoryItemFactory : Actor, IInventoryItemFactoryService
    {
        [SerializeField] private InventoryItem _prefab;

        public InventoryItem Create(InventoryItemData itemData, Transform parentOnDrag)
        {
            var inventoryItem = Instantiate(_prefab);
            inventoryItem.Setup(itemData, parentOnDrag);
            return inventoryItem;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Debug.Log("On Initialize");
            ServiceLocator.Set<IInventoryItemFactoryService>(this);
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            Debug.Log("On Spawn");
        }
    }

    [DynamicService]
    public interface IInventoryItemFactoryService : IService
    {
        public InventoryItem Create(InventoryItemData itemData, Transform parentOnDrag);
    }
}


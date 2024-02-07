using NaughtyAttributes;
using UnityEngine;

namespace Systems.Inventory_System.Test
{
    public class InventoryManagerTest : MonoBehaviour
    {
        [SerializeField] private InventoryItemData _itemData;
        [SerializeField] private int _stackToAdd;

        [Button]
        private void AddItems()
        {
            if (Application.isPlaying)
            {
                var inventoryManager = FindObjectOfType<InventoryManager>();
                inventoryManager.AddItemsToInventory(_itemData, _stackToAdd, out int _);
            }
        }
    }
}
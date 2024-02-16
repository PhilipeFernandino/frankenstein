using NaughtyAttributes;
using UnityEngine;

namespace Systems.Item_System.Test
{
    public class InventoryManagerTest : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
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
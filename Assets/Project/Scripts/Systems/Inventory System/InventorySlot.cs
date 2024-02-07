using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace Systems.Inventory_System
{
    [RequireComponent(typeof(Image))]
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField, ReadOnly] private Image _image;

        private InventoryItem _item;
        private IInventoryItemFactoryService _inventoryItemFactory;

        public bool HasItem => _item != null;
        public InventoryItem CurrentItem => _item;

        public int TryAddOrStackItem(InventoryItemData itemData, in int stack, Transform parent = null)
        {
            int totalAmountAdded = 0;

            if (_item != null)
            {
                if (MatchItemData(itemData))
                {
                    totalAmountAdded = Mathf.Clamp(stack, 0, CurrentItem.StackCurrentCapacity);
                    _item.Stack += totalAmountAdded;
                }
            }
            else
            {
                var item = _inventoryItemFactory.Create(itemData, parent);
                totalAmountAdded = Mathf.Clamp(stack, 0, item.StackCurrentCapacity);
                item.Stack = totalAmountAdded;
                item.SetupInventorySlot(this);
                _item = item;
            }
            
            return totalAmountAdded;
        }

        public void SetInventoryItem(InventoryItem inventoryItem)
        {
            _item = inventoryItem;
        }

        public void Empty()
        {
            _item = null;
        }

        public bool MatchItemData(InventoryItemData itemData)
        {
            return _item != null && _item.ItemData == itemData;
        }
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            _inventoryItemFactory = ServiceLocatorUtilities.GetServiceAssert<IInventoryItemFactoryService>();
        }
    }
}
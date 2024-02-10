using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;
using static UnityEditor.Progress;

namespace Systems.Inventory_System
{
    [RequireComponent(typeof(Image))]
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField, ReadOnly] private Image _image;

        private InventoryItem _item;
        private IInventoryItemFactoryService _inventoryItemFactory;

        public bool HasItem => _item != null;
        public InventoryItem Item
        {
            get => _item;
            set
            {
                _item = value;
                ItemChanged?.Invoke(_item);
            }
        }

        public event Action<InventoryItem> ItemChanged; 

        public int TryAddOrStackItem(ItemData itemData, in int stack, Transform parent = null)
        {
            if (stack == 0)
            {
                return 0; 
            }

            int totalAmountAdded = 0;

            if (_item != null)
            {
                if (MatchItemData(itemData))
                {
                    totalAmountAdded = Mathf.Clamp(stack, 0, Item.StackCurrentCapacity);
                    _item.Stack += totalAmountAdded;
                }
            }
            else
            {
                var item = _inventoryItemFactory.Create(itemData, parent);
                totalAmountAdded = Mathf.Clamp(stack, 0, item.StackCurrentCapacity);
                item.Stack = totalAmountAdded;
                item.SetupInventorySlot(this);
                Item = item;
            }

            return totalAmountAdded;
        }

        public void SetInventoryItem(InventoryItem inventoryItem)
        {
            Item = inventoryItem;
        }

        public void Empty()
        {
            Item = null;
        }

        public bool MatchItemData(ItemData itemData)
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
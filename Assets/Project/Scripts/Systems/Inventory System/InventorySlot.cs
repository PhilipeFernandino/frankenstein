using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.Inventory_System
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        private IInventoryItem _item;

        public bool HasItem => _item != null;
        public IInventoryItem CurrentItem => _item;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out IInventoryItem newItem))
            {
                if (HasItem)
                {
                    // Switch
                    if (newItem.CurrentSlot != null)
                    {
                        _item.SetupInventorySlot(newItem.CurrentSlot);
                        newItem.SetupInventorySlot(this);
                    }
                }
                else
                {
                    newItem.SetupInventorySlot(this);
                }
            }


            Debug.Log($"{gameObject.name} - {GetType()} dropped here");
        }

        public void StackItem(InventoryItemData itemData, int stack)
        {
            if (itemData == _item.ItemData)
            {
                _item.Stack += stack;
            }
            else
            {
                Debug.LogError($"{GetType()} error stacking {itemData}, stack: {stack}, currentStack: {_item.Stack}");
            }
        }

        public void AddItem(InventoryItem item, int stack)
        {
            _item = item;
            _item.Stack = stack;
            item.SetupInventorySlot(this);
        }

        public void SetInventoryItem(IInventoryItem inventoryItem)
        {
            _item = inventoryItem;
        }

        public void Empty()
        {
            _item = null;
        }

    }
}
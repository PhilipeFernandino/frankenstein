using Coimbra.Services;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Inventory_System
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private Transform _hotbarSlotsParent;
        [SerializeField] private Transform _storageSlotsParent;

        private int _hotbarSlotsCount;

        [SerializeField, ReadOnly] private List<InventorySlot> _inventorySlots = new();

        private IInventoryItemFactoryService _inventoryItemFactory;

        public void AddItemsToInventory(InventoryItemData itemData, in int stack, out int totalAmountStacked)
        {
            totalAmountStacked = 0;
            int leftToStack = stack;

            // If it's stackable, we check for existing stacks in the inventory
            if (itemData.IsStackable)
            {
                for (int i = 0; i < _inventorySlots.Count; i++)
                {
                    var slot = _inventorySlots[i];

                    if (slot.CurrentItem != null && slot.CurrentItem.ItemData == itemData)
                    {
                        int canStack = itemData.MaxStack - slot.CurrentItem.Stack;

                        if (canStack >= leftToStack)
                        {
                            totalAmountStacked = stack;
                            slot.StackItem(itemData, leftToStack);
                            return;
                        }
                        else
                        {
                            leftToStack -= canStack;
                            totalAmountStacked += canStack;
                            slot.StackItem(itemData, canStack);
                        }
                    }
                }
            }

            // Checking for empty slots in the inventory
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                var slot = _inventorySlots[i];

                if (!slot.HasItem)
                {
                    int canStack = itemData.MaxStack;

                    if (canStack >= leftToStack)
                    {
                        totalAmountStacked = stack;
                        slot.AddItem(_inventoryItemFactory.Create(itemData, transform), leftToStack);
                        return;
                    }
                    else
                    {
                        leftToStack -= canStack;
                        totalAmountStacked += canStack;
                        slot.AddItem(_inventoryItemFactory.Create(itemData, transform), canStack);
                    }
                }
            }
        }

        private void Start()
        {
            _inventoryItemFactory = ServiceLocator.Get<IInventoryItemFactoryService>();
        }

        [Button]
        private void Reset()
        {
            _inventorySlots.Clear();

            foreach (Transform child in _hotbarSlotsParent)
            {
                _inventorySlots.Add(child.GetComponent<InventorySlot>());
            }

            _hotbarSlotsCount = _inventorySlots.Count;

            foreach (Transform child in _storageSlotsParent)
            {
                _inventorySlots.Add(child.GetComponent<InventorySlot>());
            }
        }
    }
}
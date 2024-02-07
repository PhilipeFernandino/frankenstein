using Coimbra;
using Coimbra.Services;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UI;
using static Utils.ServiceLocatorUtilities;
using UnityEngine.InputSystem;

namespace Systems.Inventory_System
{
    public class InventoryManager : Actor, IInventoryManagerService
    {
        [SerializeField] private Transform _hotbarSlotsParent;
        [SerializeField] private Transform _storageSlotsParent;
        [SerializeField] private UIDynamicCanvas _storageCanvas;

        private int _hotbarSlotsCount;

        [SerializeField, ReadOnly] private List<InventorySlot> _inventorySlots = new();

        private IInventoryItemFactoryService _inventoryItemFactory;

        public void CreateWithDrag(InventoryItemData itemData, in int stack)
        {
            var itemOnDrag = _inventoryItemFactory.Create(itemData, transform);
            itemOnDrag.Stack = stack;
            itemOnDrag.StartDrag();
        }

        public void AddToSlot(InventorySlot slot, InventoryItemData itemData, in int stack, out int totalAmountStacked)
        {
            totalAmountStacked = slot.TryAddOrStackItem(itemData, stack, transform);
        }

        public void AddItemsToInventory(InventoryItemData itemData, in int stack, out int totalAmountStacked)
        {
            totalAmountStacked = 0;
            int leftToStack = stack;

            // If it's stackable, we check for existing stacks in the inventory
            if (itemData.IsStackable)
            {
                for (int i = 0; i < _inventorySlots.Count; i++)
                {
                    leftToStack -= _inventorySlots[i].TryAddOrStackItem(itemData, leftToStack, transform);
                    if (leftToStack == 0)
                        return;
                }
            }

            // Checking for empty slots in the inventory
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                var slot = _inventorySlots[i];

                if (!slot.HasItem)
                {
                    leftToStack -= _inventorySlots[i].TryAddOrStackItem(itemData, leftToStack, transform);
                    if (leftToStack == 0)
                        return;
                }
            }
        }

        #region Input
        public void ToggleStorage()
        {
            _storageCanvas.ToggleSelf();
        }
        #endregion

        protected override void OnInitialize()
        {
            base.OnInitialize();

            GetInventorySlots();

            ServiceLocator.Set<IInventoryManagerService>(this);
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            _inventoryItemFactory = GetServiceAssert<IInventoryItemFactoryService>();
        }

        [Button]
        private void GetInventorySlots()
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

    [DynamicService]
    public interface IInventoryManagerService : IService
    {
        public void CreateWithDrag(InventoryItemData itemData, in int stack);
        public void AddItemsToInventory(InventoryItemData itemData, in int stack, out int totalAmountStacked);
        public void AddToSlot(InventorySlot slot, InventoryItemData itemData, in int stack, out int amountAdded);
    }
}
﻿using Coimbra;
using Coimbra.Services;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using static Utils.ServiceLocatorUtilities;

namespace Systems.Inventory_System
{
    public class InventoryManager : Actor, IInventoryManagerService
    {
        [SerializeField] private Transform _hotbarSlotsParent;
        [SerializeField] private Transform _storageSlotsParent;
        [SerializeField] private UIDynamicCanvas _storageCanvas;
        [SerializeField] private HotbarManager _hotbarManager;

        private int _hotbarSlotsCount;

        [SerializeField, ReadOnly] private List<InventorySlot> _inventorySlots = new();
        
        private HashSet<InventorySlot> _dirtySlots;

        private IInventoryItemFactoryService _inventoryItemFactory;

        public List<InventorySlot> InventorySlots => _inventorySlots;

        public void CreateWithDrag(InventoryItemData itemData, in int stack)
        {
            var itemOnDrag = _inventoryItemFactory.Create(itemData, transform);
            itemOnDrag.Stack = stack;
            itemOnDrag.StartDrag();
        }

        public void AddToSlot(InventorySlot slot, InventoryItemData itemData, in int stack, out int totalAmountStacked)
        {
            totalAmountStacked = slot.TryAddOrStackItem(itemData, stack, transform);

            if (totalAmountStacked > 0)
            {
                DirtyStorage(slot);
            }
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
                    AddToSlot(_inventorySlots[i], itemData, leftToStack, out int amountStacked);
                    leftToStack -= amountStacked;
                    
                    if (leftToStack == 0)
                    {
                        return;
                    }
                }
            }

            // Checking for empty slots in the inventory
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                var slot = _inventorySlots[i];

                if (!slot.HasItem)
                {
                    AddToSlot(_inventorySlots[i], itemData, leftToStack, out int amountStacked);
                    leftToStack -= amountStacked;

                    if (leftToStack == 0)
                    {
                        return;
                    }
                }
            }
        }

        private void DirtyStorage(InventorySlot dirtySlot)
        {
            Debug.Log($"{GetType()} - {dirtySlot} is dirty");
            _dirtySlots.Add(dirtySlot);
        }

        private void LateUpdate()
        {
            if (_dirtySlots.Count > 0)
            {
                new HotbarChanged(_dirtySlots).Invoke(this);
                _dirtySlots.Clear();
            }

        }

        #region Input
        public void ToggleStorage()
        {
            _storageCanvas.ToggleSelf();
            _hotbarManager.ToggleClickCapture();
        }

        #endregion

        public void SetStorageActive(bool state)
        {
            _storageCanvas.SetActiveState(state);
            _hotbarManager.SetClickCaptureActive(state);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _dirtySlots = new(_inventorySlots.Count);

            SetStorageActive(false);
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
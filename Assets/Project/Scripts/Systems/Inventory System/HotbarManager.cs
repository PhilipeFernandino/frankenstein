using Coimbra;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Inventory_System
{
    public class HotbarManager : Actor
    {
        [SerializeField] private Transform _inventorySlotsParent;

        [Header("Information")]
        [SerializeField, ReadOnly] private List<InventorySlot> _inventorySlots = new();
        [SerializeField, ReadOnly] private int _selectedHotbarIndex;

        public int SelectedHotbarIndex => _selectedHotbarIndex;

        public void HotbarNavigation(InputAction.CallbackContext context)
        {
            if (int.TryParse(context.control.name, out int value))
            {
                _selectedHotbarIndex = value;
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            GetHotbarSlots();
        }

        private void GetHotbarSlots()
        {
            _inventorySlots.Clear();
            foreach (Transform child in _inventorySlotsParent)
            {
                if (child.TryGetComponent(out InventorySlot slot))
                {
                    _inventorySlots.Add(slot);
                }
            }
        }
    }
}
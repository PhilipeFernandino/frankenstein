using Coimbra;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Systems.Inventory_System
{
    public class HotbarManager : Actor
    {
        [SerializeField] private Transform _inventorySlotsParent;
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private Image _selectedImage;

        [Header("Information")]
        [SerializeField, ReadOnly] private List<InventorySlot> _inventorySlots = new();
        [SerializeField, ReadOnly] private int _selectedHotbarIndex;

        private int _hotbarSlots = 9;

        public int SelectedHotbarIndex
        {
            get => _selectedHotbarIndex;
            set
            {
                if (value >= _hotbarSlots)
                {
                    _selectedHotbarIndex = 0;
                }
                else if (value < 0)
                {
                    _selectedHotbarIndex = _hotbarSlots - 1;
                }
                else
                {
                    _selectedHotbarIndex = value;
                }

                _selectedImage.transform.position = new Vector2(_inventorySlots[_selectedHotbarIndex].transform.position.x, _selectedImage.transform.position.y);
            }
        }

        public void HotbarNavigation(InputAction.CallbackContext context)
        {
            if (int.TryParse(context.control.name, out int value) && value >= 1 && value <= 9)
            {
                SelectedHotbarIndex = value - 1;
            }
        }

        public void HotbarNavigationWithScroll(InputAction.CallbackContext context)
        {
            float yScroll = context.ReadValue<Vector2>().y;

            if (yScroll > 0)
            {
                SelectedHotbarIndex -= 1;
            }
            else if (yScroll < 0)
            {
                SelectedHotbarIndex += 1;
            }
        }

        public void SetClickCaptureActive(bool state)
        {
            _raycaster.enabled = state;
        }

        public void ToggleClickCapture()
        {
            _raycaster.enabled = !_raycaster.enabled;
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
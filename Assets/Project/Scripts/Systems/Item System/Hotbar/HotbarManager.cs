using Coimbra;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.Item_System
{
    public class HotbarManager : Actor
    {
        [SerializeField] private Transform _inventorySlotsParent;
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private Image _selectedImage;
        [SerializeField] private TextMeshProUGUI _hotbarNumberIndicator;

        [Header("Information")]
        [SerializeField, ReadOnly] private List<InventorySlot> _inventorySlots = new();
        [SerializeField, ReadOnly] private int _selectedHotbarIndex = 0;

        private int _hotbarSlots = 9;

        public int SelectedHotbarIndex
        {
            get => _selectedHotbarIndex;
            set
            {
                if (_inventorySlots[_selectedHotbarIndex] != null)
                {
                    _inventorySlots[_selectedHotbarIndex].ItemChanged -= SlotChangedEventHandler;
                }

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

                var selectedSlot = _inventorySlots[_selectedHotbarIndex];
                selectedSlot.ItemChanged += SlotChangedEventHandler;

                EquipItem(selectedSlot.Item);
                UpdateImageIndicatorPosition();
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

        private void SlotChangedEventHandler(InventoryItem item)
        {
            EquipItem(item);
        }

        private void UpdateImageIndicatorPosition()
        {
            _selectedImage.transform.position = new Vector2(_inventorySlots[_selectedHotbarIndex].transform.position.x, _selectedImage.transform.position.y);
        }

        private async void InitializeHotbarSelection()
        {
            await UniTask.WaitForEndOfFrame(this);

            _selectedImage.gameObject.SetActive(true);
            SelectedHotbarIndex = 0;
        }

        private void GetHotbarSlots()
        {
            _inventorySlots.Clear();

            int childCount = 1;

            foreach (Transform child in _inventorySlotsParent)
            {
                if (child.TryGetComponent(out InventorySlot slot))
                {
                    _inventorySlots.Add(slot);
                    var numberIndicator = Instantiate(_hotbarNumberIndicator, slot.transform);
                    numberIndicator.text = childCount++.ToString();
                }
            }
        }

        private void EquipItem(InventoryItem item)
        {
            new SelectedItemChanged(item).Invoke(this);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            GetHotbarSlots();
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            InitializeHotbarSelection();
        }
    }
}
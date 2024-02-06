using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Systems.Inventory_System
{
    [RequireComponent(typeof(Image))]
    public class InventoryItem : MonoBehaviour, IInventoryItem, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Object References")]
        [SerializeField] private TextMeshProUGUI _stackText;

        [Header("Information")]
        [SerializeField, ReadOnly] private InventorySlot _slot;
        [SerializeField, ReadOnly] private InventoryItemData _itemData;
        [SerializeField, ReadOnly] private Image _iconImage;
        [SerializeField, ReadOnly] private Transform _parentWhenDragging;
        [SerializeField, ReadOnly] private int _stack;

        private Vector3 _originalPosition;
        private bool _settingUpInventorySlot = false;

        public InventorySlot CurrentSlot => _slot;

        public InventoryItemData ItemData
        {
            get => _itemData;
            set
            {
                _itemData = value;
                _iconImage.sprite = value.Icon;
            }
        }

        public int Stack
        {
            get => _stack;
            set
            {
                Debug.Log($"{GetType()} - {gameObject.name} - stack: {value}");
                _stack = value;
                if (value > 1)
                {
                    _stackText.text = value.ToString();
                }
                else
                {
                    _stackText.text = "";
                }
            }
        }

        private void Start()
        {
            _slot = GetComponentInParent<InventorySlot>();
            if (_slot != null)
            {
                _slot.SetInventoryItem(this);
            }
        }

        private void Reset()
        {
            _iconImage = GetComponent<Image>();
        }

        public void Setup(InventoryItemData itemData, Transform parentOnDrag)
        {
            _parentWhenDragging = parentOnDrag;
            ItemData = itemData;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _iconImage.raycastTarget = false;
            _originalPosition = transform.position;
            transform.SetParent(_parentWhenDragging, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            _iconImage.raycastTarget = true;

            if (!_settingUpInventorySlot)
            {
                transform.position = _originalPosition;
                transform.SetParent(_slot.transform, true);
            }

            _settingUpInventorySlot = false;
        }

        public void SetupInventorySlot(InventorySlot inventorySlot)
        {
            _settingUpInventorySlot = true;

            if (_slot != null && _slot.CurrentItem != null && _slot.CurrentItem == (IInventoryItem)this)
            {
                _slot.Empty();
            }

            _slot = inventorySlot;
            _slot.SetInventoryItem(this);
            transform.SetParent(_slot.transform, false);
            transform.position = _slot.transform.position;
        }
    }
}

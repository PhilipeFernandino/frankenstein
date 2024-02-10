using Coimbra;
using Coimbra.Services;
using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Utils.ServiceLocatorUtilities;

namespace Systems.Inventory_System
{
    [RequireComponent(typeof(Image))]
    public class InventoryItem : MonoBehaviour, IPointerClickHandler
    {
        [Header("Object References")]
        [SerializeField] private TextMeshProUGUI _stackText;

        [Header("Information")]
        [SerializeField, ReadOnly] private InventorySlot _slot;
        [SerializeField, ReadOnly] private ItemData _itemData;
        [SerializeField, ReadOnly] private Image _iconImage;
        [SerializeField, ReadOnly] private Transform _parentWhenDragging;
        [SerializeField, ReadOnly] private int _stack;

        private Vector3 _originalPosition;
        private bool _isDragging;

        private IInventoryItemFactoryService _inventoryItemFactoryService;
        private IInventoryManagerService _inventoryManagerService;

        public InventorySlot CurrentSlot => _slot;
        public int StackCurrentCapacity => ItemData.MaxStack - Stack;

        public ItemData ItemData
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

        public void SetupInventorySlot(InventorySlot inventorySlot)
        {
            if (_slot != null && _slot.Item != null && _slot.Item == this)
            {
                _slot.Empty();
            }

            _slot = inventorySlot;
            _slot.SetInventoryItem(this);
            transform.SetParent(_slot.transform, false);
            transform.SetAsFirstSibling();
            transform.position = _slot.transform.position;
        }

        public void Setup(ItemData itemData, Transform parentOnDrag)
        {
            _parentWhenDragging = parentOnDrag;
            ItemData = itemData;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("On pointer click");

            if (_isDragging)
            {
                PointerClickWhileDragEventHandler(eventData);
            }
            else
            {
                PointerClickNotDragEventHandler(eventData);
            }
        }

        private void PointerClickWhileDragEventHandler(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                DropItem();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (Stack > 1)
                {
                    if (TryGetInventorySlotRaycast(out InventorySlot slot))
                    {
                        int amountAdded = slot.TryAddOrStackItem(ItemData, 1, _parentWhenDragging);
                        Stack -= amountAdded;
                    }
                }
                else
                {
                    DropItem();
                }
            }
        }

        private void PointerClickNotDragEventHandler(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                StartDrag();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (Stack > 1)
                {
                    int newItemStack = Stack;
                    Stack /= 2;
                    newItemStack -= Stack;
                    _inventoryManagerService.CreateWithDrag(ItemData, newItemStack);
                }
                else
                {
                    StartDrag();
                }
            }
        }


        public void StartDrag()
        {
            Debug.Log($"{GetType()} - Start Drag");

            _isDragging = true;
            _originalPosition = transform.position;
            transform.SetParent(_parentWhenDragging, false);

            Drag();

            if (_slot != null)
            {
                _slot.Empty();
            }
        }


        public void DropItem()
        {
            Debug.Log($"{GetType()} - End Drag");

            if (TryGetInventorySlotRaycast(out InventorySlot inventorySlot))
            {
                int amountAdded = inventorySlot.TryAddOrStackItem(ItemData, Stack, _parentWhenDragging);

                if (amountAdded == Stack)
                {
                    gameObject.Dispose(false);
                }
                else
                {
                    Stack -= amountAdded;
                }
            }
        }

        private void Awake()
        {
            _iconImage = GetComponent<Image>();
        }

        private void Start()
        {
            _inventoryItemFactoryService = GetServiceAssert<IInventoryItemFactoryService>();
            _inventoryManagerService = GetServiceAssert<IInventoryManagerService>();

        }

        private void Update()
        {
            Drag();
        }

        private void Drag()
        {
            if (_isDragging)
            {
                transform.position = Input.mousePosition;
            }
        }

        private static bool TryGetInventorySlotRaycast(out InventorySlot inventorySlot)
        {
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(new(EventSystem.current) { position = Input.mousePosition }, results);
            foreach (var hits in results)
            {
                if (hits.gameObject.TryGetComponent(out inventorySlot))
                {
                    Debug.Log($"{nameof(InventoryItem)} - found {inventorySlot.name}");
                    return true;
                }
            }
            inventorySlot = null;
            return false;
        }
    }
}

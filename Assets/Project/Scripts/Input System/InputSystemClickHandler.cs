using Systems.Inventory_System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utils.UI;

namespace Assets.Project.Scripts.Input_System
{
    public class InputSystemClickHandler : MonoBehaviour
    {
        private IInventoryManagerService _inventoryManager;

        public void ClickInput(InputAction.CallbackContext context)
        {

            if (_inventoryManager.IsStorageUIActive)
            {
                var gameObject = UIRaycastUtilities.UIRaycastFirst(Input.mousePosition);
                if (gameObject.TryGetComponent(out InventoryItem item))
                {
                    //item.PointerClick(PointerEventData.InputButton.Left);
                }
            }
        }

        private void Start()
        {
            _inventoryManager = Utils.ServiceLocatorUtilities.GetServiceAssert<IInventoryManagerService>();
        }
    }
}
using System.Collections;
using Systems.Inventory_System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Inventory_System
{
    [RequireComponent(typeof(InventoryManager))]
    public class InventoryManagerInputHandler : MonoBehaviour
    {
        private InventoryManager _inventoryManager;

        public void ToggleStorageInput(InputAction.CallbackContext context)
        {
            Debug.Log(nameof(ToggleStorageInput));
            _inventoryManager.ToggleStorage();
        }

        private void Awake()
        {
            _inventoryManager = GetComponent<InventoryManager>();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Systems.Inventory_System
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundItemMagnet : MonoBehaviour
    {
        [SerializeField] private float _attractionForce;
        [SerializeField] private float _distanceToPickup;

        private HashSet<GroundItem> _onRangeItems = new();

        private IInventoryManagerService _inventoryManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out GroundItem groundItem))
            {
                _onRangeItems.Add(groundItem);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out GroundItem groundItem) && _onRangeItems.Contains(groundItem))
            {
                _onRangeItems.Remove(groundItem);
            }
        }

        private void Update()
        {
            foreach (var item in _onRangeItems.ToList())
            {
                item.transform.position += ((transform.position - item.transform.position).normalized * (Time.deltaTime * _attractionForce));

                if (Vector3.Distance(transform.position, item.transform.position) < _distanceToPickup)
                {
                    TryToPickupItem(item);
                }   
            }   
        }

        private void TryToPickupItem(GroundItem item)
        {
            _inventoryManager.AddItemsToInventory(item.ItemData, item.Stack, out int totalAmountAdded);
            if (item.Stack == totalAmountAdded)
            {
                _onRangeItems.Remove(item);
            }
            item.Stack -= totalAmountAdded;
        }

        private void Start()
        {
            _inventoryManager = ServiceLocatorUtilities.GetServiceAssert<IInventoryManagerService>();   
        }
    }
}
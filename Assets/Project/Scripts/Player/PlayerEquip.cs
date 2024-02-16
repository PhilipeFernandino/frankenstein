using Coimbra.Services.Events;
using System;
using System.Collections.Generic;
using Systems.Item_System;
using TMPro;
using UnityEngine;

namespace Player
{

    public class PlayerEquip : MonoBehaviour
    {
        [SerializeField] private Transform _equipPlacementOrigin;

        private InventoryItem _selectedItem;
        private Equipment _equipped;

        private Dictionary<Type, Equipment> _equipmentPool = new();

        public InventoryItem SelectedItem => _selectedItem;

        public void TryToUse()
        {
            Debug.Log($"{GetType()} - Trying to use");

            if (_equipped != null && _equipped.TryToUse() && _equipped.IsConsumable)
            {
                _selectedItem.Stack--;
            }
        }

        private void Awake()
        {
            SelectedItemChanged.AddListener(SelectedItemChangedEventHandler);
        }

        private void SelectedItemChangedEventHandler(ref EventContext context, in SelectedItemChanged e)
        {
            Debug.Log($"{GetType()} Selected item changed to: {e.Item}");

            _selectedItem = e.Item;

            if (_selectedItem != null && _selectedItem.ItemData.GetType() == typeof(EquippableItemData))
            {
                var equipmentClass = ((EquippableItemData)_selectedItem.ItemData).Equipment;
                var equipmentType = equipmentClass.GetType();
                _equipped = GetEquipmentFromPool(equipmentType, equipmentClass);
            }
            else
            {
                _equipped = null;
            }
        }

        private Equipment GetEquipmentFromPool(Type equipmentType, Equipment equipmentClass)
        {
            Debug.Log($"{GetType()} - Get Equipment From Pool\n" +
                $"\tType: {equipmentType}\n" +
                $"\tClass: {equipmentClass}");

            if (_equipmentPool.ContainsKey(equipmentType))
            {
                return _equipmentPool[equipmentType];
            }
            else
            {    
                var equipment = equipmentClass.New();
                _equipmentPool.Add(equipmentType, equipment);
                return equipment;
            }
        }
    }
}

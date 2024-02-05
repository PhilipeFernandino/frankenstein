using NaughtyAttributes;
using System;
using UnityEngine;

namespace Systems.Inventory_System
{
    [CreateAssetMenu(fileName = "Inventory Item", menuName = "Inventory System/Inventory Item", order = 1)]
    public class InventoryItem_SO : ScriptableObject
    {

        [field: SerializeField, EnumFlags] public InventoryItemActions Actions { get; private set; }

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int SellPrice { get; private set; }

        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject HoldablePrefab { get; private set; }

    }

    [Flags]
    public enum InventoryItemActions 
    {
        None = 0,
        Sellable = 1,
        Consumable = 2,
        Interactable = 4,
        Placeable = 8,
        Holdable = 16,
        Stackable = 32,
    }

}
using NaughtyAttributes;
using System;
using UnityEngine;

namespace Systems.Inventory_System
{
    [CreateAssetMenu(fileName = "Inventory Item", menuName = "Inventory System/Inventory Item", order = 1)]
    public class InventoryItemData : ScriptableObject
    {

        [field: SerializeField, EnumFlags] public ItemProperties Properties { get; private set; }
        [field: SerializeField] public ItemType Type { get; private set; } 
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int SellPrice { get; private set; }
        [field: SerializeField] public int MaxStack { get; private set; } = 1;

        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject HoldablePrefab { get; private set; }

        public bool IsStackable => MaxStack > 1;
        public bool IsSellable => Properties.HasFlag(ItemProperties.Sellable);
        public bool IsInteractable => Properties.HasFlag(ItemProperties.Interactable);
        public bool IsHoldable => Properties.HasFlag(ItemProperties.Holdable);

    }

    [Flags]
    public enum ItemProperties 
    {
        None = 0,
        /// <summary>
        /// If it can be selled at a store
        /// </summary>
        Sellable = 1,
        /// <summary>
        /// If it can be interacted. An consumable can be interacted by consuming it, an equippament will be actionable, like digging with a shovel
        /// </summary>
        Interactable = 2,
        /// <summary>
        /// If it can be hold and shown visually
        /// </summary>
        Holdable = 4,
    }

    public enum ItemType
    {
        None = 0,
        Consumable = 1,
        Equipable = 2,
        Placeable = 4,
    }

}
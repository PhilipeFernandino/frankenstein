using NaughtyAttributes;
using System;
using UnityEngine;

namespace Systems.Item_System
{
    [CreateAssetMenu(menuName = "Item System/Item Data")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int SellPrice { get; private set; }
        [field: SerializeField] public int MaxStack { get; private set; } = 1;

        [field: SerializeField] public Sprite Icon { get; private set; }

        public bool IsStackable => MaxStack > 1;
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
        /// If it can be used. An consumable can be used by consuming it, an equippament will be actionable, like digging with a shovel
        /// </summary>
        Usable = 2,
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
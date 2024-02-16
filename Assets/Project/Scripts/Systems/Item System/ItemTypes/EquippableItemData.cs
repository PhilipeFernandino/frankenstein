using Player;
using Systems.Item_System;
using UnityEngine;

namespace Systems.Item_System
{
    [CreateAssetMenu(menuName = "Item System/Equippable Item Data")]
    public class EquippableItemData : ItemData
    {
        [field: SerializeField] public Equipment Equipment { get; private set; }
    }
}
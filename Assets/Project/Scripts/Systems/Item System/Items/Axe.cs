using System;
using UnityEngine;

namespace Systems.Item_System
{
    public class Axe : Equipment
    {
        [SerializeField] private Transform _originTransform;
        [SerializeField] private bool _isConsumable;

        public override bool IsConsumable => _isConsumable;
        public override Vector3 Origin => _originTransform.position;

        public override event Action Finished;

        public override bool TryToUse()
        {
            Debug.Log("Used axe :)");
            return true;
        }
    }
}
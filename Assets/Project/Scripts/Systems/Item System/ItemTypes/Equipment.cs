using System;
using UnityEngine;

namespace Systems.Item_System
{
    public abstract class Equipment : MonoBehaviour
    {
        public abstract bool TryToUse();
        public virtual Equipment New()
        {
            Equipment equip = Instantiate(this);
            return equip;
        }

        public abstract Vector3 Origin { get; }
        public abstract bool IsConsumable { get; }

        public abstract event Action Finished;
    }
}

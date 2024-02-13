using Coimbra;
using Coimbra.Services;
using UnityEngine;

namespace Systems.Inventory_System
{
    public class GroundItemFactory : Actor, IGroundItemFactoryService
    {
        [SerializeField] private GroundItem _groundItemPrefab;

        public GroundItem Create(ItemData itemData, int amount)
        {
            var groundItem = Instantiate(_groundItemPrefab, Vector3.zero, Quaternion.identity);
            groundItem.Setup(itemData, amount);
            return groundItem;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            ServiceLocator.Set<IGroundItemFactoryService>(this);
        }
    }
}
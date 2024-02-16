using Coimbra.Services;

namespace Systems.Item_System
{
    [DynamicService]
    public interface IGroundItemFactoryService : IService
    {
        public GroundItem Create(ItemData itemData, int amount);
    }
}
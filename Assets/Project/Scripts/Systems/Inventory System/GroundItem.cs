using Coimbra;
using NaughtyAttributes;
using UnityEngine;

namespace Systems.Inventory_System
{
    public class GroundItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ItemData _itemData;
        [SerializeField] private int _stack;
        
        public int Stack
        {
            get => _stack;
            set
            {
                _stack = value;
                if (_stack <= 0)
                {
                    gameObject.Dispose(false);
                }
            }
        }

        public ItemData ItemData => _itemData;

        public void Setup(ItemData itemData, int stack)
        {
            _itemData = itemData;
            Stack = stack;

            transform.rotation = Quaternion.Euler(0, 90, 0);
            _spriteRenderer.sprite = _itemData.Icon;
        }
    }
}
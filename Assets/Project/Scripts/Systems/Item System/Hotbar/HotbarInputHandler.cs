using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Item_System
{
    [RequireComponent(typeof(HotbarManager))]
    public class HotbarInputHandler : MonoBehaviour
    {
        private HotbarManager _hotbarManager;

        public void HotbarNavigation(InputAction.CallbackContext context)
        {
            if (int.TryParse(context.control.name, out int value) && value >= 1 && value <= 9)
            {
                _hotbarManager.SelectedHotbarIndex = value - 1;
            }
        }

        public void HotbarNavigationWithScroll(InputAction.CallbackContext context)
        {
            float yScroll = context.ReadValue<Vector2>().y;

            if (yScroll > 0)
            {
                _hotbarManager.SelectedHotbarIndex -= 1;
            }
            else if (yScroll < 0)
            {
                _hotbarManager.SelectedHotbarIndex += 1;
            }
        }

        private void Awake()
        {
            _hotbarManager = GetComponent<HotbarManager>();
        }
    }
}
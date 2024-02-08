using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Utils.UI
{
    public static class UIRaycastUtilities
    {
        private static List<RaycastResult> results = new();

        public static bool PointerIsOverUI(Vector2 screenPos)
        {
            var hitObject = UIRaycastFirst(screenPos);
            return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI");
        }

        public static List<RaycastResult> UIRaycastAll(Vector2 screenPos)
        {
            var pointerData = ScreenPosToPointerData(screenPos);
            EventSystem.current.RaycastAll(pointerData, results);
            return results;
        }

        public static GameObject UIRaycastFirst(Vector2 screenPos)
        {
            var pointerData = ScreenPosToPointerData(screenPos);
            EventSystem.current.RaycastAll(pointerData, results);
            return results.Count > 0 ? results[0].gameObject : null;
        }

        static PointerEventData ScreenPosToPointerData(Vector2 screenPos)
           => new(EventSystem.current) { position = screenPos };
    }
}

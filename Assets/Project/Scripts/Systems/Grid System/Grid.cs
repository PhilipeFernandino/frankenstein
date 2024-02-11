using UnityEngine;

namespace Systems.Grid_System
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private int[,] _grid = new int[256, 256];
        [SerializeField] private SpriteRenderer _pointIndicator; 

        private void Update()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10f;
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            Debug.Log(pos);
            _pointIndicator.transform.position = pos;
        }
    }
}
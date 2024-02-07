using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;

        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical"); 
        
            Vector2 xy = new Vector2(x, y);

            if (xy.magnitude > 1)
            {
                xy.Normalize();
            }

            var fs = _speed * Time.deltaTime;
            var motion = (xy.y * transform.forward + xy.x * transform.right) * fs;
            _characterController.Move(motion);
        }
    }
}
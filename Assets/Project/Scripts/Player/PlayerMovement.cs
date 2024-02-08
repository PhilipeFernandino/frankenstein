using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;

        public void TryToMove(Vector2 direction)
        {
            Vector2 xy = direction;

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
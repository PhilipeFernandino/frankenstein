using Coimbra;
using System;
using UnityEngine;

namespace Player
{
    public class Player : Actor
    {
        [SerializeField] private PlayerMovement _playerMovement;

        public void TryToMove(Vector2 direction)
        {
            _playerMovement.TryToMove(direction);
        }

        public void TryToInteract()
        {

        }

        public void TryToUseCurrentItem(Vector2 position)
        {
            Debug.Log(position);
        }
    }
}
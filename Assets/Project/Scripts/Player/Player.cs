﻿using Coimbra;
using System;
using UnityEngine;

namespace Player
{
    public class Player : Actor
    {
        [SerializeField] private PlayerMovement2D _playerMovement;
        [SerializeField] private PlayerEquip _playerEquip;

        public void TryToMove(Vector2 direction)
        {
            _playerMovement.TryToMove(direction);
        }

        public void TryToInteract()
        {
        }

        public void TryToUseCurrentItem(Vector2 position)
        {
            _playerEquip.TryToUse();
        }
    }
}
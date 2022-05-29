using UnityEngine;
using Utility.Component_System;

namespace Player.Core_Components
{
    public class PlayerFlipper : Flipper
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _slingShooterTransform;

        public bool IsFacingRight { get; private set; } = true;

        public void FlipPlayer()
        {
            FlipSprite();
            FlipSlingShooter();
        }

        private void FlipSprite()
        {
            IsFacingRight = !IsFacingRight;
            Transform currentTransform = _playerTransform;
            Vector3 localScale = currentTransform.localScale;
            
            localScale.x *= -1f;
            currentTransform.localScale = localScale;
        }

        private void FlipSlingShooter()
        {
            Vector3 slingShooterLocalScale = _slingShooterTransform.localScale;
            
            slingShooterLocalScale.x *= -1f;
            _slingShooterTransform.localScale = slingShooterLocalScale;
        }
    }
}

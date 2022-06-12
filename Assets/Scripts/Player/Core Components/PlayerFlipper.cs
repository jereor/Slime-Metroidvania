using UnityEngine;
using Utility.Component_System;

namespace Player.Core_Components
{
    public class PlayerFlipper : Flipper
    {
        [SerializeField] private Transform _slingShooterTransform;

        public override void FlipPlayer()
        {
            base.FlipPlayer();
            
            FlipSlingShooter();
        }

        private void FlipSlingShooter()
        {
            Vector3 slingShooterLocalScale = _slingShooterTransform.localScale;
            
            slingShooterLocalScale.x *= -1f;
            _slingShooterTransform.localScale = slingShooterLocalScale;
        }
    }
}

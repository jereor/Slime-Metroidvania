using GameFramework.ComponentSystem;
using UnityEngine;

namespace Player.Core_Components
{
    public class PlayerFlipper : Flipper
    {
        [SerializeField] private Transform _slingShooterTransform;

        public override void Flip()
        {
            base.Flip();
            
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

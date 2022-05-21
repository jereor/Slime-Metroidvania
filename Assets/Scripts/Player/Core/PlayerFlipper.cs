using Player.Core.Slime_Sling;
using Player.State_Machine;
using UnityEngine;

namespace Player.Core
{
    public class PlayerFlipper
    {
        private Transform _playerTransform;
        
        public PlayerFlipper(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

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

        private static void FlipSlingShooter()
        {
            Transform slingShooterTransform = SlingShooter.Instance.transform;
            Vector3 slingShooterLocalScale = slingShooterTransform.localScale;
            
            slingShooterLocalScale.x *= -1f;
            slingShooterTransform.localScale = slingShooterLocalScale;
        }
    }
}

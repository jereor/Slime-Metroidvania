using Player.Core.Parameters;
using UnityEngine;

namespace Player.Core.Modules
{
    public class PlayerFlipper
    {
        private readonly Transform _playerTransform;
        private readonly Transform _slingShooterTransform;

        public PlayerFlipper(PlayerFlipperParameters playerFlipperParameters)
        {
            _playerTransform = playerFlipperParameters.PlayerTransform;
            _slingShooterTransform = playerFlipperParameters.SlingShooterTransform;
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

        private void FlipSlingShooter()
        {
            Vector3 slingShooterLocalScale = _slingShooterTransform.localScale;
            
            slingShooterLocalScale.x *= -1f;
            _slingShooterTransform.localScale = slingShooterLocalScale;
        }
    }
}

using Player.Core.Slime_Sling;
using Player.State_Machine;
using UnityEngine;

namespace Player.Core
{
    public class PlayerFlipper
    {
        private readonly PlayerAdapter _playerAdapter;

        public PlayerFlipper(PlayerAdapter playerAdapter)
        {
            _playerAdapter = playerAdapter;
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
            Transform currentTransform = _playerAdapter.transform;
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

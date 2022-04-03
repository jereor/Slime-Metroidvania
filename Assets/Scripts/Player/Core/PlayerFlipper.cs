using Player.Core.Slime_Sling;
using Player.State_Machine;
using UnityEngine;

namespace Player.Core
{
    public class PlayerFlipper : MonoBehaviour
    {
        private PlayerStateMachine _context;

        public static PlayerFlipper Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void HandleDirectionChange(PlayerStateMachine currentContext)
        {
            _context = currentContext;

            if (_context.HasMoveDirectionChanged() == false)
            {
                return;
            }
            
            FlipSprite();
            FlipSlingShooter();
        }

        private void FlipSprite()
        {
            _context.IsFacingRight = !_context.IsFacingRight;
            Transform currentTransform = transform;
            Vector3 localScale = currentTransform.localScale;
            
            localScale.x *= -1f;
            currentTransform.localScale = localScale;
        }

        private static void FlipSlingShooter()
        {
            Vector3 slingShooterLocalScale = SlingShooter.Instance.transform.localScale;
            slingShooterLocalScale.x *= -1f;
            SlingShooter.Instance.transform.localScale = slingShooterLocalScale;
        }
    }
}

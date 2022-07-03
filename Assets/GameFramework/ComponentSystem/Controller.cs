using UnityEngine;

namespace GameFramework.ComponentSystem
{
    public class Controller : CoreComponent
    {
        [SerializeField] protected Flipper _flipper;
        
        public float CurrentMovementInput { get; set; }
        
        protected bool HasMoveDirectionChanged()
        {
            bool isFacingRight = _flipper.IsFacingRight;
            bool facingRightButNowMovingLeft = isFacingRight && CurrentMovementInput < 0f;
            bool facingLeftButNowMovingRight = !isFacingRight && CurrentMovementInput > 0f;

            return facingRightButNowMovingLeft
                   || facingLeftButNowMovingRight;
        }

        public void Initialize(Flipper flipper)
        {
            _flipper = flipper;
        }
    }
}

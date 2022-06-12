using UnityEngine;
using Utility.Component_System;

namespace Player.Core_Components
{
    public class Controller : CoreComponent
    {
        [SerializeField] protected Flipper _flipper;
        
        public float CurrentMovementInput { get; protected set; }
        
        protected bool HasMoveDirectionChanged()
        {
            bool isFacingRight = _flipper.IsFacingRight;
            bool facingRightButNowMovingLeft = isFacingRight && CurrentMovementInput < 0f;
            bool facingLeftButNowMovingRight = !isFacingRight && CurrentMovementInput > 0f;

            return facingRightButNowMovingLeft
                   || facingLeftButNowMovingRight;
        }
    }
}

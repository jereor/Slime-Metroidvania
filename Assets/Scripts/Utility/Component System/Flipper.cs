using UnityEngine;

namespace Utility.Component_System
{
    public class Flipper : CoreComponent
    {
        [SerializeField] private Transform _transform;
        
        public bool IsFacingRight { get; private set; } = true;
        
        public virtual void FlipPlayer()
        {
            FlipSprite();
        }
        
        private void FlipSprite()
        {
            IsFacingRight = !IsFacingRight;
            Transform currentTransform = _transform;
            Vector3 localScale = currentTransform.localScale;
            
            localScale.x *= -1f;
            currentTransform.localScale = localScale;
        }
    }
}

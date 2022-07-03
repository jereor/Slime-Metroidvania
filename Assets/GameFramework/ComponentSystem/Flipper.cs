using UnityEngine;

namespace GameFramework.ComponentSystem
{
    public class Flipper : CoreComponent
    {
        [SerializeField] private Transform _transform;
        
        public bool IsFacingRight { get; private set; } = true;
        
        public virtual void Flip()
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

        public void Initialize(Transform objectTransform)
        {
            _transform = objectTransform;
        }
    }
}

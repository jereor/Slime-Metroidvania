using Enemies.States.Data;
using UnityEngine;

namespace Enemies
{
    public class Entity : MonoBehaviour
    {
        public FiniteStateMachine StateMachine;
        public bool IsFacingRight { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public Animator Animator { get; private set; }
        
        [Header("Child References")]
        [SerializeField] private Transform _wallChecker;
        [SerializeField] private Transform _ledgeChecker;

        [Header("Enemy Data")]
        [SerializeField] private D_Entity _entityData;

        private Vector2 _velocityWorkspace;

        public virtual void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();
            StateMachine = new FiniteStateMachine();
        }

        public virtual void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
        }

        public virtual void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
        
        public virtual void SetVelocity(float velocity)
        {
            int facingDirection = IsFacingRight ? 1 : -1;
            _velocityWorkspace.Set(facingDirection * velocity, Rb.velocity.y);
            Rb.velocity = _velocityWorkspace;
        }

        public virtual bool CheckWall()
        {
            return Physics2D.Raycast(_wallChecker.position, transform.right, _entityData._wallCheckDistance,
                _entityData._groundLayer.value);
        }

        public virtual bool CheckLedge()
        {
            return Physics2D.Raycast(_ledgeChecker.position, Vector2.down, _entityData._ledgeCheckDistance,
                _entityData._groundLayer.value);
        }

        public virtual void FlipSprite()
        {
            IsFacingRight = !IsFacingRight;
            Transform currentTransform = transform;
            Vector3 localScale = currentTransform.localScale;
            
            localScale.x *= -1f;
            currentTransform.localScale = localScale;
        }

        public virtual void OnDrawGizmos()
        {
            int facingDirection = IsFacingRight ? 1 : -1;
            
            Vector3 wallCheckerPosition = _wallChecker.position;
            Gizmos.DrawLine(wallCheckerPosition, wallCheckerPosition + (Vector3)(Vector2.right * facingDirection * _entityData._wallCheckDistance));
            
            Vector3 ledgeCheckerPosition = _ledgeChecker.position;
            Gizmos.DrawLine(ledgeCheckerPosition, ledgeCheckerPosition + (Vector3)(Vector2.down * _entityData._ledgeCheckDistance));
        }

        // --------- BOOLS ---------
        private bool HasMoveDirectionChanged()
        {
            //Debug.Log($"Facing right: {IsFacingRight} \nVelocity X: {_velocityWorkspace.x}");
            bool facingRightButNowMovingLeft = IsFacingRight && _velocityWorkspace.x < 0f;
            bool facingLeftButNowMovingRight = !IsFacingRight && _velocityWorkspace.x > 0f;

            return facingRightButNowMovingLeft
                   || facingLeftButNowMovingRight;
        }
    }
}

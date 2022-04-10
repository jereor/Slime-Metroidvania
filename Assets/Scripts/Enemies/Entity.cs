using Enemies.States.Data;
using UnityEngine;

namespace Enemies
{
    public class Entity : MonoBehaviour
    {
        protected FiniteStateMachine StateMachine;
        public int FacingDirection { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public Animator Animator { get; private set; }

        [Header("Child References")] 
        [SerializeField] private Transform _wallChecker;
        [SerializeField] private Transform _ledgeChecker;
        [SerializeField] private Transform _playerChecker;

        [Header("Enemy Data")]
        [SerializeField] private D_Entity _entityData;

        private Vector2 _velocityWorkspace;

        public virtual void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();
            StateMachine = new FiniteStateMachine();

            FacingDirection = 1;
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
            _velocityWorkspace.Set(
                FacingDirection * velocity, 
                Rb.velocity.y);
            Rb.velocity = _velocityWorkspace;
        }

        public virtual bool CheckWall()
        {
            return Physics2D.Raycast(
                _wallChecker.position, 
                transform.right * FacingDirection,
                _entityData._wallCheckDistance,
                _entityData._groundLayer.value);
        }

        public virtual bool CheckLedge()
        {
            return Physics2D.Raycast(
                _ledgeChecker.position, 
                Vector2.down, 
                _entityData._ledgeCheckDistance,
                _entityData._groundLayer.value);
        }

        public virtual bool CheckPlayerInMinAggroRange()
        {
            return Physics2D.Raycast(
                _playerChecker.position, 
                transform.right * FacingDirection,
                _entityData._minAggroDistance,
                _entityData._playerLayer.value);
        }

        public virtual bool CheckPlayerInMaxAggroRange()
        {
            return Physics2D.Raycast(
                _playerChecker.position, 
                transform.right * FacingDirection,
                _entityData._maxAggroDistance,
                _entityData._playerLayer.value);
        }

        public virtual void FlipSprite()
        {
            FacingDirection *= -1;
            Transform currentTransform = transform;
            Vector3 localScale = currentTransform.localScale;
            
            localScale.x *= -1f;
            currentTransform.localScale = localScale;
        }

        public virtual void OnDrawGizmos()
        {
            Vector3 wallCheckerPosition = _wallChecker.position;
            Gizmos.DrawLine(wallCheckerPosition,
                wallCheckerPosition + (Vector3) (Vector2.right * FacingDirection * _entityData._wallCheckDistance));

            Vector3 ledgeCheckerPosition = _ledgeChecker.position;
            Gizmos.DrawLine(ledgeCheckerPosition,
                ledgeCheckerPosition + (Vector3) (Vector2.down * _entityData._ledgeCheckDistance));

            Vector3 playerCheckerPosition = _playerChecker.position;
            Gizmos.DrawLine(playerCheckerPosition,
                playerCheckerPosition + (Vector3) (Vector2.right * FacingDirection * _entityData._minAggroDistance));
        }
    }
}

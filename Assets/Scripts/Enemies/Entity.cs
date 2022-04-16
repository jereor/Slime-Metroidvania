using Enemies.States.Data;
using UnityEngine;
using Utility;

namespace Enemies
{
    public class Entity : MonoBehaviour
    {
        protected FiniteStateMachine StateMachine;
        public Rigidbody2D Rb { get; private set; }
        public Animator Animator { get; private set; }
        public AnimationToStateMachine AnimationToStateMachine { get; private set; }

        private int _facingDirection = 1;
        
        [Header("Child References")] 
        [SerializeField] private Transform _wallChecker;
        [SerializeField] private Transform _ledgeChecker;
        [SerializeField] private Transform _playerChecker;

        [Header("Enemy Data")]
        [SerializeField] private D_Entity _entityData;

        private float _currentHealth;

        private int _lastDamageDirection;
        
        private Vector2 _velocityWorkspace;

        public virtual void Start()
        {
            _facingDirection = 1;
            _currentHealth = _entityData._maxHealth;
            StateMachine = new FiniteStateMachine();
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();
            AnimationToStateMachine = GetComponentInChildren<AnimationToStateMachine>();
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
                _facingDirection * velocity, 
                Rb.velocity.y);
            Rb.velocity = _velocityWorkspace;
        }

        public virtual bool CheckWall()
        {
            return Physics2D.Raycast(
                _wallChecker.position, 
                transform.right * _facingDirection,
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
                transform.right * _facingDirection,
                _entityData._minAggroDistance,
                _entityData._playerLayer.value);
        }

        public virtual bool CheckPlayerInMaxAggroRange()
        {
            return Physics2D.Raycast(
                _playerChecker.position, 
                transform.right * _facingDirection,
                _entityData._maxAggroDistance,
                _entityData._playerLayer.value);
        }

        public virtual bool CheckPlayerInCloseRangeAction()
        {
            return Physics2D.Raycast(
                _playerChecker.position, 
                transform.right * _facingDirection, 
                _entityData._closeRangeActionDistance,
                _entityData._playerLayer.value);
        }

        public virtual void FlipSprite()
        {
            _facingDirection *= -1;
            Transform currentTransform = transform;
            Vector3 localScale = currentTransform.localScale;
            
            localScale.x *= -1f;
            currentTransform.localScale = localScale;
        }

        public virtual void DamageHop(float velocity)
        {
            _velocityWorkspace.Set(Rb.velocity.x + velocity/2, velocity);
            Rb.velocity = _velocityWorkspace;
        }
        
        public virtual void Damage(AttackDetails attackDetails)
        {
            _currentHealth -= attackDetails.DamageAmount;

            DamageHop(_entityData._damageHopSpeed);
            
            bool attackFromRight = attackDetails.Position.x > transform.position.x;
            _lastDamageDirection = attackFromRight ? -1 : 1;
        }
        
        public virtual void OnDrawGizmos()
        {
            Vector3 wallCheckerPosition = _wallChecker.position;
            Gizmos.DrawLine(wallCheckerPosition,
                wallCheckerPosition + (Vector3) (Vector2.right * _facingDirection * _entityData._wallCheckDistance));

            Vector3 ledgeCheckerPosition = _ledgeChecker.position;
            Gizmos.DrawLine(ledgeCheckerPosition,
                ledgeCheckerPosition + (Vector3) (Vector2.down * _entityData._ledgeCheckDistance));

            Vector3 playerCheckerPosition = _playerChecker.position;
            Vector2 checkDirection = Vector2.right * _facingDirection;
            Gizmos.DrawWireSphere(playerCheckerPosition + (Vector3)(checkDirection * _entityData._minAggroDistance), 0.2f);
            
            Gizmos.DrawWireSphere(playerCheckerPosition + (Vector3)(checkDirection * _entityData._maxAggroDistance), 0.2f);
            
            Gizmos.DrawWireSphere(playerCheckerPosition + (Vector3)(checkDirection * _entityData._closeRangeActionDistance), 0.2f);
        }
    }
}

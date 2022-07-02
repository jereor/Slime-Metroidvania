using Enemies.Data;
using GameFramework;
using JetBrains.Annotations;
using UnityEngine;

namespace Enemies
{
    public class Entity : MonoBehaviour
    {
        public Rigidbody2D Rb { get; private set; }
        public Animator Animator { get; private set; }
        public AnimationToStateMachine AnimationToStateMachine { get; private set; }
        public int LastDamageDirection { get; private set; }
        
        protected FiniteStateMachine StateMachine;

        private int _facingDirection = 1;

        [Header("Child References")] 
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private Transform _wallChecker;
        [SerializeField] private Transform _ledgeChecker;
        [SerializeField] private Transform _playerChecker;

        [Header("Enemy Data")]
        [SerializeField] private D_Entity _entityData;

        protected bool IsStunned;
        protected bool IsDead;
        
        private float _currentHealth;
        private float _currentStunResistance;
        private float _lastDamageTime;
        
        private Vector2 _velocityWorkspace;

        public virtual void Start()
        {
            _facingDirection = 1;
            _currentHealth = _entityData._maxHealth;
            _currentStunResistance = _entityData._stunResistance;
            
            StateMachine = new FiniteStateMachine();
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();
            AnimationToStateMachine = GetComponentInChildren<AnimationToStateMachine>();
        }

        public virtual void Update()
        {
            StateMachine.CurrentState.LogicUpdate();

            if (Time.time >= _lastDamageTime + _entityData._stunRecoveryTime)
            {
                ResetStunResistance();
            }
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

        public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
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

        public virtual bool CheckGround()
        {
            return Physics2D.OverlapCircle(
                _groundChecker.position, 
                _entityData._groundCheckRadius,
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

        public virtual void ResetStunResistance()
        {
            IsStunned = false;
            _currentStunResistance = _entityData._stunResistance;
        }
        
        [UsedImplicitly]
        public virtual void Damage(AttackDetails attackDetails)
        {
            _lastDamageTime = Time.time;

            _currentHealth -= attackDetails.DamageAmount;
            _currentStunResistance -= attackDetails.StunDamageAmount;

            DamageHop(_entityData._damageHopSpeed);

            Instantiate(_entityData._hitParticles, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360f)));

            bool attackFromRight = attackDetails.Position.x > transform.position.x;
            LastDamageDirection = attackFromRight ? -1 : 1;

            if (_currentStunResistance <= 0)
            {
                IsStunned = true;
            }

            if (_currentHealth <= 0)
            {
                IsDead = true;
            }
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

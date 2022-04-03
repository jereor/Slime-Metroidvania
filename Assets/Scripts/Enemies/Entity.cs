using System;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies
{
    public class Entity : MonoBehaviour
    {
        public FiniteStateMachine StateMachine;
        public int FacingDirection { get; private set; }
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
            Animator = GetComponent<Animator>();

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
            _velocityWorkspace.Set(FacingDirection * velocity, Rb.velocity.y);
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
            Transform currentTransform = transform;
            Vector3 localScale = currentTransform.localScale;
            
            localScale.x *= -1f;
            currentTransform.localScale = localScale;
        }
    }
}

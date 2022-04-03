using System;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies
{
    public class Entity : MonoBehaviour
    {
        public FiniteStateMachine StateMachine;

        public D_Entity EntityData; 
            
        public int FacingDirection { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public Animator Animator { get; private set; }

        [SerializeField] private Transform _wallChecker;
        [SerializeField] private Transform _ledgeChecker;

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
            return Physics2D.Raycast(_wallChecker.position, transform.right, EntityData._wallCheckDistance,
                EntityData._groundLayer.value);
        }

        public virtual bool CheckLedge()
        {
            return Physics2D.Raycast(_ledgeChecker.position, Vector2.down, EntityData._ledgeCheckDistance,
                EntityData._groundLayer.value);
        }
    }
}

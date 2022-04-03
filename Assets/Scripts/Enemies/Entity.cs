using System;
using UnityEngine;

namespace Enemies
{
    public class Entity : MonoBehaviour
    {
        public FiniteStateMachine StateMachine;
        public int FacingDirection { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public Animator Animator { get; private set; }

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
    }
}

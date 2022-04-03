using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Enemy
{
    public class State
    {
        protected readonly FiniteStateMachine StateMachine;
        protected readonly Entity Entity;

        protected float StartTime;

        public State(Entity entity, FiniteStateMachine stateMachine)
        {
            this.Entity = entity;
            this.StateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            StartTime = Time.time;
        }

        public virtual void Exit()
        {
            
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
            
        }
    }
}

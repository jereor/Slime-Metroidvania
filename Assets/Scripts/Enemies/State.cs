using UnityEngine;

namespace Enemies
{
    public class State
    {
        protected readonly FiniteStateMachine StateMachine;
        protected readonly Entity Entity;
        protected readonly string AnimatorBoolName;
        
        protected float StartTime;


        protected State(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName)
        {
            Entity = entity;
            StateMachine = stateMachine;
            AnimatorBoolName = animatorBoolName;
        }

        public virtual void Enter()
        {
            StartTime = Time.time;
            Entity.Animator.SetBool(AnimatorBoolName, true);
            HandleChecks();
        }

        public virtual void Exit()
        {
            Entity.Animator.SetBool(AnimatorBoolName, false);
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
            HandleChecks();
        }

        public virtual void HandleChecks()
        {
            
        }
    }
}

using UnityEngine;

namespace Enemies.States
{
    public class AttackState : State
    {
        protected readonly Transform AttackPosition;

        protected bool IsAnimationFinished;
        
        protected AttackState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, Transform attackPosition) : base(entity, stateMachine, animatorBoolName)
        {
            AttackPosition = attackPosition;
        }

        public override void Enter()
        {
            base.Enter();

            Entity.AnimationToStateMachine.AttackState = this;
            IsAnimationFinished = false;
            Entity.SetVelocity(0f);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void HandleChecks()
        {
            base.HandleChecks();
        }

        public virtual void TriggerAttack()
        {
               
        }

        public virtual void FinishAttack()
        {
            IsAnimationFinished = true;
        }
    }
}

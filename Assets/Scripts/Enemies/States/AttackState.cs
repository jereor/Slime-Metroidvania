using UnityEngine;

namespace Enemies.States
{
    public class AttackState : State
    {
        protected readonly Transform AttackPosition;
        
        protected AttackState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, Transform attackPosition) : base(entity, stateMachine, animatorBoolName)
        {
            AttackPosition = attackPosition;
        }

        public override void Enter()
        {
            base.Enter();
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
    }
}

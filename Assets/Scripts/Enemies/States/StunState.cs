using UnityEngine;

namespace Enemies.States
{
    public class StunState : State
    {
        protected D_StunState StateData;

        protected bool IsStunTimeOver;
        
        protected StunState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_StunState stateData) : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            IsStunTimeOver = false;
            Entity.SetVelocity(StateData._stunKnockbackSpeed, StateData._stunKnockbackAngle, Entity.LastDamageDirection);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + StateData._stunTime)
            {
                IsStunTimeOver = true;
            }
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
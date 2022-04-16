using UnityEngine;

namespace Enemies.States
{
    public class StunState : State
    {
        protected D_StunState StateData;

        protected bool IsStunTimeOver;
        protected bool IsGrounded;
        protected bool IsMovementStopped;
        protected bool CanPerformCloseRangeAction;
        protected bool IsPlayerInMinAggroRange;
        
        protected StunState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_StunState stateData) : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            IsStunTimeOver = false;
            IsMovementStopped = false;
            Entity.SetVelocity(StateData._stunKnockbackSpeed, StateData._stunKnockbackAngle, Entity.LastDamageDirection);
        }

        public override void Exit()
        {
            base.Exit();
            
            Entity.ResetStunResistance();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + StateData._stunTime)
            {
                IsStunTimeOver = true;
            }

            if (IsGrounded 
                && Time.time >= StartTime + StateData._stunKnockbackTime
                && IsMovementStopped == false)
            {
                IsMovementStopped = true;
                Entity.SetVelocity(0f);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void HandleChecks()
        {
            base.HandleChecks();

            IsGrounded = Entity.CheckGround();
            CanPerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
            IsPlayerInMinAggroRange = Entity.CheckPlayerInMinAggroRange();
        }
    }
}
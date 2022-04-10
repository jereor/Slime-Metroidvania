using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class ChargeState : State
    {
        protected readonly D_ChargeState StateData;
        
        protected bool IsPlayerInMinAggroRange;
        protected bool IsDetectingLedge;
        protected bool IsDetectingWall;
        protected bool IsChargeTimeOver;
        protected bool CanPerformCloseRangeAction;
        
        protected ChargeState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_ChargeState stateData) : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            IsChargeTimeOver = false;
            Entity.SetVelocity(StateData._chargeSpeed);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + StateData._chargeTime)
            {
                IsChargeTimeOver = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            
        }

        public override void HandleChecks()
        {
            base.HandleChecks();
            
            IsPlayerInMinAggroRange = Entity.CheckPlayerInMinAggroRange();
            IsDetectingLedge = Entity.CheckLedge();
            IsDetectingWall = Entity.CheckWall();

            CanPerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
        }
    }
}

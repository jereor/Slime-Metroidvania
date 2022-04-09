using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.Worm
{
    public class WormChargeState : ChargeState
    {
        private Worm _worm;
        
        public WormChargeState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_ChargeState stateData, Worm worm) : base(entity, stateMachine, animatorBoolName, stateData)
        {
            _worm = worm;
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

            if (!IsDetectingLedge || IsDetectingWall)
            {
                // TODO: Connect to look for player state
            }
            else if (IsChargeTimeOver)
            {
                if (IsPlayerInMinAggroRange)
                {
                    StateMachine.ChangeState(_worm.PlayerDetectedState);
                }
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

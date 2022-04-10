using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormChargeState : ChargeState
    {
        private readonly Worm _worm;
        
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

            if (IsDetectingLedge == false 
                || IsDetectingWall)
            {
                StateMachine.ChangeState(_worm.LookForPlayerState);
            }
            else if (IsChargeTimeOver)
            {
                if (CanPerformCloseRangeAction)
                {
                    StateMachine.ChangeState(_worm.MeleeAttackState);    
                }
                else if (IsPlayerInMinAggroRange)
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

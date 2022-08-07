using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormChargeState : ChargeState
    {
        private readonly Worm _worm;
        
        public WormChargeState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, D_ChargeState stateData) : base(worm, stateMachine, animatorBoolName, stateData)
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

            if (CanPerformCloseRangeAction)
            {
                StateMachine.ChangeState(_worm.MeleeAttackState);    
            }
            else if (IsDetectingLedge == false 
                || IsDetectingWall)
            {
                StateMachine.ChangeState(_worm.LookForPlayerState);
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

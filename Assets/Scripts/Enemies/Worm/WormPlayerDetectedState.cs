using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormPlayerDetectedState : PlayerDetectedState
    {
        private readonly Worm _worm;
        
        public WormPlayerDetectedState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, D_PlayerDetectedState stateData) : base(worm, stateMachine, animatorBoolName, stateData)
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
            else if (CanPerformLongRangeAction || IsAnimationFinished)
            {
                StateMachine.ChangeState(_worm.ChargeState);
            }
            else if (IsPlayerInMaxAggroRange == false)
            {
                StateMachine.ChangeState(_worm.LookForPlayerState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}

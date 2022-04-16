using Enemies.States;

namespace Enemies.Worm
{
    public class WormStunState : StunState
    {
        private readonly Worm _worm;

        public WormStunState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, D_StunState stateData) : base(worm, stateMachine, animatorBoolName, stateData)
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

            if (!IsStunTimeOver)
            {
                return;
            }
            
            if (CanPerformCloseRangeAction)
            {
                StateMachine.ChangeState(_worm.MeleeAttackState);    
            }
            else if (IsPlayerInMinAggroRange)
            {
                 StateMachine.ChangeState(_worm.ChargeState);
            }
            else
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
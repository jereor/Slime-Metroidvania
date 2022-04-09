using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormPlayerDetectedState : PlayerDetectedState
    {
        private readonly Worm _worm;
        
        public WormPlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_PlayerDetectedState stateData, Worm worm) : base(entity, stateMachine, animatorBoolName, stateData)
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

            if (WillPerformLongRangeAction)
            {
                StateMachine.ChangeState(_worm.ChargeState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}

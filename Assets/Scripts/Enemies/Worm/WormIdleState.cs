using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormIdleState : IdleState
    {
        private readonly Worm _worm;
        
        public WormIdleState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, D_IdleState stateData) : base(worm, stateMachine, animatorBoolName, stateData)
        {
            _worm = worm;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsPlayerInMinAggroRange)
            {
                StateMachine.ChangeState(_worm.PlayerDetectedState);
            }
            else if (IsIdleTimeOver)
            {
                StateMachine.ChangeState(_worm.MoveState);
                
            }
        }
    }
}

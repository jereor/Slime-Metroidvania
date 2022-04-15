using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormMoveState : MoveState
    {
        private readonly Worm _worm; 
            
        public WormMoveState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, D_MoveState stateData) : base(worm, stateMachine, animatorBoolName, stateData)
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
            else if (IsDetectingLedge == false 
                     || IsDetectingWall)
            {
                _worm.IdleState.SetFlipAfterIdle(true);
                StateMachine.ChangeState(_worm.IdleState);
            }
        }
    }
}

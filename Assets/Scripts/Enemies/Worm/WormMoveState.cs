using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormMoveState : MoveState
    {
        private Worm _worm; 
            
        public WormMoveState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_MoveState stateData, Worm worm) : base(entity, stateMachine, animatorBoolName, stateData)
        {
            _worm = worm;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsDetectingWall || !IsDetectingLedge)
            {
                StateMachine.ChangeState(_worm.IdleState);
            }
        }
    }
}

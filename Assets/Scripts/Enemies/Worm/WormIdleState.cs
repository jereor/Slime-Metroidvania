using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormIdleState : IdleState
    {
        private Worm _worm;
        
        public WormIdleState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_IdleState stateData, Worm worm) : base(entity, stateMachine, animatorBoolName, stateData)
        {
            _worm = worm;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsIdleTimeOver)
            {
                StateMachine.ChangeState(_worm.MoveState);
            }
        }
    }
}

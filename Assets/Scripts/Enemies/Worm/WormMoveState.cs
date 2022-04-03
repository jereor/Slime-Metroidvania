using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormMoveState : MoveState
    {
        public WormMoveState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_MoveState stateData) : base(entity, stateMachine, animatorBoolName, stateData)
        {
        }
    }
}

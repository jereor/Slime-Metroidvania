using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormIdleState : IdleState
    {
        protected WormIdleState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_IdleState stateData) : base(entity, stateMachine, animatorBoolName, stateData)
        {
        }
    }
}

using Enemies.States.Data;

namespace Enemies.States
{
    public class IdleState : State
    {
        protected D_IdleState StateData;
        protected bool FlipsAfterIdle;
        
        protected IdleState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_IdleState stateData) : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }

        public void SetFlipsAfterIdle(bool flipOrNot)
        {
            FlipsAfterIdle = flipOrNot;
        }
    }
}

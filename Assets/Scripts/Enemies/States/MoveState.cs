using Enemies.States.Data;

namespace Enemies.States
{
    public class MoveState : State
    {
        protected D_MoveState StateData;
        
        public MoveState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_MoveState stateData) 
            : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }
        
        
    }
}

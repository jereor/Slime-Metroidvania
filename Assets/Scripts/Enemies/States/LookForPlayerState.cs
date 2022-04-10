namespace Enemies.States
{
    public class LookForPlayerState : State
    {
    

        protected LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName) : base(entity, stateMachine, animatorBoolName)
        {
        }
    }
}

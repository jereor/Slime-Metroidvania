using Enemies.States.Data;

namespace Enemies.States
{
    public class LookForPlayerState : State
    {
        protected D_LookForPlayerState StateData;

        protected LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
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
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void HandleChecks()
        {
            base.HandleChecks();
        }
    }
}

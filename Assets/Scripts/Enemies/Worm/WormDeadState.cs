using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormDeadState : DeadState
    {
        private readonly Worm _worm;
        
        protected WormDeadState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, D_DeadState stateData) : base(worm, stateMachine, animatorBoolName, stateData)
        {
            _worm = worm;
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

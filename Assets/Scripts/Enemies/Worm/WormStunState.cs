using Enemies.States;

namespace Enemies.Worm
{
    public class WormStunState : StunState
    {
        private Worm _worm;
        
        protected WormStunState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, D_StunState stateData) : base(worm, stateMachine, animatorBoolName, stateData)
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
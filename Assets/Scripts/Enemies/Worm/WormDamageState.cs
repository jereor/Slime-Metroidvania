namespace Enemies.Worm
{
    public class WormDamageState : DamageState
    {
        private readonly Worm _worm;
        
        public WormDamageState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName) : base(worm, stateMachine, animatorBoolName)
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
            
            if (!IsAnimationFinished)
            {
                return;
            }
            
            if (IsPlayerInMinAggroRange)
            {
                StateMachine.ChangeState(_worm.PlayerDetectedState);
            }
            else
            {
                StateMachine.ChangeState(_worm.LookForPlayerState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void HandleChecks()
        {
            base.HandleChecks();
        }

        public override void FinishDamageAnimation()
        {
            base.FinishDamageAnimation();
        }
    }
}
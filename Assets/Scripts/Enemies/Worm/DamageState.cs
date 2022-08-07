namespace Enemies.Worm
{
    public class DamageState : State
    {
        protected bool IsAnimationFinished;
        protected bool IsPlayerInMinAggroRange;
        
        protected DamageState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName) : base(entity, stateMachine, animatorBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Entity.AnimationToStateMachine.DamageState = this;
            IsAnimationFinished = false;
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
            
            IsPlayerInMinAggroRange = Entity.CheckPlayerInMinAggroRange();
        }

        public virtual void FinishDamageAnimation()
        {
            IsAnimationFinished = true;
        }
        
    }
}
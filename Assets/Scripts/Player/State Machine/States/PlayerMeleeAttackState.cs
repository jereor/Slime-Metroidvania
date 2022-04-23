namespace Player.State_Machine.States
{
    public class PlayerMeleeAttackState : PlayerBaseState
    {
        public PlayerMeleeAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
            : base(currentContext, playerStateFactory)
        {
            
        }

        protected override void EnterState()
        {
            Context.Animator.SetBool(Context.PlayerAnimations.IsMeleeAttackingHash, true);
        }

        protected override void ExitState()
        {
            Context.Animator.SetBool(Context.PlayerAnimations.IsMeleeAttackingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void CheckSwitchStates()
        {
            if (Context.IsMeleeAttacking)
            {
                return;
            }
            
            SwitchState(Context.IsMovementPressed 
                ? Factory.Move() 
                : Factory.Idle());
        }

        protected override void InitializeSubState()
        {
        }
    }
}

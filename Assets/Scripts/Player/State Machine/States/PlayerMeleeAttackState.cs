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
            Context.PlayerAdapter.Animator.SetBool(Context.PlayerAdapter.PlayerAnimations.IsMeleeAttackingHash, true);
        }

        protected override void ExitState()
        {
            Context.PlayerAdapter.Animator.SetBool(Context.PlayerAdapter.PlayerAnimations.IsMeleeAttackingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void CheckSwitchStates()
        {
            if (Context.PlayerAdapter.PlayerCombat.IsMeleeAttacking)
            {
                return;
            }
            
            SwitchState(Context.PlayerAdapter.PlayerController.IsMovementPressed 
                ? Factory.Move() 
                : Factory.Idle());
        }

        protected override void InitializeSubState()
        {
        }
    }
}

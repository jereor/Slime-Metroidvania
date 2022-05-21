namespace Player.State_Machine.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            
        }

        protected override void EnterState()
        {
            Context.PlayerAdapter.Animator.SetBool(Context.PlayerAdapter.PlayerAnimations.IsMovingHash, true);
        }

        protected override void ExitState()
        {
            Context.PlayerAdapter.Animator.SetBool(Context.PlayerAdapter.PlayerAnimations.IsMovingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
            Context.PlayerAdapter.HandleMovement();
        }

        protected override void InitializeSubState()
        {
        }

        protected override void CheckSwitchStates()
        {
            if (Context.PlayerAdapter.PlayerCombat.IsMeleeAttacking)
            {
                SwitchState(Factory.MeleeAttack());
            }
            else if (Context.PlayerAdapter.PlayerController.IsMovementPressed == false)
            {
                SwitchState(Factory.Idle());
            }
        }
    }
}

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
            PlayerAdapter.SetAnimatorBool(PlayerAdapter.PlayerAnimations.IsMovingHash, true);
        }

        protected override void ExitState()
        {
            PlayerAdapter.SetAnimatorBool(PlayerAdapter.PlayerAnimations.IsMovingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
            PlayerAdapter.HandleMovement();
        }

        protected override void InitializeSubState()
        {
        }

        protected override void CheckSwitchStates()
        {
            if (PlayerAdapter.IsMeleeAttacking())
            {
                SwitchState(Factory.MeleeAttack());
            }
            else if (PlayerAdapter.IsMovementPressed() == false)
            {
                SwitchState(Factory.Idle());
            }
        }
    }
}

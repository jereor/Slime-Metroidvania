namespace Player.State_Machine.States
{
    public sealed class PlayerJumpState : PlayerBaseState
    {
        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) 
        {
            IsRootState = true;
            InitializeSubState();
        }

        // ENTER STATE
        protected override void EnterState()
        {
            PlayerAdapter.SetAnimatorBool(PlayerAdapter.PlayerAnimations.IsAirborneHash, true);
            PlayerAdapter.PlayerMovement.JumpStart();
        }


        // EXIT STATE
        protected override void ExitState()
        {
            PlayerAdapter.SetAnimatorBool(PlayerAdapter.PlayerAnimations.IsAirborneHash, false);
            PlayerAdapter.PlayerMovement.JumpEnd();
        }


        // UPDATE STATE
        protected override void UpdateState()
        {
            CheckSwitchStates();
        }


        // INITIALIZE SUB STATE
        protected override void InitializeSubState()
        {
            if (PlayerAdapter.IsMeleeAttacking())
            {
                SetSubState(Factory.MeleeAttack());
            }
            else if (PlayerAdapter.IsMovementPressed())
            {
                SetSubState(Factory.Move());
            }
            else
            {
                SetSubState(Factory.Idle());
            }
        }
    

        // CHECK SWITCH STATES
        protected override void CheckSwitchStates()
        {
            if (PlayerAdapter.IsGrounded() && PlayerAdapter.IsFalling())
            {
                Logger.LogVerbose("Jump -> Grounded");
                SwitchState(Factory.Grounded());
            }
        }
    }
}

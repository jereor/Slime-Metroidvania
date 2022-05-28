using Player.Core.Modules;

namespace Player.State_Machine.States
{
    public sealed class PlayerJumpState : PlayerBaseState
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerCombat _playerCombat;
        
        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            _playerMovement = currentContext.PlayerAdapter.PlayerMovement;
            _playerCombat = currentContext.PlayerAdapter.PlayerCombat;
            
            IsRootState = true;
            InitializeSubState();
        }

        // ENTER STATE
        protected override void EnterState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsAirborneHash, true);
            _playerMovement.JumpStart();
        }


        // EXIT STATE
        protected override void ExitState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsAirborneHash, false);
            _playerMovement.JumpEnd();
        }


        // UPDATE STATE
        protected override void UpdateState()
        {
            CheckSwitchStates();
        }


        // INITIALIZE SUB STATE
        protected override void InitializeSubState()
        {
            if (_playerCombat.IsMeleeAttacking)
            {
                SetSubState(Factory.MeleeAttack());
            }
            else if (PlayerController.IsMovementPressed)
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
            if (_playerMovement.IsGrounded() && _playerMovement.IsFalling)
            {
                Logger.LogVerbose("Jump -> Grounded");
                SwitchState(Factory.Grounded());
            }
        }
    }
}

using Player.Core_Components;

namespace Player.State_Machine.States
{
    public sealed class PlayerJumpState : PlayerBaseState
    {
        private PlayerController _playerController;
        private PlayerAnimations _playerAnimations;
        private PlayerMovement _playerMovement;
        private PlayerCombat _playerCombat;

        private PlayerController PlayerController
        {
            get { return _playerController ??= Core.GetCoreComponent<PlayerController>(); }
        }
        
        private PlayerAnimations PlayerAnimations
        {
            get { return _playerAnimations ??= Core.GetCoreComponent<PlayerAnimations>(); }
        }

        private PlayerMovement PlayerMovement
        {
            get { return _playerMovement ??= Core.GetCoreComponent<PlayerMovement>(); }
        }
        
        private PlayerCombat PlayerCombat
        {
            get { return _playerCombat ??= Core.GetCoreComponent<PlayerCombat>(); }
        }
        
        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {IsRootState = true;
            InitializeSubState();
        }

        // ENTER STATE
        protected override void EnterState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsAirborneHash, true);
            PlayerMovement.JumpStart();
        }


        // EXIT STATE
        protected override void ExitState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsAirborneHash, false);
            PlayerMovement.JumpEnd();
        }


        // UPDATE STATE
        protected override void UpdateState()
        {
            CheckSwitchStates();
        }


        // INITIALIZE SUB STATE
        protected override void InitializeSubState()
        {
            if (PlayerCombat.IsMeleeAttacking)
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
            if (PlayerMovement.IsGrounded() && PlayerMovement.IsFalling)
            {
                Logger.LogVerbose("Jump -> Grounded");
                SwitchState(Factory.Grounded());
            }
        }
    }
}

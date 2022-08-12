using Player.Core_Components;

namespace Player.State_Machine.States
{
    public sealed class PlayerAirborneState : PlayerBaseState
    {
        private PlayerController _playerController;
        private PlayerMovement _playerMovement;
        private PlayerCombat _playerCombat;

        private PlayerController PlayerController
        {
            get { return _playerController ??= Core.GetCoreComponent<PlayerController>(); }
        }

        private PlayerMovement PlayerMovement
        {
            get { return _playerMovement ??= Core.GetCoreComponent<PlayerMovement>(); }
        }
        
        private PlayerCombat PlayerCombat
        {
            get { return _playerCombat ??= Core.GetCoreComponent<PlayerCombat>(); }
        }
        
        public PlayerAirborneState(PlayerBase player, PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(player, currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

        // ENTER STATE
        protected override void EnterState()
        {
        }


        // EXIT STATE
        protected override void ExitState()
        {
        }


        // UPDATE STATE
        protected override void UpdateState()
        {
            CheckSwitchStates();
        }


        // INITIALIZE SUB STATE
        protected override void InitializeSubState()
        {
            if (PlayerMovement.IsKnockedBack)
            {
                SetSubState(Factory.KnockedBack());
            }
            else if (PlayerController.IsJumpInputPressed)
            {
                SetSubState(Factory.Jump());
            }
            else if (PlayerMovement.IsFalling)
            {
                SetSubState(Factory.Fall());
            }
            else if (PlayerCombat.IsMeleeAttacking)
            {
                SetSubState(Factory.MeleeAttack());
            }
        }
    

        // CHECK SWITCH STATES
        protected override void CheckSwitchStates()
        {
            if (PlayerMovement.IsGrounded() && PlayerMovement.IsFalling)
            {
                SwitchState(Factory.Grounded());
            }
        }
    }
}

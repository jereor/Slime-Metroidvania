using Player.Core.Modules;

namespace Player.State_Machine.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerCombat _playerCombat;
        
        public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            _playerMovement = PlayerAdapter.PlayerMovement;
            _playerCombat = PlayerAdapter.PlayerCombat;
        }

        protected override void EnterState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsMovingHash, true);
        }

        protected override void ExitState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsMovingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
            _playerMovement.HandleMovement();
        }

        protected override void InitializeSubState()
        {
        }

        protected override void CheckSwitchStates()
        {
            if (_playerCombat.IsMeleeAttacking)
            {
                Logger.LogVerbose("Move -> Melee");
                SwitchState(Factory.MeleeAttack());
            }
            else if (PlayerController.IsMovementPressed == false)
            {
                Logger.LogVerbose("Move -> Idle");
                SwitchState(Factory.Idle());
            }
        }
    }
}

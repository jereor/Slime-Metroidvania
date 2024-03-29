using Player.Core_Components;

namespace Player.State_Machine.States
{
    public class PlayerMoveState : PlayerBaseState
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
        
        public PlayerMoveState(PlayerBase player, PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(player, currentContext, playerStateFactory)
        {
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
        }

        protected override void InitializeSubState()
        {
        }

        protected override void CheckSwitchStates()
        {
            if (PlayerCombat.IsMeleeAttacking)
            {
                SwitchState(Factory.MeleeAttack());
            }
            else if (PlayerController.IsMovementInputPressed == false)
            {
                SwitchState(Factory.Idle());
            }
            else if (PlayerMovement.IsJumping)
            {
                SwitchState(Factory.Jump());
            }
            else if (PlayerMovement.IsFalling)
            {
                SwitchState(Factory.Fall());
            }
        }
    }
}

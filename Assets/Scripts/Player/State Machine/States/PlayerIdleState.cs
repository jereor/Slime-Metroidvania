using Player.Core_Components;

namespace Player.State_Machine.States
{
    public class PlayerIdleState : PlayerBaseState
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
        
        public PlayerIdleState(PlayerBase player, PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(player, currentContext, playerStateFactory)
        {
        }

        protected override void EnterState()
        {
        }

        protected override void ExitState()
        {
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
            else if (PlayerController.IsMovementInputPressed)
            {
                SwitchState(Factory.Move());
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

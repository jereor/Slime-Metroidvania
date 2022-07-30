using Player.Core_Components;

namespace Player.State_Machine.States
{
    public class PlayerJumpPeakState : PlayerBaseState
    {
        private PlayerAnimations _playerAnimations;
        private PlayerMovement _playerMovement;
        private PlayerCombat _playerCombat;
        
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
        
        public PlayerJumpPeakState(PlayerBase player, PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(player, currentContext, playerStateFactory)
        {
        }

        protected override void EnterState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsAtJumpPeak, true);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void ExitState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsAtJumpPeak, false);
        }

        protected override void CheckSwitchStates()
        {
            if (PlayerMovement.IsFalling)
            {
                SwitchState(Factory.Fall());
            }
            else if (PlayerCombat.IsMeleeAttacking)
            {
                SwitchState(Factory.MeleeAttack());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}
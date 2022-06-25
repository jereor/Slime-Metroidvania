using Player.Core_Components;

namespace Player.State_Machine.States
{
    public class PlayerMeleeAttackState : PlayerBaseState
    {
        private PlayerAnimations _playerAnimations;
        private PlayerController _playerController;
        private PlayerCombat _playerCombat;

        private PlayerAnimations PlayerAnimations
        {
            get { return _playerAnimations ??= Core.GetCoreComponent<PlayerAnimations>(); }
        }
        
        private PlayerController PlayerController
        {
            get { return _playerController ??= Core.GetCoreComponent<PlayerController>(); }
        }
        
        private PlayerCombat PlayerCombat
        {
            get { return _playerCombat ??= Core.GetCoreComponent<PlayerCombat>(); }
        }
        
        public PlayerMeleeAttackState(PlayerBase player, PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(player, currentContext, playerStateFactory)
        {
        }

        protected override void EnterState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsMeleeAttackingHash, true);
        }

        protected override void ExitState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsMeleeAttackingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void CheckSwitchStates()
        {
            if (PlayerCombat.IsMeleeAttacking)
            {
                return;
            }

            SwitchState(PlayerController.IsMovementInputPressed
                ? Factory.Move()
                : Factory.Idle());
        }

        protected override void InitializeSubState()
        {
        }
    }
}

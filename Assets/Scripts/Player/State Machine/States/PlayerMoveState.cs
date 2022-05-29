using Player.Core_Components;

namespace Player.State_Machine.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        private PlayerController _playerController;
        private PlayerAnimations _playerAnimations;
        private PlayerCombat _playerCombat;
        
        private PlayerController PlayerController
        {
            get { return _playerController ??= Core.GetCoreComponent<PlayerController>(); }
        }
        
        private PlayerAnimations PlayerAnimations
        {
            get { return _playerAnimations ??= Core.GetCoreComponent<PlayerAnimations>(); }
        }
        
        private PlayerCombat PlayerCombat
        {
            get { return _playerCombat ??= Core.GetCoreComponent<PlayerCombat>(); }
        }
        
        public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
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

using Player.Core_Components;
using UnityEngine;

namespace Player.State_Machine.States
{
    public sealed class PlayerGroundedState : PlayerBaseState
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
        
        public PlayerGroundedState(Core_Components.Player player, PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base (player, currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

        protected override void EnterState()
        {
        }

        protected override void ExitState()
        {
            PlayerMovement.SetLastGroundedTime();
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        // TODO: Consider letting each state update and handle bools directly?
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

        protected override void CheckSwitchStates()
        {
            bool isJumpBuffered = Time.time - PlayerController.JumpButtonPressedTime <= PlayerMovement.CoyoteTime;
            if (isJumpBuffered)
            {
                SwitchState(Factory.Jump());
            }
        }
    }
}

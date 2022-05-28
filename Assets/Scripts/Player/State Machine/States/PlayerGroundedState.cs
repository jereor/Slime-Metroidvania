using Player.Core.Modules;
using UnityEngine;

namespace Player.State_Machine.States
{
    public sealed class PlayerGroundedState : PlayerBaseState
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerCombat _playerCombat;
        
        public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base (currentContext, playerStateFactory)
        {
            _playerMovement = currentContext.PlayerAdapter.PlayerMovement;
            _playerCombat = currentContext.PlayerAdapter.PlayerCombat;
            
            IsRootState = true;
            InitializeSubState();
        }

        protected override void EnterState()
        {
        }

        protected override void ExitState()
        {
            _playerMovement.SetLastGroundedTime();
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void InitializeSubState()
        {
            if (_playerCombat.IsMeleeAttacking)
            {
                SetSubState(Factory.MeleeAttack());
            }
            else if (PlayerController.IsMovementPressed)
            {
                Logger.LogVerbose("Grounded Substate: Move");
                SetSubState(Factory.Move());
            }
            else
            {
                Logger.LogVerbose("Grounded Substate: Idle");
                SetSubState(Factory.Idle());
            }
        }

        protected override void CheckSwitchStates()
        {
            bool isJumpBuffered = Time.time - PlayerController.JumpButtonPressedTime <= _playerMovement.CoyoteTime;
            if (isJumpBuffered)
            {
                Logger.LogVerbose("Grounded -> Jump");
                SwitchState(Factory.Jump());
            }
        }
    }
}

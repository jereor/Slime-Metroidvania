using Player.Core_Components;
using UnityEngine;

namespace Player.State_Machine.States
{
    public class PlayerJumpState : PlayerBaseState
    {
        private PlayerAnimations _playerAnimations;
        private PlayerMovement _playerMovement;
        
        private PlayerAnimations PlayerAnimations
        {
            get { return _playerAnimations ??= Core.GetCoreComponent<PlayerAnimations>(); }
        }
        
        private PlayerMovement PlayerMovement
        {
            get { return _playerMovement ??= Core.GetCoreComponent<PlayerMovement>(); }
        }
        
        public PlayerJumpState(PlayerBase player, PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(player, currentContext, playerStateFactory)
        {
        }

        protected override void EnterState()
        {
            Debug.Log(this);
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsJumpingHash, true);
            PlayerMovement.JumpStart();
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void ExitState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsJumpingHash, false);
            PlayerMovement.JumpEnd();
        }

        protected override void CheckSwitchStates()
        {
            if (PlayerMovement.IsAtJumpPeak)
            {
                SwitchState(Factory.JumpPeak());
            }
            else if (PlayerMovement.IsFalling)
            {
                SwitchState(Factory.Fall());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}
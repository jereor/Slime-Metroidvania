using UnityEngine;

namespace Player.State_Machine.States
{
    public sealed class PlayerGroundedState : PlayerBaseState
    {
        public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base (currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

        protected override void EnterState()
        {
        }

        protected override void ExitState()
        {
            PlayerAdapter.ResetLastGroundedTime();
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void InitializeSubState()
        {
            if (PlayerAdapter.IsMeleeAttacking())
            {
                SetSubState(Factory.MeleeAttack());
            }
            else if (PlayerAdapter.IsMovementPressed())
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
            bool isJumpBuffered = Time.time - PlayerAdapter.PlayerController.JumpButtonPressedTime <=
                                  PlayerAdapter.PlayerMovement.CoyoteTime;
            if (isJumpBuffered)
            {
                Logger.LogVerbose("Grounded -> Jump");
                SwitchState(Factory.Jump());
            }
        }
    }
}

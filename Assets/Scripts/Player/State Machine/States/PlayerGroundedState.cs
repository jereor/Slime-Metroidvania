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
                SetSubState(Factory.Move());
            }
            else
            {
                SetSubState(Factory.Idle());
            }
        }

        protected override void CheckSwitchStates()
        {
            if (PlayerAdapter.IsJumpPressed())
            {
                SwitchState(Factory.Jump());
            }
        }
    }
}

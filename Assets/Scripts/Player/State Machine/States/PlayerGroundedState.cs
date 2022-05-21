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
            Context.PlayerAdapter.LastGroundedTime = Time.time;
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void InitializeSubState()
        {
            if (Context.PlayerAdapter.PlayerCombat.IsMeleeAttacking)
            {
                SetSubState(Factory.MeleeAttack());
            }
            else if (Context.PlayerAdapter.PlayerController.IsMovementPressed)
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
            if (Context.PlayerAdapter.PlayerController.IsJumpPressed)
            {
                SwitchState(Factory.Jump());
            }
        }
    }
}

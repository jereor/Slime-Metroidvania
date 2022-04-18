using UnityEngine;

namespace Player.State_Machine
{
    public sealed class PlayerGroundedState : PlayerBaseState
    {
        public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base (currentContext, playerStateFactory){
            IsRootState = true;
            InitializeSubState();
        }

        protected override void EnterState()
        {
        }

        protected override void ExitState()
        {
            Context.LastGroundedTime = Time.time;
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void InitializeSubState()
        {
            if (Context.IsMeleeAttacking)
            {
                SetSubState(Factory.MeleeAttack());
            }
            else if (Context.IsMovementPressed)
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
            if (Context.IsJumpPressed)
            {
                SwitchState(Factory.Jump());
            }
        }
    }
}

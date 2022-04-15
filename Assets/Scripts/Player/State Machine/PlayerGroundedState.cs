using UnityEngine;

namespace Player.State_Machine
{
    public class PlayerGroundedState : PlayerBaseState
    {
        public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base (currentContext, playerStateFactory){
            IsRootState = true;
            InitializeSubState();
        }

        public PlayerStateFactory PlayerStateFactory { get; }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
            Context.LastGroundedTime = Time.time;
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void InitializeSubState()
        {
            if (Context.IsMovementPressed)
            {
                SetSubState(Factory.Move());
            }
            else
            {
                SetSubState(Factory.Idle());
            }
        }

        public override void CheckSwitchStates()
        {
            if (Context.IsJumpPressed)
            {
                SwitchState(Factory.Jump());
            }
        }
    }
}

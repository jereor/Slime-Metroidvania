using UnityEngine;

namespace Player.State_Machine
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }

        public PlayerStateFactory PlayerStateFactory { get; }

        protected override void EnterState()
        {
        }

        protected override void ExitState()
        {
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void InitializeSubState()
        {
            throw new System.NotImplementedException();
        }

        protected override void CheckSwitchStates()
        {
            if (Context.IsMeleeAttacking)
            {
                SwitchState(Factory.MeleeAttack());
            }
            else if (Context.IsMovementPressed)
            {
                SwitchState(Factory.Move());
            }
        }
    }
}

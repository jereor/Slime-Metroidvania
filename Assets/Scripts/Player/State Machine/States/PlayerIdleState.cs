namespace Player.State_Machine.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            
        }

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
        }

        protected override void CheckSwitchStates()
        {
            if (Context.PlayerAdapter.PlayerCombat.IsMeleeAttacking)
            {
                SwitchState(Factory.MeleeAttack());
            }
            else if (Context.PlayerAdapter.PlayerController.IsMovementPressed)
            {
                SwitchState(Factory.Move());
            }
        }
    }
}

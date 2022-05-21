namespace Player.State_Machine.States
{
    public class PlayerMeleeAttackState : PlayerBaseState
    {
        public PlayerMeleeAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
            : base(currentContext, playerStateFactory)
        {
            
        }

        protected override void EnterState()
        {
            PlayerAdapter.SetAnimatorBool(PlayerAdapter.PlayerAnimations.IsMeleeAttackingHash, true);
        }

        protected override void ExitState()
        {
            PlayerAdapter.SetAnimatorBool(PlayerAdapter.PlayerAnimations.IsMeleeAttackingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void CheckSwitchStates()
        {
            if (PlayerAdapter.IsMeleeAttacking())
            {
                return;
            }
            
            SwitchState(PlayerAdapter.IsMovementPressed()
                ? Factory.Move() 
                : Factory.Idle());
        }

        protected override void InitializeSubState()
        {
        }
    }
}

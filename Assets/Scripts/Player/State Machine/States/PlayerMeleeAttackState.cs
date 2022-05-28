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
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsMeleeAttackingHash, true);
        }

        protected override void ExitState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsMeleeAttackingHash, false);
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
            
            Logger.LogVerbose("Melee -> Move/Idle");
            
            SwitchState(PlayerAdapter.IsMovementPressed()
                ? Factory.Move() 
                : Factory.Idle());
        }

        protected override void InitializeSubState()
        {
        }
    }
}

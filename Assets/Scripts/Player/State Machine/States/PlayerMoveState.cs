namespace Player.State_Machine.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            
        }

        protected override void EnterState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsMovingHash, true);
        }

        protected override void ExitState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsMovingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
            PlayerAdapter.HandleMovement();
        }

        protected override void InitializeSubState()
        {
        }

        protected override void CheckSwitchStates()
        {
            if (PlayerAdapter.IsMeleeAttacking())
            {
                Logger.LogVerbose("Move -> Melee");
                SwitchState(Factory.MeleeAttack());
            }
            else if (PlayerAdapter.IsMovementPressed() == false)
            {
                Logger.LogVerbose("Move -> Idle");
                SwitchState(Factory.Idle());
            }
        }
    }
}

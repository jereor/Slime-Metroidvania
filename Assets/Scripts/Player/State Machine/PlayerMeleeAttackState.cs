namespace Player.State_Machine
{
    public class PlayerMeleeAttackState : PlayerBaseState
    {
        public PlayerMeleeAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
            : base(currentContext, playerStateFactory)
        {
            
        }

        public override void EnterState()
        {
            Context.Animator.SetBool(Context.IsMeleeAttackingHash, true);
        }
        
        public override void ExitState()
        {
            Context.Animator.SetBool(Context.IsMeleeAttackingHash, false);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void CheckSwitchStates()
        {
            if (Context.IsMeleeAttacking)
            {
                return;
            }
            
            SwitchState(Context.IsMovementPressed 
                ? Factory.Move() 
                : Factory.Idle());
        }

        public override void InitializeSubState()
        {
            throw new System.NotImplementedException();
        }
    }
}

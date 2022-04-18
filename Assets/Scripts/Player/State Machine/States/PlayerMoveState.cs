using Player.Core;

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
            Context.Animator.SetBool(Context.IsMovingHash, true);
        }

        protected override void ExitState()
        {
            Context.Animator.SetBool(Context.IsMovingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
            PlayerMovement.Instance.HandleMovement(Context);
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
            else if (Context.IsMovementPressed == false)
            {
                SwitchState(Factory.Idle());
            }
        }
    }
}

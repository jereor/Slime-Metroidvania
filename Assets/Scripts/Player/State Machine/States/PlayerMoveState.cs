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
            Context.Animator.SetBool(Context.PlayerAnimations.IsMovingHash, true);
        }

        protected override void ExitState()
        {
            Context.Animator.SetBool(Context.PlayerAnimations.IsMovingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
            PlayerMovement.Instance.HandleMovement(Context);
        }

        protected override void InitializeSubState()
        {
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

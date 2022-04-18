using Player.Core;
using UnityEngine;

namespace Player.State_Machine
{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }

        public PlayerStateFactory PlayerStateFactory { get; }

        public override void EnterState()
        {
            Context.Animator.SetBool(Context.IsMovingHash, true);
        }

        public override void ExitState()
        {
            Context.Animator.SetBool(Context.IsMovingHash, false);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
            PlayerMovement.Instance.HandleMovement(Context);
        }
    
        public override void InitializeSubState()
        {
            throw new System.NotImplementedException();
        }
    
        public override void CheckSwitchStates()
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

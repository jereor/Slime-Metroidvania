using Player.Core;
using UnityEngine;

namespace Player.State_Machine.States
{
    public sealed class PlayerJumpState : PlayerBaseState
    {
        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) 
        {
            IsRootState = true;
            InitializeSubState();
        }

        // ENTER STATE
        protected override void EnterState()
        {
            Context.Animator.SetBool(Context.PlayerAnimations.IsAirborneHash, true);
            JumpStart();
        }

        private void JumpStart()
        {
            if (Context.IsGrounded() == false)
            {
                return;
            }

            Context.JumpButtonPressedTime = Time.time;

            bool isCoyoteTime = Time.time - Context.LastGroundedTime <= PlayerMovement.Instance.CoyoteTime;
            bool isJumpBuffered = Time.time - Context.JumpButtonPressedTime <= PlayerMovement.Instance.CoyoteTime;

            if (isCoyoteTime && isJumpBuffered)
            {
                Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, PlayerMovement.Instance.JumpForce);
            }
        }


        // EXIT STATE
        protected override void ExitState()
        {
            Context.Animator.SetBool(Context.PlayerAnimations.IsAirborneHash, false);
        }


        // UPDATE STATE
        protected override void UpdateState()
        {
            CheckSwitchStates();
            CheckJumpEnd();
        }

        private void CheckJumpEnd()
        {
            if (Context.RigidBody.velocity.y > 0f
                && Context.IsJumpPressed == false)
            {
                StartFalling();
            }
        }

        private void StartFalling()
        {
            Vector2 velocity = Context.RigidBody.velocity;
            Context.RigidBody.velocity = new Vector2(velocity.x, velocity.y * 0.5f);;
            Context.JumpButtonPressedTime = null;
            Context.LastGroundedTime = null;
        }


        // INITIALIZE SUB STATE
        protected override void InitializeSubState()
        {
        }
    

        // CHECK SWITCH STATES
        protected override void CheckSwitchStates()
        {
            if (Context.IsJumpPressed == false)
            {
                SwitchState(Factory.Grounded());
            }
        }
    }
}

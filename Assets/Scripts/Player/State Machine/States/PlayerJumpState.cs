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
            PlayerAdapter.SetAnimatorBool(PlayerAdapter.PlayerAnimations.IsAirborneHash, true);
            JumpStart();
        }

        private void JumpStart()
        {
            if (PlayerAdapter.IsGrounded() == false)
            {
                return;
            }

            PlayerAdapter.ResetJumpButtonPressedTime();

            bool isCoyoteTime = Time.time - PlayerAdapter.GetLastGroundedTime() <= Context.PlayerAdapter.PlayerMovement.CoyoteTime;
            bool isJumpBuffered = Time.time - PlayerAdapter.PlayerController.JumpButtonPressedTime <= Context.PlayerAdapter.PlayerMovement.CoyoteTime;

            if (isCoyoteTime && isJumpBuffered)
            {
                PlayerAdapter.RigidBody.velocity = new Vector2(Context.PlayerAdapter.RigidBody.velocity.x, Context.PlayerAdapter.PlayerMovement.JumpForce);
            }
        }


        // EXIT STATE
        protected override void ExitState()
        {
            Context.PlayerAdapter.Animator.SetBool(Context.PlayerAdapter.PlayerAnimations.IsAirborneHash, false);
        }


        // UPDATE STATE
        protected override void UpdateState()
        {
            CheckSwitchStates();
            CheckJumpEnd();
        }

        private void CheckJumpEnd()
        {
            if (Context.PlayerAdapter.RigidBody.velocity.y > 0f
                && Context.PlayerAdapter.PlayerController.IsJumpPressed == false)
            {
                StartFalling();
            }
        }

        private void StartFalling()
        {
            Vector2 velocity = Context.PlayerAdapter.RigidBody.velocity;
            Context.PlayerAdapter.RigidBody.velocity = new Vector2(velocity.x, velocity.y * 0.5f);;
            Context.PlayerAdapter.PlayerController.JumpButtonPressedTime = null;
            Context.PlayerAdapter.PlayerMovement.LastGroundedTime = null;
        }


        // INITIALIZE SUB STATE
        protected override void InitializeSubState()
        {
        }
    

        // CHECK SWITCH STATES
        protected override void CheckSwitchStates()
        {
            if (Context.PlayerAdapter.PlayerController.IsJumpPressed == false)
            {
                SwitchState(Factory.Grounded());
            }
        }
    }
}

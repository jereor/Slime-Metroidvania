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

            bool isCoyoteTime = Time.time - PlayerAdapter.GetLastGroundedTime() <= PlayerAdapter.PlayerMovement.CoyoteTime;
            bool isJumpBuffered = Time.time - PlayerAdapter.GetJumpButtonPressedTime() <= PlayerAdapter.PlayerMovement.CoyoteTime;

            if (isCoyoteTime && isJumpBuffered)
            {
                PlayerAdapter.RigidBody.velocity = new Vector2(PlayerAdapter.RigidBody.velocity.x, PlayerAdapter.PlayerMovement.JumpForce);
            }
        }


        // EXIT STATE
        protected override void ExitState()
        {
            PlayerAdapter.SetAnimatorBool(PlayerAdapter.PlayerAnimations.IsAirborneHash, false);
        }


        // UPDATE STATE
        protected override void UpdateState()
        {
            CheckSwitchStates();
            CheckJumpEnd();
        }

        private void CheckJumpEnd()
        {
            if (PlayerAdapter.RigidBody.velocity.y > 0f
                && PlayerAdapter.IsJumpPressed() == false)
            {
                StartFalling();
            }
        }

        private void StartFalling()
        {
            Vector2 velocity = PlayerAdapter.RigidBody.velocity;
            PlayerAdapter.RigidBody.velocity = new Vector2(velocity.x, velocity.y * 0.5f);;
            PlayerAdapter.ResetJumpVariables();
        }


        // INITIALIZE SUB STATE
        protected override void InitializeSubState()
        {
        }
    

        // CHECK SWITCH STATES
        protected override void CheckSwitchStates()
        {
            if (PlayerAdapter.IsJumpPressed() == false)
            {
                SwitchState(Factory.Grounded());
            }
        }
    }
}

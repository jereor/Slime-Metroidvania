using GameFramework.ComponentSystem;
using UnityEngine;

namespace Player.Core_Components
{
    public class PlayerMovement : Movement
    {
        [Header("Data")]
        [SerializeField] private float _coyoteTime;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundCheckRadius;
        
        [Header("Dependencies")]
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;

        public float? LastGroundedTime { get; set; }
        public bool IsFalling { get; private set; }
        public bool IsJumping { get; private set; }
        public bool IsAtJumpPeak { get; private set; }

        public float CoyoteTime
        {
            get
            {
                return _coyoteTime;
            }
        }

        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            HandleMovement();
            HandleJumping();
            HandleFalling();
        }

        private void HandleMovement()
        {
            if (_playerController.IsMovementInputPressed == false)
            {
                StopMovement();
                return;
            }

            _rigidBody.velocity = 
                new Vector2(x: _playerController.CurrentMovementInput * _moveSpeed, 
                    y: CurrentVelocity.y);
        }

        private void HandleJumping()
        {
            if (CurrentVelocity.y > 0f)
            {
                IsJumping = true;
            }
            else
            {
                IsJumping = false;
            }
            
            if (IsJumping)
            {
                CheckJumpEnd();
            }
        }
        
        private void HandleFalling()
        {
            if (CurrentVelocity.y < 0f)
            {
                IsFalling = true;
            }
            else
            {
                IsFalling = false;
            }
        }

        private void StopMovement()
        {
            _rigidBody.velocity = new Vector2(x: 0, y: CurrentVelocity.y);
        }
        
        public void JumpStart()
        {
            if (IsGrounded() == false)
            {
                return;
            }

            bool isCoyoteTime = Time.time - LastGroundedTime <= _coyoteTime;
            bool isJumpBuffered = Time.time - _playerController.JumpInputPressedTime <= _coyoteTime;

            if (isCoyoteTime && isJumpBuffered)
            {
                _rigidBody.velocity = new Vector2(CurrentVelocity.x, _jumpForce);
            }
        }

        private void CheckJumpEnd()
        {
            if (CurrentVelocity.y < 0f && IsAtJumpPeak == false && IsFalling == false)
            {
                IsAtJumpPeak = true;
                StartFalling();
                ResetJumpVariables();
            }
            
            bool jumpingButJumpReleased = CurrentVelocity.y > 0f
                                          && _playerController.IsJumpInputPressed == false 
                                          && IsFalling == false;
            if (jumpingButJumpReleased)
            {
                StartFalling();
                ResetJumpVariables();
            }
            
            bool fallingAndHitGround = CurrentVelocity.y < 0f && IsGrounded();
            if (fallingAndHitGround)
            {
                ResetJumpVariables();
            }
        }

        private void StartFalling()
        {
            _rigidBody.velocity = new Vector2(CurrentVelocity.x, CurrentVelocity.y * 0.5f);
        }

        private void ResetJumpVariables()
        {
            LastGroundedTime = null;
            _playerController.JumpInputPressedTime = null;
            IsAtJumpPeak = false;
        }

        public void SetLastGroundedTime()
        {
            LastGroundedTime = Time.time;
        }
        
    }
}

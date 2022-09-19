using GameFramework.ComponentSystem;
using GameFramework.Constants;
using UnityEngine;

namespace Player.Core_Components
{
    public class PlayerMovement : Movement
    {
        [Header("Data")]
        [SerializeField] private float _coyoteTime;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _knockbackSpeed;
        
        [Header("Dependencies")]
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _groundCheck;
        
        private Vector2 _velocityWorkspace;

        public float? LastGroundedTime { get; set; }
        public bool IsFalling { get; private set; }
        public bool IsJumping { get; private set; }
        public bool IsAtJumpPeak { get; private set; }
        public bool HasHitJumpPeak { get; private set; }
        public bool IsKnockedBack { get; private set; }

        public float CoyoteTime
        {
            get
            {
                return _coyoteTime;
            }
        }

        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(
                _groundCheck.position, 
                _groundCheckRadius, 
                1<<PhysicsConstants.GROUND_LAYER_NUMBER);
        }

        private bool IsFallingAndHitGround()
        {
            return CurrentVelocity.y < 0f 
                   && IsGrounded();
        }
        
        public void SetLastGroundedTime()
        {
            LastGroundedTime = Time.time;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsKnockedBack)
            {
                CheckForKnockbackEnd();
                return;
            }
            
            HandleMovement();
            HandleJumping();
            HandleFalling();
        }

        private void HandleMovement()
        {
            if (_playerController.IsMovementInputPressed == false)
            {
                _rigidBody.velocity = new Vector2(x: 0, y: CurrentVelocity.y);
                return;
            }

            _rigidBody.velocity = 
                new Vector2(x: _playerController.CurrentMovementInput * _moveSpeed, 
                    y: CurrentVelocity.y);
        }

        private void HandleJumping()
        {
            IsJumping = CurrentVelocity.y > 0f;
            IsAtJumpPeak = IsFalling && !HasHitJumpPeak;
            
            if (IsJumping || IsAtJumpPeak)
            {
                CheckForJumpEnd();
            }

            if (IsAtJumpPeak && !HasHitJumpPeak)
            {
                Invoke(nameof(ExitJumpPeak), 1f);
            }
        }

        private void ExitJumpPeak()
        {
            StartFalling();
            ResetJumpVariables();
            HasHitJumpPeak = true;
            IsAtJumpPeak = false;
        }

        private void HandleFalling()
        {
            IsFalling = CurrentVelocity.y < 0f 
                        && IsAtJumpPeak == false;

            if (IsFalling)
            {
                if (IsFallingAndHitGround())
                {
                    ResetJumpVariables();
                }
            }
        }

        private void CheckForKnockbackEnd()
        {
            if (IsFallingAndHitGround())
            {
                IsKnockedBack = false;
            }
        }

        public void JumpStart()
        {
            bool isCoyoteTime = Time.time - LastGroundedTime <= _coyoteTime;
            bool isJumpBuffered = Time.time - _playerController.JumpInputPressedTime <= _coyoteTime;

            if (isCoyoteTime && isJumpBuffered)
            {
                _rigidBody.velocity = new Vector2(CurrentVelocity.x, _jumpForce);
            }
        }

        private void CheckForJumpEnd()
        {
            bool jumpingButJumpReleased = CurrentVelocity.y > 0f
                                          && _playerController.IsJumpInputPressed == false;
            if (jumpingButJumpReleased)
            {
                StartFalling();
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
            HasHitJumpPeak = false;
        }

        public void DamageKnockback(int knockbackDirection)
        {
            IsKnockedBack = true;
            _velocityWorkspace.Set(CurrentVelocity.x + _knockbackSpeed / 2 * knockbackDirection, _knockbackSpeed);
            _rigidBody.velocity = _velocityWorkspace;
        }
        
    }
}

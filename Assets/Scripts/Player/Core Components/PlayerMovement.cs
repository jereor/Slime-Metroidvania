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
        [SerializeField] private float _knockbackSpeed;
        
        [Header("Dependencies")]
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;
        
        private Vector2 _velocityWorkspace;

        public float? LastGroundedTime { get; set; }
        public bool IsFalling { get; private set; }
        public bool IsJumping { get; private set; }
        public bool IsAtJumpPeak { get; private set; }
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
            return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsGrounded())
            {
                HandleMovement();
                HandleJumping();
            }

            if (IsKnockedBack)
            {
                CheckKnockbackEnd();   
            }
            
            CheckForFalling();
        }

        private void HandleMovement()
        {
            if (IsKnockedBack)
            {
                return;
            }
            
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
            if (IsKnockedBack)
            {
                return;
            }
            
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
                HandleJumpPeak();
            }
        }

        private void HandleJumpPeak()
        {
            if (CurrentVelocity.y < 0f && IsAtJumpPeak == false && IsFalling == false)
            {
                IsAtJumpPeak = true;
                StartFalling();
            }
            else
            {
                IsAtJumpPeak = false;
            }
        }
        
        private void CheckForFalling()
        {
            if (IsKnockedBack)
            {
                return;
            }
            
            if (CurrentVelocity.y < 0f && IsAtJumpPeak == false)
            {
                IsFalling = true;
            }
            else
            {
                IsFalling = false;
            }
        }

        private void CheckKnockbackEnd()
        {
            bool fallingAndHitGround = CurrentVelocity.y < 0f && IsGrounded();
            if (fallingAndHitGround)
            {
                IsKnockedBack = false;
            }
        }

        public void JumpStart()
        {
            if (IsGrounded() == false)
            {
                return;
            }
            
            if (IsKnockedBack)
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
            if (IsJumping == false)
            {
                return;
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
        
        public virtual void DamageKnockback(int knockbackDirection)
        {
            IsKnockedBack = true;
            _velocityWorkspace.Set(CurrentVelocity.x + _knockbackSpeed / 2 * knockbackDirection, _knockbackSpeed);
            _rigidBody.velocity = _velocityWorkspace;
        }
        
    }
}

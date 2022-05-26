using Player.Core.Parameters;
using Player.Data;
using UnityEngine;

namespace Player.Core.Modules
{
    public class PlayerMovement
    {
        // Data
        private readonly float _coyoteTime;
        private readonly float _jumpForce;
        private readonly float _moveSpeed;
        private readonly float _groundCheckRadius;
        
        // Dependencies
        private readonly PlayerController _playerController;
        private readonly Rigidbody2D _rigidBody;
        private readonly Transform _groundCheck;
        private readonly LayerMask _groundLayer;

        private Vector2 _currentVelocity;
        private bool _isJumping;

        public float? LastGroundedTime { get; set; }
        public bool IsFalling { get; set; }

        public PlayerMovement(D_PlayerMovement playerMovementData, PlayerMovementParameters playerMovementParameters)
        {
            _coyoteTime = playerMovementData._coyoteTime;
            _jumpForce = playerMovementData._jumpForce;
            _moveSpeed = playerMovementData._moveSpeed;
            _groundCheckRadius = playerMovementData._groundCheckRadius;

            _playerController = playerMovementParameters.PlayerController;
            _rigidBody = playerMovementParameters.Rigidbody;
            _groundCheck = playerMovementParameters.GroundCheck;
            _groundLayer = playerMovementParameters.GroundLayer;
        }
        
        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }

        public void HandleMovement()
        {
            _currentVelocity = _rigidBody.velocity;

            if (_playerController.IsMovementPressed == false)
            {
                StopMovement();
                return;
            }

            _rigidBody.velocity = 
                new Vector2(x: _playerController.CurrentMovementInput * _moveSpeed, 
                    y: _currentVelocity.y);
        }

        private void StopMovement()
        {
            _rigidBody.velocity = new Vector2(x: 0, y: _currentVelocity.y);
        }
        
        public void JumpStart()
        {
            if (IsGrounded() == false)
            {
                return;
            }

            _isJumping = true;

            bool isCoyoteTime = Time.time - LastGroundedTime <= _coyoteTime;
            bool isJumpBuffered = Time.time - _playerController.JumpButtonPressedTime <= _coyoteTime;

            if (isCoyoteTime && isJumpBuffered)
            {
                IsFalling = false;
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
            }
        }

        public void JumpEnd()
        {
            _isJumping = false;
        }
        
        public void Update()
        {
            if (_isJumping == false)
            {
                return;
            }
            
            CheckJumpEnd();
        }

        private void CheckJumpEnd()
        {
            if (_rigidBody.velocity.y > 0f
                && _playerController.IsJumpPressed == false)
            {
                Debug.Log("Released");
                StartFalling();
            }
            if (_rigidBody.velocity.y < 0f && IsGrounded())
            {
                Debug.Log("Hit ground");
                StartFalling();
            }
        }

        private void StartFalling()
        {
            IsFalling = true;
            
            Vector2 velocity = _rigidBody.velocity;
            _rigidBody.velocity = new Vector2(velocity.x, velocity.y * 0.5f);;
            ResetJumpVariables();
        }

        private void ResetJumpVariables()
        {
            LastGroundedTime = null;
            _playerController.JumpButtonPressedTime = null;
        }
    }
}

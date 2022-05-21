using Player.Data;
using UnityEngine;

namespace Player.Core
{
    public class PlayerMovement
    {
        private readonly PlayerAdapter _playerAdapter;
        private readonly float _coyoteTime;
        private readonly float _jumpForce;
        private readonly float _moveSpeed;
        private readonly float _groundCheckRadius;
        private readonly Transform _groundCheck;
        private readonly LayerMask _groundLayer;

        private Vector2 _currentVelocity;

        public float CoyoteTime
        {
            get { return _coyoteTime; }
        }

        public float JumpForce
        {
            get { return _jumpForce; }
        }
        
        public PlayerMovement(PlayerAdapter playerAdapter, D_PlayerMovement playerMovementData, PlayerMovementParameters playerMovementParameters)
        {
            _playerAdapter = playerAdapter;
            
            _coyoteTime = playerMovementData._coyoteTime;
            _jumpForce = playerMovementData._jumpForce;
            _moveSpeed = playerMovementData._moveSpeed;
            _groundCheckRadius = playerMovementData._groundCheckRadius;

            _groundCheck = playerMovementParameters.GroundCheck;
            _groundLayer = playerMovementParameters.GroundLayer;
        }
        
        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }

        public void HandleMovement()
        {
            _currentVelocity = _playerAdapter.RigidBody.velocity;

            if (_playerAdapter.PlayerController.IsMovementPressed == false)
            {
                StopMovement();
                return;
            }

            _playerAdapter.RigidBody.velocity = 
                new Vector2(x: _playerAdapter.PlayerController.CurrentMovementInput * _moveSpeed, 
                    y: _currentVelocity.y);
        }

        private void StopMovement()
        {
            _playerAdapter.RigidBody.velocity = new Vector2(x: 0, y: _currentVelocity.y);
        }
    }
}

using Player.Data;
using UnityEngine;

namespace Player.Core
{
    public class PlayerMovement
    {
        public readonly float CoyoteTime;
        public readonly float JumpForce;
        public readonly float MoveSpeed;
        public readonly float GroundCheckRadius;
        
        private readonly Transform _groundCheck;
        private readonly LayerMask _groundLayer;
        
        private readonly PlayerAdapter _playerAdapter;
        private Vector2 _currentVelocity;

        public PlayerMovement(PlayerAdapter playerAdapter, D_PlayerMovement playerMovementData, PlayerMovementParameters playerMovementParameters)
        {
            _playerAdapter = playerAdapter;
            
            CoyoteTime = playerMovementData._coyoteTime;
            JumpForce = playerMovementData._jumpForce;
            MoveSpeed = playerMovementData._moveSpeed;
            GroundCheckRadius = playerMovementData._groundCheckRadius;

            _groundCheck = playerMovementParameters.GroundCheck;
            _groundLayer = playerMovementParameters.GroundLayer;
        }
        
        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundCheck.position, GroundCheckRadius, _groundLayer);
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
                new Vector2(x: _playerAdapter.PlayerController.CurrentMovementInput * MoveSpeed, 
                    y: _currentVelocity.y);
        }

        private void StopMovement()
        {
            _playerAdapter.RigidBody.velocity = new Vector2(x: 0, y: _currentVelocity.y);
        }
    }

    public struct PlayerMovementParameters
    {
        public Transform GroundCheck;
        public LayerMask GroundLayer;
    }
}

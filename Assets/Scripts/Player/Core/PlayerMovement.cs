using Player.Data;
using Player.State_Machine;
using UnityEngine;

namespace Player.Core
{
    public class PlayerMovement
    {
        public float CoyoteTime = 0.2f;
        public float JumpForce = 8;
        public float MoveSpeed = 8;
        
        private readonly PlayerAdapter _playerAdapter;
        private Vector2 _currentVelocity;

        public PlayerMovement(PlayerAdapter playerAdapter, D_PlayerMovement playerMovementData)
        {
            _playerAdapter = playerAdapter;
            CoyoteTime = playerMovementData.CoyoteTime;
            JumpForce = playerMovementData.JumpForce;
            MoveSpeed = playerMovementData.MoveSpeed;
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
}

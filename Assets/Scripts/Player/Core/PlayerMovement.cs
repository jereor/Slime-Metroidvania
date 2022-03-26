using Player.State_Machine;
using UnityEngine;

namespace Player.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement variables")]
        [SerializeField] private float moveSpeed;

        private PlayerStateMachine _context;
        private Vector2 _currentVelocity;

        public static PlayerMovement Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void HandleMovement(PlayerStateMachine currentContext)
        {
            _context = currentContext;
            _currentVelocity = _context.RigidBody.velocity;

            if (_context.IsMovementPressed == false)
            {
                StopMovement();
                return;
            }

            _context.RigidBody.velocity = 
                new Vector2(x: _context.CurrentMovementInput * moveSpeed, 
                    y: _currentVelocity.y);
        }

        private void StopMovement()
        {
            _context.RigidBody.velocity = new Vector2(x: 0, y: _currentVelocity.y);
        }
    }
}

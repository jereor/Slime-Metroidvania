using Player.State_Machine;
using UnityEngine;

namespace Player.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        // TODO: Experiment with making movement variables into a configuration class, struct or scriptable object
        [Header("Movement variables")]
        [SerializeField] private float _moveSpeed;
        
        [Header("Jump variables")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _coyoteTime;

        public static PlayerMovement Instance;
        
        private PlayerStateMachine _context;
        private Vector2 _currentVelocity;

        public float JumpForce
        {
            get { return _jumpForce; }
        }
        public float CoyoteTime
        {
            get { return _coyoteTime; }
        }

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
                new Vector2(x: _context.CurrentMovementInput * _moveSpeed, 
                    y: _currentVelocity.y);
        }

        private void StopMovement()
        {
            _context.RigidBody.velocity = new Vector2(x: 0, y: _currentVelocity.y);
        }
    }
}

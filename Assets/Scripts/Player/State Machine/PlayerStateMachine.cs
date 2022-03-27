using Player.Core;
using Player.Core.Slime_Sling;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.State_Machine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [Header("Jump variables")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _coyoteTime;

        [Header("References")] 
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerMask _groundLayer;

        // References
        public Rigidbody2D RigidBody
        {
            get { return _rigidbody2D; }
        }
        public Animator Animator
        {
            get { return _animator; }
        }
        private PlayerControls _playerControls;

        // Grounded
        public float? LastGroundedTime { get; set; }
        private const float GROUND_CHECK_RADIUS = 0.3f;

        // Jump
        public float JumpForce
        {
            get { return _jumpForce; }
        }
        public float CoyoteTime
        {
            get { return _coyoteTime; }
        }
        public bool IsJumpPressed { get; private set; }
        public float? JumpButtonPressedTime { get; set; }

        // Movement
        public bool IsFacingRight { get; set; } = true;
        public bool IsMovementPressed { get; private set; }
        public float CurrentMovementInput { get; private set; }

        // Animator hashes
        public int IsMovingHash { get; private set; }
        public int IsAirborneHash { get; private set; }

        // States
        public PlayerBaseState CurrentState
        {
            set { _currentState = value; }
        }
        private PlayerBaseState _currentState;
        private PlayerStateFactory _states;

        private void Awake()
        {
            _states = new PlayerStateFactory(this);
            _currentState = _states.Grounded();
            _currentState.EnterState();

            IsMovingHash = Animator.StringToHash("isMoving");
            IsAirborneHash = Animator.StringToHash("isAirborne");

            SubscribePlayerInputs();
        }

        private void SubscribePlayerInputs()
        {
            _playerControls = new PlayerControls();
            _playerControls.Surface.Move.started += OnMovementInput;
            _playerControls.Surface.Move.canceled += OnMovementInput;
            _playerControls.Surface.Move.performed += OnMovementInput;
            _playerControls.Surface.Jump.started += OnJumpInput;
            _playerControls.Surface.Jump.canceled += OnJumpInput;
            _playerControls.Surface.ShootSling.started += OnShootSlingInput;
            _playerControls.Surface.ShootSling.canceled += OnShootSlingInput;
        }

        private void Update()
        {
            _currentState.UpdateStates();
        }

        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundCheck.position, GROUND_CHECK_RADIUS, _groundLayer);
        }

        public bool HasMoveDirectionChanged()
        {
            bool facingRightButNowMovingLeft = IsFacingRight && CurrentMovementInput < 0f;
            bool facingLeftButNowMovingRight = !IsFacingRight && CurrentMovementInput > 0f;

            return facingRightButNowMovingLeft
                   || facingLeftButNowMovingRight;
        }

        private void OnMovementInput(InputAction.CallbackContext context)
        {
            CurrentMovementInput = context.ReadValue<float>();
            IsMovementPressed = CurrentMovementInput != 0;
            PlayerFlipper.Instance.HandleDirectionChange(this);
        }

        private void OnJumpInput(InputAction.CallbackContext context)
        {
            IsJumpPressed = context.ReadValueAsButton();
        }

        private static void OnShootSlingInput(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                Debug.Log("Pull canceled!");
                SlingShooter.Instance.CancelPull();
                return;
            }

            if (context.started)
            {
                SlingShooter.Instance.SetGrapplePoint();
            }

            SlingShooter.Instance.StartPull();
        }


        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }
    }
}
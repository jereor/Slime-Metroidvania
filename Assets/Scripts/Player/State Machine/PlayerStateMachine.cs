using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _coyoteTime;

    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask GROUND_LAYER;

    private PlayerControls _playerControls;

    private const float GROUND_CHECK_RADIUS = 0.3f;

    private float? _jumpButtonPressedTime;
    private float? _lastGroundedTime;

    private bool _isFacingRight = true;
    private bool _isJumpPressed;
    private bool _isMovementPressed;

    private float _currentMovementInput;
    private bool _hasMoveDirectionChanged
    {
        get
        {
            bool facingRightButNowMovingLeft = _isFacingRight && _currentMovementInput < 0f;
            bool facingLeftButNowMovingRight = !_isFacingRight && _currentMovementInput > 0f;

            return facingRightButNowMovingLeft
                || facingLeftButNowMovingRight;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return Physics2D.OverlapCircle(_groundCheck.position, GROUND_CHECK_RADIUS, GROUND_LAYER);
        }
    }

    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    // GETTERS AND SETTERS
    public PlayerBaseState CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
        }
    }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float JumpForce { get { return _jumpForce; } }
    public float CoyoteTime { get { return _coyoteTime; } }
    public Rigidbody2D RigidBody { get { return _rigidbody2D; } set { _rigidbody2D = value; } }
    public Transform GroundCheck { get { return _groundCheck; } }
    public Animator Animator { get { return _animator; } }
    public LayerMask GroundLayer { get { return GROUND_LAYER; } }
    public float GroundCheckRadius { get { return GROUND_CHECK_RADIUS; } }
    public float? JumpButtonPressedTime { get { return _jumpButtonPressedTime; } set { _jumpButtonPressedTime = value; } }
    public float? LastGroundedTime { get { return _lastGroundedTime; } }
    public bool IsFacingRight { get { return _isFacingRight; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool HasMoveDirectionChanged { get { return _hasMoveDirectionChanged; } }

    public bool IsMovementPressed { get { return _isMovementPressed; } }

    private void Awake()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _playerControls = new PlayerControls();
        _playerControls.Surface.Move.started += OnMovementInput;
        _playerControls.Surface.Move.canceled += OnMovementInput;
        _playerControls.Surface.Move.performed += OnMovementInput;
        _playerControls.Surface.Jump.started += OnJumpInput;
        _playerControls.Surface.Jump.canceled += OnJumpInput;
    }

    private void Update()
    {
        _currentState.UpdateStates();
        HandleMovement();
        HandleDirectionChange();
    }

    // TODO: Move to separate file
    private void HandleMovement()
    {
        if (_currentMovementInput == 0)
        {
            Stop();
            return;
        }

        _rigidbody2D.velocity =
                new Vector2(x: _currentMovementInput * _moveSpeed, y: _rigidbody2D.velocity.y);
    }

    private void Stop()
    {
        _rigidbody2D.velocity = new Vector2(x: 0, y: _rigidbody2D.velocity.y);
    }

    // TODO: Move to separate file
    private void HandleDirectionChange()
    {
        if (_hasMoveDirectionChanged)
        {
            FlipSprite();
            FlipSlingShooter();
        }
    }

    private void FlipSprite()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private static void FlipSlingShooter()
    {
        Vector3 slingShooterLocalScale = SlingShooter.Instance.transform.localScale;
        slingShooterLocalScale.x *= -1f;
        SlingShooter.Instance.transform.localScale = slingShooterLocalScale;
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<float>();
        _isMovementPressed = _currentMovementInput != 0;
    }

    void OnJumpInput(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }

    void OnShootSlingInput(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("Sling: Canceled!");
            SlingShooter.Instance.CancelPull();
            return;
        }

        if (context.started)
        {
            Debug.Log("Sling: Started!");
            SlingShooter.Instance.SetGrapplePoint();
        }

        if (context.performed == false)
        {
            return;
        }

        SlingShooter.Instance.StartPull();
    }


    void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
}

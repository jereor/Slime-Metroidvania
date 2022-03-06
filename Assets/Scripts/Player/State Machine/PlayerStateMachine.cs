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

    public static float HorizontalInput;

    private const float GROUND_CHECK_RADIUS = 0.3f;

    private float? _jumpButtonPressedTime;
    private float? _lastGroundedTime;

    private bool _isFacingRight = true;
    private bool _isjumpPressed;

    int _isWalkingHash;
    int _isRunningHash;
    int _isJumpingHash;

    private bool HasMoveDirectionChanged
    {
        get
        {
            bool facingRightButNowMovingLeft = _isFacingRight && PlayerController.HorizontalInput < 0f;
            bool facingLeftButNowMovingRight = !_isFacingRight && PlayerController.HorizontalInput > 0f;

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
    public bool IsJumpPressed
    {
        get
        {
            return _isjumpPressed;
        }
    }

    private void Awake()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
    }

    private void Update()
    {
        HandleMovement();
        HandleDirectionChange();
        GroundCheckUpdate();
    }

    private void HandleMovement()
    {
        if (PlayerController.HorizontalInput == 0)
        {
            Stop();
            return;
        }

        _rigidbody2D.velocity =
            new Vector2(x: PlayerController.HorizontalInput * _moveSpeed, y: _rigidbody2D.velocity.y);
    }

    private void Stop()
    {
        _rigidbody2D.velocity = new Vector2(x: 0, y: _rigidbody2D.velocity.y);
    }

    private void HandleDirectionChange()
    {
        if (HasMoveDirectionChanged)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

        Vector3 slingShooterLocalScale = SlingShooter.Instance.transform.localScale;
        slingShooterLocalScale.x *= -1f;
        SlingShooter.Instance.transform.localScale = slingShooterLocalScale;
    }

    private void GroundCheckUpdate()
    {
        if (IsGrounded)
        {
            _lastGroundedTime = Time.time;
        }
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        HorizontalInput = context.ReadValue<float>();

        if (HorizontalInput == 0)
        {
            // TODO: Fix playerStateInstance reference if needed
            //_playerStateInstance.SetToIdle();
            _animator.SetBool(AnimatorConstants.IS_MOVING, false);
            _animator.SetBool(AnimatorConstants.IS_IDLE, true);
            return;
        }

        // TODO: Fix playerStateInstance reference if needed
        //_playerStateInstance.SetToMoving();
        _animator.SetBool(AnimatorConstants.IS_IDLE, false);
        _animator.SetBool(AnimatorConstants.IS_MOVING, true);
    }

    void OnJumpInput(InputAction.CallbackContext context)
    {
        _isjumpPressed = context.ReadValueAsButton();

        if (context.canceled)
        {
            PlayerMovement.Instance.JumpRelease();
            return;
        }

        if (context.performed == false)
        { 
            return;
        }

        if (_animator.GetBool(AnimatorConstants.IS_JUMPING))
        {
            return;
        }

        _animator.SetBool(AnimatorConstants.IS_JUMPING, true);
        PlayerMovement.Instance.Jump();
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
        _playerInput.currentActionMap.Enable();
    }

    void OnDisable()
    {
        _playerInput.currentActionMap.Disable();
    }
}

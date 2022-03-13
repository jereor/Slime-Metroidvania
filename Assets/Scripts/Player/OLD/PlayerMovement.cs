using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [Header("Movement variables")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _coyoteTime;

    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask GROUND_LAYER;

    private const float GROUND_CHECK_RADIUS = 0.3f;

    private float? _jumpButtonPressedTime;
    private float? _lastGroundedTime;

    private bool _isFacingRight = true;

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_groundCheck.position, GROUND_CHECK_RADIUS);
    }

    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this; 
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleDirectionChange();
        GroundCheckUpdate();
    }

    private void GroundCheckUpdate()
    {
        if (IsGrounded)
        {
            _lastGroundedTime = Time.time;
        }
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

    internal void Jump()
    {
        _jumpButtonPressedTime = Time.time;

        bool isCoyoteTime = Time.time - _lastGroundedTime <= _coyoteTime;
        bool isJumpBuffered = Time.time - _jumpButtonPressedTime <= _coyoteTime;

        if (isCoyoteTime && isJumpBuffered)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
        }
    }

    internal void JumpRelease()
    {
        if (_rigidbody2D.velocity.y > 0f)
        {
            StartFalling();
        }
    }

    private void StartFalling()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.5f);
        _jumpButtonPressedTime = null;
        _lastGroundedTime = null;
    }
}
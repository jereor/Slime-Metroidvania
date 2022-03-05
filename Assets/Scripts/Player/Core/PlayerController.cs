using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public static float HorizontalInput;
    public PlayerInput PlayerInput => _playerInput;

    [Header("References")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;

    private static PlayerState _playerStateInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _playerStateInstance = PlayerState.Instance;
    }

    private void FixedUpdate()
    {
        if (PlayerMovement.Instance.IsGrounded)
        {
            _animator.SetBool(AnimatorConstants.IS_JUMPING, false);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        HorizontalInput = context.ReadValue<float>();

        if (HorizontalInput == 0)
        {
            _playerStateInstance.SetToIdle();
            _animator.SetBool(AnimatorConstants.IS_MOVING, false);
            _animator.SetBool(AnimatorConstants.IS_IDLE, true);
            return;
        }

        _playerStateInstance.SetToMoving();
        _animator.SetBool(AnimatorConstants.IS_IDLE, false);
        _animator.SetBool(AnimatorConstants.IS_MOVING, true);
    }

    public void Jump(InputAction.CallbackContext context)
    {
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

    public void ShootSling(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SlingShooter.Instance.SetGrapplePoint();
            return;
        }

        if (context.performed == false)
        {
            return;
        }

        if (context.canceled)
        {
            SlingShooter.Instance.CancelPull();
            return;
        }

        SlingShooter.Instance.StartPull();
    }
}

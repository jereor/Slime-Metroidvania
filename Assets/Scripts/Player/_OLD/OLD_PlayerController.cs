using UnityEngine;
using UnityEngine.InputSystem;

public class OLD_PlayerController : MonoBehaviour
{
    public static OLD_PlayerController Instance;

    public static float HorizontalInput;
    public PlayerInput PlayerInput => _playerInput;

    [Header("References")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;

    private static OLD_PlayerState _playerStateInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _playerStateInstance = OLD_PlayerState.Instance;
    }

    private void FixedUpdate()
    {
        if (OLD_PlayerMovement.Instance.IsGrounded)
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
            OLD_PlayerMovement.Instance.JumpRelease();
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
        OLD_PlayerMovement.Instance.Jump();
    }

    public void ShootSling(InputAction.CallbackContext context)
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
}

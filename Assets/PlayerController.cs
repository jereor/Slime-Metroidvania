using Player.Core;
using Player.Core.Slime_Sling;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerAdapter _playerAdapter;
    
    private PlayerControls _playerControls;
    
    // Movement
    public bool IsMovementPressed { get; private set; }
    public float CurrentMovementInput { get; private set; }
    
    // Jump
    public bool IsJumpPressed { get; private set; }
    public float? JumpButtonPressedTime { get; set; }

    private void Awake()
    {
        SubscribePlayerInputs();
    }

    private void SubscribePlayerInputs()
    {
        _playerControls = new PlayerControls();
            
        _playerControls.Gameplay.Move.performed += OnMovementInput;
        _playerControls.Gameplay.Move.canceled += OnMovementInput;
            
        _playerControls.Gameplay.Jump.performed += OnJumpInput;
        _playerControls.Gameplay.Jump.canceled += OnJumpInput;
            
        _playerControls.Gameplay.ShootSling.started += OnShootSlingInputStart;
        _playerControls.Gameplay.ShootSling.canceled += OnShootSlingInputCancel;
            
        _playerControls.Gameplay.MeleeAttack.started += OnMeleeAttackInputStart;
    }
    
    public bool HasMoveDirectionChanged()
    {
        bool isFacingRight = _playerAdapter.PlayerFlipper.IsFacingRight;
        bool facingRightButNowMovingLeft = isFacingRight && CurrentMovementInput < 0f;
        bool facingLeftButNowMovingRight = !isFacingRight && CurrentMovementInput > 0f;

        return facingRightButNowMovingLeft
               || facingLeftButNowMovingRight;
    }
    
    
    // Input events
    private void OnMovementInput(InputAction.CallbackContext context)
    {
        CurrentMovementInput = context.ReadValue<float>();
        IsMovementPressed = CurrentMovementInput != 0;

        if (HasMoveDirectionChanged())
        {
            _playerAdapter.FlipPlayer();
        }
    }
    
    private void OnJumpInput(InputAction.CallbackContext context)
    {
        IsJumpPressed = context.ReadValueAsButton();
    }

    private static void OnShootSlingInputStart(InputAction.CallbackContext context)
    {
        SlingShooter.Instance.SetGrapplePoint();
        SlingShooter.Instance.StartPull();
    }

    private static void OnShootSlingInputCancel(InputAction.CallbackContext context)
    {
        SlingShooter.Instance.CancelPull();
    }

    private void OnMeleeAttackInputStart(InputAction.CallbackContext context)
    {
        _playerAdapter.PlayerCombat.IsMeleeAttacking = true;
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

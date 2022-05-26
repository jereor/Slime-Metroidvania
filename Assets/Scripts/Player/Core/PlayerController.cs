using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Core
{
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
    
        private bool HasMoveDirectionChanged()
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

        private void OnShootSlingInputStart(InputAction.CallbackContext context)
        {
            _playerAdapter.SlingShooter.SetGrapplePoint();
            _playerAdapter.SlingShooter.StartPull();
        }

        private void OnShootSlingInputCancel(InputAction.CallbackContext context)
        {
            _playerAdapter.SlingShooter.CancelPull();
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

        public void SetJumpButtonPressedTime()
        {
            JumpButtonPressedTime = Time.time;
        }
    }
}

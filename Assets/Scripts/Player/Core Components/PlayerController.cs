using UnityEngine;
using UnityEngine.InputSystem;
using Utility.Component_System;

namespace Player.Core_Components
{
    public class PlayerController : Controller
    {
        [SerializeField] private PlayerFlipper _playerFlipper;
        [SerializeField] private SlingShooter _slingShooter;
        [SerializeField] private PlayerCombat _playerCombat;
    
        private PlayerControls _playerControls;
    
        // Movement
        public bool IsMovementPressed { get; private set; }
        public float CurrentMovementInput { get; private set; }
    
        // Jump
        public bool IsJumpPressed { get; private set; }
        public float? JumpButtonPressedTime { get; set; }

        protected override void Awake()
        {
            base.Awake();
            
            SubscribePlayerInputs();
        }

        private void SubscribePlayerInputs()
        {
            _playerControls = new PlayerControls();
            
            _playerControls.Gameplay.Move.performed += OnMovementInput;
            _playerControls.Gameplay.Move.canceled += OnMovementInput;

            _playerControls.Gameplay.Jump.started += OnJumpInputStart;
            _playerControls.Gameplay.Jump.performed += OnJumpInput;
            _playerControls.Gameplay.Jump.canceled += OnJumpInput;
            
            _playerControls.Gameplay.ShootSling.started += OnShootSlingInputStart;
            _playerControls.Gameplay.ShootSling.canceled += OnShootSlingInputCancel;
            
            _playerControls.Gameplay.MeleeAttack.started += OnMeleeAttackInputStart;
        }
    
        private bool HasMoveDirectionChanged()
        {
            bool isFacingRight = _playerFlipper.IsFacingRight;
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
                _playerFlipper.FlipPlayer();
            }
        }
        
        private void OnJumpInputStart(InputAction.CallbackContext context)
        {
            JumpButtonPressedTime = Time.time;
        }
    
        private void OnJumpInput(InputAction.CallbackContext context)
        {
            IsJumpPressed = context.ReadValueAsButton();
        }

        private void OnShootSlingInputStart(InputAction.CallbackContext context)
        {
            _slingShooter.SetGrapplePoint();
            _slingShooter.StartPull();
        }

        private void OnShootSlingInputCancel(InputAction.CallbackContext context)
        {
            _slingShooter.CancelPull();
        }

        private void OnMeleeAttackInputStart(InputAction.CallbackContext context)
        {
            _playerCombat.IsMeleeAttacking = true;
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

    public class Controller : CoreComponent
    {
        
    }
}

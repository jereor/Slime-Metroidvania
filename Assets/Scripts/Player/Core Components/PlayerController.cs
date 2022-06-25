using UnityEngine;
using UnityEngine.InputSystem;
using Utility.Component_System;

namespace Player.Core_Components
{
    public class PlayerController : Controller
    {
        [SerializeField] private SlingShooter _slingShooter;
        [SerializeField] private PlayerCombat _playerCombat;
    
        private PlayerControls _playerControls;

        // Movement
        public bool IsMovementInputPressed { get; private set; }
        public bool IsJumpInputPressed { get; private set; }
        public float? JumpInputPressedTime { get; set; } // TODO: Convert 'set' to private

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

        // Input events
        public void OnMovementInput(InputAction.CallbackContext context)
        {
            CurrentMovementInput = context.ReadValue<float>();
            IsMovementInputPressed = CurrentMovementInput != 0;

            if (HasMoveDirectionChanged())
            {
                _flipper.FlipPlayer();
            }
        }
        
        private void OnJumpInputStart(InputAction.CallbackContext context)
        {
            JumpInputPressedTime = Time.time;
        }
    
        private void OnJumpInput(InputAction.CallbackContext context)
        {
            IsJumpInputPressed = context.ReadValueAsButton();
        }

        private void OnShootSlingInputStart(InputAction.CallbackContext context)
        {
            // TODO: Move these to state machine and use bools instead
            _slingShooter.SetGrapplePoint();
            _slingShooter.StartPull();
        }

        private void OnShootSlingInputCancel(InputAction.CallbackContext context)
        {
            // TODO: Move these to state machine and use bools instead
            _slingShooter.CancelPull();
        }

        private void OnMeleeAttackInputStart(InputAction.CallbackContext context)
        {
            _playerCombat.Initialize();
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

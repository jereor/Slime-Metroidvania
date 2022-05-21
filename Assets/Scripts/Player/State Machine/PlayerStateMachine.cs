using Player.Core;
using Player.Core.Slime_Sling;
using Player.Data;
using Player.State_Machine.States;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace Player.State_Machine
{
    // TODO: Make class smaller, separate functionality to smaller classes
    public class PlayerStateMachine : MonoBehaviour, IStateMachine
    {
        [Header("Data")]
        [SerializeField] private D_PlayerMeleeAttack _playerMeleeAttackData;

        [Header("References")] 
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _meleeAttackHitBox;

        // References
        public PlayerAnimations PlayerAnimations; 
        public Rigidbody2D RigidBody
        {
            get { return _rigidbody2D; }
        }
        public Animator Animator
        {
            get { return _animator; }
        }

        // Grounded
        public float? LastGroundedTime { get; set; }
        private const float GROUND_CHECK_RADIUS = 0.3f;

        // Jump
        public bool IsJumpPressed { get; private set; }
        public float? JumpButtonPressedTime { get; set; }

        // Movement
        public bool IsFacingRight { get; set; } = true;
        public bool IsMovementPressed { get; private set; }
        public float CurrentMovementInput { get; private set; }
        
        // Combat
        public bool IsMeleeAttacking { get; private set; }

        // States
        public PlayerBaseState CurrentState
        {
            set { _currentState = value; }
        }

        private PlayerBaseState _currentState;
        private PlayerStateFactory _states;
        private PlayerControls _playerControls;
        private AttackDetails _attackDetails;

        private void Awake()
        {
            _states = new PlayerStateFactory(this);
            _currentState = _states.Grounded();

            PlayerAnimations = new PlayerAnimations(this);

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
        
        public void FinishMeleeAttacking()
        {
            IsMeleeAttacking = false;
        }

        public void DealDamage()
        {
            Collider2D[] detectedObjects =
                Physics2D.OverlapCircleAll(_meleeAttackHitBox.position, _playerMeleeAttackData._attackRadius,
                    _playerMeleeAttackData._damageableLayers);

            _attackDetails.Position = transform.position;
            _attackDetails.DamageAmount = _playerMeleeAttackData._attackDamage;
            _attackDetails.StunDamageAmount = _playerMeleeAttackData._stunDamage;

            foreach (Collider2D hitObject in detectedObjects)
            {
                hitObject.transform.SendMessageUpwards(EventConstants.DAMAGE, _attackDetails);
            }
        }

        // Input events
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
            IsMeleeAttacking = true;
        }

        // Activation / Deactivation events
        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        // Gizmos
        private void OnDrawGizmos()
        {
            Vector3 attackPosition = _meleeAttackHitBox.position;
            Gizmos.DrawWireSphere(attackPosition, _playerMeleeAttackData._attackRadius);
        }
        
    }
}

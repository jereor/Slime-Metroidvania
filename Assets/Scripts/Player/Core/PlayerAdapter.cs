using Player.Core.Modules;
using Player.Core.Modules.Slime_Sling;
using Player.Core.Parameters;
using Player.Data;
using Player.State_Machine;
using UnityEngine;

namespace Player.Core
{
    [RequireComponent(typeof(PlayerStateMachine))]
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAdapter : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private SlingShooter _slingShooter;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _meleeAttackHitBox;
        [SerializeField] private D_PlayerMeleeAttack _playerMeleeAttackData;
        [SerializeField] private D_PlayerMovement _playerMovementData;
        
        // References
        public PlayerAnimations PlayerAnimations { get; private set; }
        public PlayerCombat PlayerCombat { get; private set; }
        public PlayerFlipper PlayerFlipper { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }

        public PlayerController PlayerController
        {
            get { return _playerController; }
        }

        public SlingShooter SlingShooter
        {
            get { return _slingShooter; }
        }
        
        public Rigidbody2D RigidBody
        {
            get { return _rigidbody2D; }
        }

        public Animator Animator
        {
            get { return _animator; }
        }

        public Transform MeleeAttackHitBox
        {
            get { return _meleeAttackHitBox; }
        }

        public D_PlayerMeleeAttack PlayerMeleeAttackData
        {
            get { return _playerMeleeAttackData; }
        }

        private void Awake()
        {
            Transform playerTransform = gameObject.transform;
            PlayerCombat = new PlayerCombat(_playerMeleeAttackData, new PlayerCombatParameters
            {
                PlayerTransform = playerTransform,
                MeleeAttackHitBox = _meleeAttackHitBox
            });
            
            PlayerAnimations = new PlayerAnimations(new PlayerAnimationsParameters
            {
                PlayerCombat = this.PlayerCombat
            });
            
            PlayerFlipper = new PlayerFlipper(new PlayerFlipperParameters
            {
                PlayerTransform = playerTransform,
                SlingShooterTransform = _slingShooter.gameObject.transform
            });
            
            PlayerMovement = new PlayerMovement(_playerMovementData, new PlayerMovementParameters
            {
                PlayerController = _playerController,
                Rigidbody = _rigidbody2D,
                GroundCheck = _groundCheck,
                GroundLayer = _groundLayer
            });
        }

        public void FlipPlayer()
        {
            PlayerFlipper.FlipPlayer();
        }

        public void HandleMovement()
        {
            PlayerMovement.HandleMovement();
        }

        public void ResetLastGroundedTime()
        {
            PlayerMovement.LastGroundedTime = Time.time;
        }
        
        public void ResetJumpButtonPressedTime()
        {
            _playerController.JumpButtonPressedTime = Time.time;
        }
        
        public void ResetJumpVariables()
        {
            PlayerMovement.LastGroundedTime = null;
            PlayerController.JumpButtonPressedTime = null;
        }

        public bool IsMeleeAttacking()
        {
            if (PlayerCombat == null)
            {
                return false;
            }
                
            return PlayerCombat.IsMeleeAttacking;
        }

        public bool IsMovementPressed()
        {
            if (PlayerController == null)
            {
                return false;
            }
            
            return PlayerController.IsMovementPressed;
        }

        public bool IsJumpPressed()
        {
            if (PlayerController == null)
            {
                return false;
            }
            
            return PlayerController.IsJumpPressed;
        }

        public void SetAnimatorBool(int isAirborneHash, bool value)
        {
            Animator.SetBool(isAirborneHash, value);
        }

        public bool IsGrounded()
        {
            if (PlayerMovement == null)
            {
                return false;
            }
            
            return PlayerMovement.IsGrounded();
        }

        public float? GetLastGroundedTime()
        {
            if (PlayerMovement == null)
            {
                return null;
            }
            
            return PlayerMovement.LastGroundedTime;
        }

        public float? GetJumpButtonPressedTime()
        {
            if (PlayerController == null)
            {
                return null;
            }
            
            return PlayerController.JumpButtonPressedTime;
        }
        
    }
}

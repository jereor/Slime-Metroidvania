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
        // TODO: Move context data out of PlayerAdapter into PlayerStateMachine
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _meleeAttackHitBox;
        [SerializeField] private D_PlayerMeleeAttack _playerMeleeAttackData;
        [SerializeField] private D_PlayerMovement _playerMovementData;
        
        // Grounded
        public float? LastGroundedTime { get; set; }
        private const float GROUND_CHECK_RADIUS = 0.3f;

        // References
        public PlayerAnimations PlayerAnimations { get; private set; }
        public PlayerCombat PlayerCombat { get; private set; }
        public PlayerFlipper PlayerFlipper { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }

        public PlayerController PlayerController
        {
            get { return _playerController; }
        }
        
        public Rigidbody2D RigidBody
        {
            get { return _rigidbody2D; }
        }

        public Animator Animator
        {
            get { return _animator; }
        }

        private void Awake()
        {
            PlayerAnimations = new PlayerAnimations(this);
            PlayerCombat = new PlayerCombat(this, _meleeAttackHitBox, _playerMeleeAttackData);
            PlayerFlipper = new PlayerFlipper(this);
            PlayerMovement = new PlayerMovement(this, _playerMovementData, new PlayerMovementParameters
            {
                GroundCheck = _groundCheck,
                GroundLayer = _groundLayer
            });
        }

        public void DealDamage()
        {
            PlayerCombat.DealDamage();
        }

        public void FinishMeleeAttacking()
        {
            PlayerCombat.IsMeleeAttacking = false;
        }

        public void FlipPlayer()
        {
            PlayerFlipper.FlipPlayer();
        }

        public void HandleMovement()
        {
            PlayerMovement.HandleMovement();
        }
        
    }
}
using System;
using JetBrains.Annotations;
using Player.Core.Modules;
using Player.Core.Modules.Slime_Sling;
using Player.Core.Parameters;
using Player.Data;
using Player.State_Machine;
using UnityEngine;
using Utility;

namespace Player.Core
{
    [RequireComponent(typeof(PlayerStateMachine))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAdapter : EntityAdapter
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private SlingShooter _slingShooter;
        [SerializeField] private Transform _meleeAttackHitBox;
        [SerializeField] private D_PlayerMeleeAttack _playerMeleeAttackData;
        [SerializeField] private D_PlayerMovement _playerMovementData;

        public override ILoggerAdapter Logger { get; } = new UnityLogger();
        
        // Modules
        public PlayerAnimations PlayerAnimations { get; private set; }
        public PlayerCombat PlayerCombat { get; private set; }
        public PlayerFlipper PlayerFlipper { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }

        // Field accessors
        public PlayerController PlayerController
        {
            get { return _playerController; }
        }

        public SlingShooter SlingShooter
        {
            get { return _slingShooter; }
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
                Animator = _animator,
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

        private void Update()
        {
            PlayerMovement.Update();
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

        public bool IsMeleeAttacking()
        {
            if (PlayerCombat == null)
            {
                Logger.LogWarning($"{nameof(PlayerCombat)} module has null reference. Cannot access it's properties so returning.");
                return false;
            }

            return PlayerCombat.IsMeleeAttacking;
        }

        public bool IsMovementPressed()
        {
            if (PlayerController == null)
            {
                Logger.LogWarning($"{nameof(PlayerController)} module has null reference. Cannot access it's properties so returning.");
                return false;
            }

            return PlayerController.IsMovementPressed;
        }

        public bool IsJumpPressed()
        {
            if (PlayerController == null)
            {
                Logger.LogWarning($"{nameof(PlayerController)} module has null reference. Cannot access it's properties so returning.");
                return false;
            }

            return PlayerController.IsJumpPressed;
        }

        public bool IsGrounded()
        {
            if (PlayerMovement == null)
            {
                Logger.LogWarning($"{nameof(PlayerMovement)} module has null reference. Cannot access it's properties so returning.");
                return false;
            }

            return PlayerMovement.IsGrounded();
        }

        [UsedImplicitly]
        public void FinishAttack()
        {
            PlayerAnimations.FinishAttack();
        }

        public bool IsFalling()
        {
            if (PlayerMovement == null)
            {
                Logger.LogWarning($"{nameof(PlayerMovement)} module has null reference. Cannot access it's properties so returning.");
                return false;
            }
            
            return PlayerMovement.IsFalling;
        }
    }
}

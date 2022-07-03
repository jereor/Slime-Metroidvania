using GameFramework.ComponentSystem;
using GameFramework.Constants;
using NUnit.Framework;
using Player;
using Player.Core_Components;
using Player.Data;
using Player.State_Machine;
using UnityEngine;

namespace Tests.Runtime
{
    public class PlayerTestBase
    {
        protected GameObject _testGameObject;
        protected PlayerStateMachine _playerStateMachine;
        protected Rigidbody2D _rigidbody;
        protected PlayerFlipper _flipperComponent;
        protected VirtualPlayerController _controllerComponent;
        protected PlayerHealth _healthComponent;
        protected Core _coreComponent;
        protected PlayerBase _playerComponent;
        protected PlayerGizmosAdapter _gizmosAdapter;
        protected PlayerMovement _movementComponent;
        protected PlayerCombat _combatComponent;
        
        private const float MAX_HEALTH = 100;

        private static readonly Vector3 GroundCheckPosition = new Vector3(0f, -0.6f, 0);
        private static readonly Vector3 MeleeAttackHitBoxPosition = new Vector3(1.05f, 0, 0);
        private readonly D_PlayerMeleeAttack _playerMeleeAttackData = ScriptableObject.CreateInstance<D_PlayerMeleeAttack>();

        // TODO: Add SlingShooter and SpringJoint2D
        [SetUp]
        public void SetUp()
        {
            _playerStateMachine = new PlayerStateMachine();

            // Create testGameObject
            _testGameObject = Object.Instantiate(new GameObject("TestGameObject"));

            // Add RigidBody component and other essentials
            _rigidbody = _testGameObject.AddComponent<Rigidbody2D>();
            _testGameObject.AddComponent<SpriteRenderer>();
            _testGameObject.AddComponent<Animator>();
            _testGameObject.AddComponent<BoxCollider2D>();
            
            // Instantiate child MeleeAttackHitBox transform
            Transform meleeAttackHitBox = Object.Instantiate(new GameObject("MeleeAttackHitBox"),
                MeleeAttackHitBoxPosition, Quaternion.identity, _testGameObject.transform).transform;
            
            // Instantiate child GroundCheck transform
            Transform groundCheck = Object.Instantiate(new GameObject("GroundCheck"), GroundCheckPosition,
                Quaternion.identity, _testGameObject.transform).transform;
            
            // Initialize PlayerMeleeAttackData
            _playerMeleeAttackData._attackRadius = .5f;
            _playerMeleeAttackData._attackDamage = 10;
            _playerMeleeAttackData._stunDamage = 1;
            _playerMeleeAttackData._damageableLayers = PhysicsConstants.ENEMY_LAYER_NUMBER;
            
            // Add Core component
            _coreComponent = _testGameObject.AddComponent<Core>();
            
            // Add PlayerFlipper component
            _flipperComponent = _testGameObject.AddComponent<PlayerFlipper>();
            _flipperComponent.Initialize(_testGameObject.transform);

            // Add VirtualController component
            _controllerComponent = _testGameObject.AddComponent<VirtualPlayerController>();
            _controllerComponent.Initialize(_flipperComponent);
            
            // Add PlayerHealth component
            _healthComponent = _testGameObject.AddComponent<PlayerHealth>();
            _healthComponent.Initialize(MAX_HEALTH, MAX_HEALTH);

            // Add GizmosAdapter component
            _gizmosAdapter = _testGameObject.AddComponent<PlayerGizmosAdapter>();
            _gizmosAdapter.Initialize(_playerComponent);
            
            // Add PlayerMovement component
            _movementComponent = _testGameObject.AddComponent<PlayerMovement>();
            _movementComponent.Initialize(8f, _rigidbody, 0.2f, 8f, 0.2f, _controllerComponent, groundCheck, PhysicsConstants.GROUND_LAYER_NUMBER);
            
            // Add PlayerCombat component
            _combatComponent = _testGameObject.AddComponent<PlayerCombat>();
            _combatComponent.Initialize(_testGameObject.transform, meleeAttackHitBox, _playerMeleeAttackData);
            
            // Add PlayerBase component
            _playerComponent = _testGameObject.AddComponent<PlayerBase>();
            _playerComponent.Initialize(_coreComponent, meleeAttackHitBox, _playerMeleeAttackData);
            
            // Initialize PlayerStateMachine
            _playerStateMachine.Initialize(_playerComponent);
        }
        
    }
}
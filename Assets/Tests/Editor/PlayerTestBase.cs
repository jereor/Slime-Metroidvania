using GameFramework.ComponentSystem;
using GameFramework.Constants;
using NUnit.Framework;
using Player;
using Player.Core_Components;
using Player.Data;
using Player.State_Machine;
using UnityEngine;

namespace Tests.Editor
{
    public class PlayerTestBase
    {
        private const float MAX_HEALTH = 100;

        private static readonly Vector3 GroundCheckPosition = new Vector3(0f, -0.6f, 0);
        private static readonly Vector3 MeleeAttackHitBoxPosition = new Vector3(1.05f, 0, 0);
        private readonly D_PlayerMeleeAttack _playerMeleeAttackData = ScriptableObject.CreateInstance<D_PlayerMeleeAttack>();

        private GameObject _testGameObject;
        private PlayerStateMachine _playerStateMachine;
        
        // TODO: Add SlingShooter
        [SetUp]
        public void SetUp()
        {
            _playerStateMachine = new PlayerStateMachine();

            // Create testGameObject
            _testGameObject = Object.Instantiate(new GameObject("TestGameObject"));
            _testGameObject.AddComponent<SpriteRenderer>();
            _testGameObject.AddComponent<Animator>();
            _testGameObject.AddComponent<BoxCollider2D>();
            Rigidbody2D rb = _testGameObject.AddComponent<Rigidbody2D>();
            _testGameObject.AddComponent<SpringJoint2D>();
            
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
            
            // Add PlayerFlipper component
            PlayerFlipper flipperComponent = _testGameObject.AddComponent<PlayerFlipper>();
            flipperComponent.Initialize(_testGameObject.transform);

            // Add VirtualController component
            VirtualPlayerController controllerComponent = _testGameObject.AddComponent<VirtualPlayerController>();
            controllerComponent.Initialize(flipperComponent);
            
            // Add PlayerHealth component
            PlayerHealth healthComponent = _testGameObject.AddComponent<PlayerHealth>();
            healthComponent.Initialize(MAX_HEALTH, MAX_HEALTH);
            
            // Add Core component
            Core coreComponent = _testGameObject.AddComponent<Core>();

            // Add PlayerBase component
            PlayerBase playerComponent = _testGameObject.AddComponent<PlayerBase>();
            playerComponent.Initialize(coreComponent, meleeAttackHitBox, _playerMeleeAttackData);
            
            // Add GizmosAdapter component
            PlayerGizmosAdapter gizmosAdapter = _testGameObject.AddComponent<PlayerGizmosAdapter>();
            gizmosAdapter.Initialize(playerComponent);
            
            // Add PlayerMovement component
            PlayerMovement movementComponent = _testGameObject.AddComponent<PlayerMovement>();
            movementComponent.Initialize(8f, rb, 0.2f, 8f, 0.2f, controllerComponent, groundCheck, PhysicsConstants.GROUND_LAYER_NUMBER);
            
            _playerStateMachine.Initialize(playerComponent);
        }
        
    }
}
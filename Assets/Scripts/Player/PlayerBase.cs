using GameFramework.ComponentSystem;
using GameFramework.Loggers;
using Player.Data;
using Player.State_Machine;
using UnityEngine;

namespace Player
{
    public class PlayerBase : MonoBehaviour
    {
        [SerializeField] private Core _core;
        [SerializeField] private Transform _meleeAttackHitBox;
        [SerializeField] private D_PlayerMeleeAttack _playerMeleeAttackData;

        // Field accessors
        public Core Core
        {
            get { return _core; }
        }

        public Transform MeleeAttackHitBox
        {
            get { return _meleeAttackHitBox; }
        }

        public D_PlayerMeleeAttack PlayerMeleeAttackData
        {
            get { return _playerMeleeAttackData; }
        }
        
        public PlayerStateMachine StateMachine { get; private set; }
        
        public ILoggerAdapter Logger
        {
            get { return _core.Logger ?? new UnityLogger(); }
        }

        public void Initialize(Core core, Transform meleeAttackHitBox, D_PlayerMeleeAttack playerMeleeAttackData)
        {
            _core = core;
            _meleeAttackHitBox = meleeAttackHitBox;
            _playerMeleeAttackData = playerMeleeAttackData;
        }

        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
        }

        private void Start()
        {
            StateMachine.Initialize(this);
        }

        private void Update()
        {
            StateMachine.UpdateStates();
        }
    }
}

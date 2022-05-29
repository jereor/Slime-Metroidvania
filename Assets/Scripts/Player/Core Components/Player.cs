using Player.Data;
using Player.State_Machine;
using UnityEngine;
using Utility.Component_System;
using Utility.Logger;

namespace Player.Core_Components
{
    public class Player : MonoBehaviour
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

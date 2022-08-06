using System;
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

        // Field accessors
        public Core Core
        {
            get { return _core; }
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
            PlayerStateFactory stateFactory = new PlayerStateFactory(this, StateMachine);
            StateMachine.Initialize(this, stateFactory, stateFactory.Grounded());
        }

        private void Update()
        {
            StateMachine.UpdateStates();
        }
    }
}

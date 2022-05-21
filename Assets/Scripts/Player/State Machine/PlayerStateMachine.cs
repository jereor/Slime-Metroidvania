using Player.Core;
using Player.State_Machine.States;
using UnityEngine;

namespace Player.State_Machine
{
    // TODO: Make class smaller, separate functionality to smaller classes
    public class PlayerStateMachine : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerAdapter _playerAdapter;
        
        // States
        public PlayerBaseState CurrentState
        {
            set { _currentState = value; }
        }

        public PlayerAdapter PlayerAdapter { get; private set; }

        private PlayerBaseState _currentState;
        private PlayerStateFactory _states;

        private void Awake()
        {
            PlayerAdapter = _playerAdapter;
            
            _states = new PlayerStateFactory(this);
            _currentState = _states.Grounded();
        }
        
        private void Update()
        {
            _currentState.UpdateStates();
        }

    }
}

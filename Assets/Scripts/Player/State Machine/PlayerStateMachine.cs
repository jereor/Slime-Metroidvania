using Player.Core;
using Player.State_Machine.States;
using UnityEngine;

namespace Player.State_Machine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private PlayerAdapter _playerAdapter;

        private PlayerBaseState _currentState;
        private PlayerStateFactory _states;

        public PlayerBaseState CurrentState
        {
            set { _currentState = value; }
        }

        public PlayerAdapter PlayerAdapter { get; private set; }

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

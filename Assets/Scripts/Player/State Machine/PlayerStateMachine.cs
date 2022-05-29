using Player.State_Machine.States;
using UnityEngine;

namespace Player.State_Machine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Core_Components.Player _player;

        private PlayerBaseState _currentState;
        private PlayerStateFactory _states;

        public PlayerBaseState CurrentState
        {
            set { _currentState = value; }
        }

        public Core_Components.Player Player { get; private set; }

        private void Awake()
        {
            Player = _player;

            _states = new PlayerStateFactory(this);
            _currentState = _states.Grounded();
        }

        private void Update()
        {
            _currentState.UpdateStates();
        }
    }
}

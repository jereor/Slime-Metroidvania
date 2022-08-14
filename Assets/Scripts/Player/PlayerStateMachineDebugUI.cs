using Player.State_Machine;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerStateMachineDebugUI : MonoBehaviour
    {
        [SerializeField] private PlayerBase _player;
        [SerializeField] private Text _currentBaseStateText;
        [SerializeField] private Text _currentSubStateText;

        private PlayerStateMachine _stateMachine;

        private void Start()
        {
            _stateMachine = _player.StateMachine;
        }

        private void Update()
        {
            if (_stateMachine?.CurrentBaseState == null 
                || _stateMachine?.CurrentSubState == null)
            {
                return;
            }
            
            string baseStateName = _stateMachine.CurrentBaseState.ToString();
            baseStateName = baseStateName.Remove(0, 28);
            string subStateName = _stateMachine.CurrentSubState.ToString();
            subStateName = subStateName.Remove(0, 28);
            
            _currentBaseStateText.text = baseStateName;
            _currentSubStateText.text = subStateName;
        }
    }
}
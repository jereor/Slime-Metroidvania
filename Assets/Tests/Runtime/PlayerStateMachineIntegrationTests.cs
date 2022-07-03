using System;
using System.Collections;
using Player.State_Machine.States;
using UnityEngine.TestTools;

namespace Tests.Runtime
{
    public class PlayerStateMachineIntegrationTests : PlayerTestBase
    {
        
        [UnityTest]
        public IEnumerator State_Machine_switches_between_states_automatically()
        {
            yield return null;
            
            PlayerBaseState currentBaseState = _playerStateMachine.CurrentBaseState;
            PlayerBaseState currentSubState = _playerStateMachine.CurrentSubState;

            throw new NotImplementedException();
        }
        
    }
}

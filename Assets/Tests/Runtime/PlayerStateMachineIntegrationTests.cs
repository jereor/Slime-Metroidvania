using System.Collections;
using NUnit.Framework;
using Player;
using Player.State_Machine;
using Player.State_Machine.States;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.Runtime
{
    public class PlayerStateMachineIntegrationTests : InputTestFixture
    {
        protected Mouse _mouse;
        protected Keyboard _keyboard;
        protected GameObject _playerPrefab;
        
        [SetUp]
        public void SetUp()
        {
            base.Setup();
            
            _mouse = InputSystem.AddDevice<Mouse>();
            _keyboard = InputSystem.AddDevice<Keyboard>();
            
            _playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
            SceneManager.LoadScene("Scenes/TestSandbox");
        }

        [UnityTest]
        public IEnumerator State_Machine_starts_in_given_default_base_state()
        {
            Vector3 playerStartingPos = new Vector3(0f, 0f, -1f);
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);
            PlayerStateMachine playerStateMachine = player.GetComponent<PlayerBase>().StateMachine;
            
            yield return new WaitForSeconds(1f);
            
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
        }
        
        [UnityTest]
        public IEnumerator State_Machine_switches_from_idle_to_movement_when_movement_input_is_given()
        {
            Vector3 playerStartingPos = new Vector3(0f, 0f, -1f);
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);
            PlayerStateMachine playerStateMachine = player.GetComponent<PlayerBase>().StateMachine;
            
            Press(_keyboard.dKey);
            yield return new WaitForSeconds(0.5f);
            
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMoveState>());
        }
        
    }
}

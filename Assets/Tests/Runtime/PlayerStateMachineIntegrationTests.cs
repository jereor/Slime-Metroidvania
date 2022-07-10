using System;
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
using Object = UnityEngine.Object;

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

            yield return null;
            
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_GroundedState_to_JumpState_when_jump_input_is_given()
        {
            Vector3 playerStartingPos = new Vector3(0f, 0f, -1f);
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);
            PlayerStateMachine playerStateMachine = player.GetComponent<PlayerBase>().StateMachine;
            yield return null;
            
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Press(_keyboard.spaceKey);
            yield return new WaitForSeconds(0.3f);
            
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerJumpState>());
        }

        // TODO: Rework JumpState to work without needing to jump first
        // NOTE: JumpState will probably need to cut it into additional sub states like Jumping, Falling, Landing
        // and JumpState needs to be renamed into AirborneState
        [UnityTest, Ignore("JumpState needs reworking")]
        public IEnumerator State_Machine_switches_from_JumpState_to_GroundedState_when_touching_ground()
        {
            throw new NotImplementedException();
            Vector3 playerStartingPos = new Vector3(0f, 3f, -1f);
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);
            PlayerStateMachine playerStateMachine = player.GetComponent<PlayerBase>().StateMachine;
            yield return new WaitForSeconds(0.1f);
            
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerJumpState>());
            yield return new WaitForSeconds(1f);
            
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
        }
        
        [UnityTest]
        public IEnumerator State_Machine_switches_from_IdleState_to_MoveState_when_movement_input_is_given()
        {
            Vector3 playerStartingPos = new Vector3(0f, 0f, -1f);
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);
            PlayerStateMachine playerStateMachine = player.GetComponent<PlayerBase>().StateMachine;
            yield return null;
            
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerIdleState>());
            Press(_keyboard.dKey);
            yield return new WaitForSeconds(0.3f);
            
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMoveState>());
        }
        
        [UnityTest]
        public IEnumerator State_Machine_switches_from_MoveState_to_IdleState_when_movement_input_stops()
        {
            Vector3 playerStartingPos = new Vector3(0f, 0f, -1f);
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);
            PlayerStateMachine playerStateMachine = player.GetComponent<PlayerBase>().StateMachine;
            yield return null;
            
            Press(_keyboard.dKey);
            yield return new WaitForSeconds(0.3f);
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMoveState>());
            Release(_keyboard.dKey);
            yield return null;
            
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerIdleState>());
        }
        
    }
}

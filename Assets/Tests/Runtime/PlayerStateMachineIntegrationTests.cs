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
        
        private static readonly Vector3 DefaultStartingPosition = new Vector3(0f, 0f, -1f);
        
        [SetUp]
        public void SetUp()
        {
            base.Setup();

            _mouse = InputSystem.AddDevice<Mouse>();
            _keyboard = InputSystem.AddDevice<Keyboard>();

            _playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
            SceneManager.LoadScene("Scenes/TestSandbox");
        }

        private GameObject InstantiatePlayer(Vector3 startingPosition)
        {
            Vector3 playerStartingPos = startingPosition;
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);
            return player;
        }

        [UnityTest]
        public IEnumerator State_Machine_starts_in_given_default_base_state()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;

            // Default state is given in PlayerBase script, where the PlayerStateMachine is initialized on Start()
            yield return null;

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_GroundedState_to_AirborneState_when_jump_input_is_given()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Press(_keyboard.spaceKey);
            yield return new WaitForSeconds(0.3f);

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerAirborneState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_AirborneState_to_GroundedState_when_touching_ground()
        {
            Vector3 startingPosition = new Vector3(0f, 2f, -1f);
            PlayerStateMachine playerStateMachine = InstantiatePlayer(startingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerAirborneState>());
            yield return new WaitForSeconds(1f);

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_IdleState_to_MoveState_when_movement_input_is_given_while_in_GroundedState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerIdleState>());
            Press(_keyboard.dKey);
            yield return new WaitForSeconds(0.3f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMoveState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_IdleState_to_JumpState_when_jump_input_is_given_while_in_GroundedState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerIdleState>());
            Press(_keyboard.spaceKey);
            yield return new WaitForSeconds(0.3f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerJumpState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_IdleState_to_MeleeAttackState_when_melee_attack_input_is_given_while_in_GroundedState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerIdleState>());
            Press(_mouse.rightButton);
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMeleeAttackState>());
        }


        [UnityTest]
        public IEnumerator State_Machine_switches_from_MoveState_to_IdleState_when_movement_input_stops_while_in_GroundedState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Press(_keyboard.dKey);
            yield return new WaitForSeconds(0.3f);
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMoveState>());
            Release(_keyboard.dKey);
            yield return new WaitForSeconds(0.3f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerIdleState>());
        }

        
        [UnityTest]
        public IEnumerator State_Machine_switches_from_MoveState_to_MeleeAttackState_when_melee_attack_input_is_given_while_in_GroundedState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Press(_keyboard.dKey);
            yield return new WaitForSeconds(0.2f);
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMoveState>());
            Press(_mouse.rightButton);
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMeleeAttackState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_JumpState_to_MeleeAttackState_when_melee_attack_input_is_given_while_in_AirborneState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Press(_keyboard.spaceKey);
            yield return new WaitForSeconds(0.1f);
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerAirborneState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerJumpState>());
            Press(_mouse.rightButton);
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMeleeAttackState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_FallState_to_MeleeAttackState_when_melee_attack_input_is_given_while_in_AirborneState()
        {
            Vector3 startingPosition = new Vector3(0f, 2f, -1f);
            PlayerStateMachine playerStateMachine = InstantiatePlayer(startingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerAirborneState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerFallState>());
            Press(_mouse.rightButton);
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMeleeAttackState>());
        }


    }
}
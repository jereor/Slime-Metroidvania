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
        
        private const string PLAYER_PREFAB_PATH = "Assets/Prefabs/Player.prefab";
        private const string PLATFORM_PREFAB_PATH = "Assets/Prefabs/Platform.prefab";
        private static readonly Vector3 DefaultStartingPosition = new Vector3(0f, 0f, -1f);
        
        [SetUp]
        public void SetUp()
        {
            base.Setup();

            _mouse = InputSystem.AddDevice<Mouse>();
            _keyboard = InputSystem.AddDevice<Keyboard>();

            _playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PLAYER_PREFAB_PATH);
            SceneManager.LoadScene("Scenes/TestSandbox");
        }

        private GameObject InstantiatePlayer(Vector3 startingPosition)
        {
            Vector3 playerStartingPos = startingPosition;
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);
            return player;
        }

        // --- BASE STATES ---
        #region Base State Transitions
        
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
        #endregion

        // --- GROUNDED TRANSITIONS ---
        #region Grounded Transitions
        
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
        public IEnumerator State_Machine_switches_from_IdleState_to_FallState_when_player_starts_falling_while_in_GroundedState()
        {
            Vector3 startingPosition = new Vector3(0f, 5f, -1f);
            Vector3 platformPosition = new Vector3(0f, 4f, -1f);
            PlayerStateMachine playerStateMachine = InstantiatePlayer(startingPosition).GetComponent<PlayerBase>().StateMachine;
            GameObject platform = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(PLATFORM_PREFAB_PATH), platformPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.4f);

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerIdleState>());
            Object.Destroy(platform);
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerFallState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_MoveState_to_IdleState_when_movement_input_stops_while_in_GroundedState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Press(_keyboard.dKey);
            yield return new WaitForSeconds(0.4f);
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMoveState>());
            Release(_keyboard.dKey);
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerIdleState>());
        }

        
        [UnityTest]
        public IEnumerator State_Machine_switches_from_MoveState_to_MeleeAttackState_when_melee_attack_input_is_given_while_in_GroundedState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Press(_keyboard.dKey);
            yield return new WaitForSeconds(0.4f);
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMoveState>());
            Press(_mouse.rightButton);
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMeleeAttackState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_MoveState_to_JumpState_when_jump_input_is_given_while_in_GroundedState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Press(_keyboard.dKey);
            yield return new WaitForSeconds(0.4f);
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMoveState>());
            Press(_keyboard.spaceKey);
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerJumpState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_MoveState_to_FallState_when_player_starts_falling_while_in_GroundedState()
        {
            Vector3 startingPosition = new Vector3(0f, 5f, -1f);
            Vector3 platformPosition = new Vector3(0f, 4f, -1f);
            PlayerStateMachine playerStateMachine = InstantiatePlayer(startingPosition).GetComponent<PlayerBase>().StateMachine;
            GameObject platform = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(PLATFORM_PREFAB_PATH), platformPosition, Quaternion.identity);
            yield return null;

            Press(_keyboard.dKey);
            yield return new WaitForSeconds(0.1f);
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerGroundedState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMoveState>());
            Object.Destroy(platform);
            yield return new WaitForSeconds(0.3f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerFallState>());
        }
        
        #endregion

        // --- AIRBORNE TRANSITIONS ---
        #region Airborne Transitions

        [UnityTest]
        public IEnumerator State_Machine_switches_from_JumpState_to_JumpPeakState_when_jump_hits_its_peak_while_in_AirborneState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Press(_keyboard.spaceKey);
            yield return new WaitForSeconds(0.1f);
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerAirborneState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerJumpState>());
            yield return new WaitWhile(() => playerStateMachine.CurrentSubState.GetType() == typeof(PlayerJumpState));
            
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerJumpPeakState>());
        }
        
        [UnityTest]
        public IEnumerator State_Machine_switches_from_JumpState_to_MeleeAttackState_when_melee_attack_input_is_given_while_in_AirborneState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Press(_keyboard.spaceKey);
            yield return new WaitForSeconds(0.2f);
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerAirborneState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerJumpState>());
            Press(_mouse.rightButton);
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMeleeAttackState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_FallState_to_MeleeAttackState_when_melee_attack_input_is_given_while_in_AirborneState()
        {
            Vector3 startingPosition = new Vector3(0f, 5f, -1f);
            PlayerStateMachine playerStateMachine = InstantiatePlayer(startingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return new WaitForSeconds(0.1f);

            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerAirborneState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerFallState>());
            Press(_mouse.rightButton);
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerMeleeAttackState>());
        }

        [UnityTest]
        public IEnumerator State_Machine_switches_from_JumpPeakState_to_FallState_when_player_starts_falling_while_in_AirborneState()
        {
            PlayerStateMachine playerStateMachine = InstantiatePlayer(DefaultStartingPosition).GetComponent<PlayerBase>().StateMachine;
            yield return null;

            Press(_keyboard.spaceKey);
            yield return new WaitForSeconds(0.1f);
            Assert.That(playerStateMachine.CurrentBaseState, Is.InstanceOf<PlayerAirborneState>());
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerJumpState>());
            yield return new WaitWhile(() => playerStateMachine.CurrentSubState.GetType() == typeof(PlayerJumpState));
            yield return new WaitWhile(() => playerStateMachine.CurrentSubState.GetType() == typeof(PlayerJumpPeakState));
            
            Assert.That(playerStateMachine.CurrentSubState, Is.InstanceOf<PlayerFallState>());
        }
        
        #endregion
        
    }
}
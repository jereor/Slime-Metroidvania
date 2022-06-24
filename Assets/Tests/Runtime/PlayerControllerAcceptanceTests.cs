using System;
using System.Collections;
using NUnit.Framework;
using Player;
using Player.State_Machine.States;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Tests.Runtime
{
    public class PlayerControllerAcceptanceTests : InputTestFixture
    {
        private Mouse _mouse;
        private Keyboard _keyboard;
        
        private GameObject _playerPrefab;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            _mouse = InputSystem.AddDevice<Mouse>();
            _keyboard = InputSystem.AddDevice<Keyboard>();
            
            _playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
            SceneManager.LoadScene("Scenes/TestSandbox");
        }

        [UnityTest]
        public IEnumerator CharacterMovesRightWhenControllerIsGivenInputAxisRight()
        {
            Vector3 playerStartingPos = new Vector3(0f, 0f, -1f);
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);

            Press(_keyboard.dKey);
            yield return new WaitForSeconds(1f);
            Release(_keyboard.dKey);
            Vector3 playerEndPos = player.gameObject.transform.position;

            float distanceMoved = Mathf.Abs(playerEndPos.x - playerStartingPos.x);
            Assert.Greater(distanceMoved, 0);
        }
        
        [UnityTest]
        public IEnumerator CharacterMovesLeftWhenControllerIsGivenInputAxisLeft()
        {
            Vector3 playerStartingPos = new Vector3(0f, 0f, -1f);
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);

            Press(_keyboard.aKey);
            yield return new WaitForSeconds(1f);
            Release(_keyboard.aKey);
            Vector3 playerEndPos = player.gameObject.transform.position;

            float distanceMoved = Mathf.Abs(playerEndPos.x - playerStartingPos.x);
            Assert.Greater(distanceMoved, 0);
        }
        
        [UnityTest]
        public IEnumerator CharacterJumpsWhenControllerIsGivenInputJump()
        {
            Vector3 playerStartingPos = new Vector3(0f, 0f, -1f);
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);
            yield return new WaitForSeconds(1f);

            Press(_keyboard.spaceKey);
            yield return new WaitForSeconds(0.5f);
            Release(_keyboard.spaceKey);
            Vector3 playerEndPos = player.gameObject.transform.position;

            float distanceJumped = Mathf.Abs(playerEndPos.x - playerStartingPos.x);
            Assert.Greater(distanceJumped, 0);
        }
        
        [UnityTest]
        public IEnumerator CharacterAttacksWhenControllerIsGivenInputMeleeAttack()
        {
            Vector3 playerStartingPos = new Vector3(0f, 0f, -1f);
            Quaternion playerDir = Quaternion.identity;
            GameObject player = Object.Instantiate(_playerPrefab, playerStartingPos, playerDir);

            PressAndRelease(_mouse.rightButton);
            yield return new WaitForSeconds(0.2f);
            Type actualState = player.GetComponent<PlayerBase>().StateMachine.CurrentSubState.GetType();

            Type expectedState = typeof(PlayerMeleeAttackState);
            Assert.AreSame(expectedState, actualState);
        }
    }
}

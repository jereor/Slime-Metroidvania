using System;
using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
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
        }

        [UnityTest]
        public IEnumerator CharacterMovesRightWhenControllerIsGivenInputAxisRight()
        {
            Vector3 playerStartingPos = new Vector3(2f, 1f, -1f);
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
            Vector3 playerStartingPos = new Vector3(2f, 1f, -1f);
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
            throw new NotImplementedException();
        }
        
        [UnityTest]
        public IEnumerator CharacterAttacksWhenControllerIsGivenInputMeleeAttack()
        {
            throw new NotImplementedException();
        }
    }
}

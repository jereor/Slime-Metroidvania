using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Player.Core_Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Editor
{
    public class PlayerControllerIntegrationTests
    {
        private GameObject _player;
        private PlayerController _playerController;
        private PlayerMovement _playerMovement;

        [SetUp]
        public void Setup()
        {
            _player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
            _playerController = _player.GetComponent<PlayerController>();
            _playerMovement = _player.GetComponent<PlayerMovement>();
        }

        [UnityTest]
        public IEnumerator CharacterMovesRightWhenControllerIsGivenInputAxisRight()
        {
            _playerController.CurrentMovementInput = 1f;
            _playerMovement.LogicUpdate();
            
            yield return null;
            
            Assert.Greater(_playerMovement.CurrentVelocity.x, 0f);
        }
        
        [UnityTest]
        public IEnumerator CharacterMovesLeftWhenControllerIsGivenInputAxisLeft()
        {
            throw new NotImplementedException();
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

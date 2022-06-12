using System;
using NUnit.Framework;
using Player.Core_Components;
using UnityEngine;

namespace Tests.Editor
{
    public class PlayerControllerUnitTests
    {
        private PlayerController _playerController;

        [SetUp]
        public void Setup()
        {
            _playerController = new GameObject("TestPlayerController", typeof(PlayerController))
                .GetComponent<PlayerController>();
        }

        [Test]
        public void CharacterMovesRightWhenControllerIsGivenInputAxisRight()
        {
            
        }
        
        [Test]
        public void CharacterMovesLeftWhenControllerIsGivenInputAxisLeft()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public void CharacterJumpsWhenControllerIsGivenInputJump()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public void CharacterAttacksWhenControllerIsGivenInputMeleeAttack()
        {
            throw new NotImplementedException();
        }
    }
}

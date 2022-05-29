using Player.Data;
using Player.State_Machine;
using UnityEngine;
using Utility.Component_System;
using Utility.Logger;

namespace Player.Core_Components
{
    [RequireComponent(typeof(PlayerStateMachine))]
    [RequireComponent(typeof(PlayerController))]
    public class Player : EntityAdapter
    {
        [SerializeField] private Core _core;
        [SerializeField] private Transform _meleeAttackHitBox;
        [SerializeField] private D_PlayerMeleeAttack _playerMeleeAttackData;

        public override ILoggerAdapter Logger { get; } = new UnityLogger();

        // Field accessors
        public Core Core
        {
            get { return _core; }
        }

        public Transform MeleeAttackHitBox
        {
            get { return _meleeAttackHitBox; }
        }

        public D_PlayerMeleeAttack PlayerMeleeAttackData
        {
            get { return _playerMeleeAttackData; }
        }
        
    }
}

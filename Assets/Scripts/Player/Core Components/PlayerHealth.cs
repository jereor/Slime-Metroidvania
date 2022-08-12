using GameFramework;
using GameFramework.ComponentSystem;
using UnityEngine;

namespace Player.Core_Components
{
    public class PlayerHealth : Health
    {
        [Header("Player Health references")]
        [SerializeField] private PlayerMovement _playerMovement;
        
        public override void Damage(AttackDetails attackDetails)
        {
            base.Damage(attackDetails);

            _playerMovement.DamageKnockback(LastDamageDirection);
        }
    }
}

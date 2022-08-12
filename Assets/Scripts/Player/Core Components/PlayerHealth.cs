using GameFramework;
using GameFramework.ComponentSystem;
using UnityEngine;

namespace Player.Core_Components
{
    public class PlayerHealth : Health
    {
        [Header("Player Health references")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private GameObject _bloodParticles;
        
        public override void Damage(AttackDetails attackDetails)
        {
            base.Damage(attackDetails);

            _playerMovement.DamageKnockback(LastDamageDirection);
            _playerCamera.CameraShake(2, .2f);
            Instantiate(_bloodParticles, transform.position, Quaternion.identity);
        }
    }
}

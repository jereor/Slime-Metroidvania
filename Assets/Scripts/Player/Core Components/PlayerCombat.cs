using GameFramework;
using GameFramework.ComponentSystem;
using GameFramework.Constants;
using Player.Data;
using UnityEngine;

namespace Player.Core_Components
{
    public class PlayerCombat : Combat
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _meleeAttackHitBox;
        [SerializeField] private D_PlayerMeleeAttack _playerMeleeAttackData;
        
        public bool IsMeleeAttacking { get; private set; }

        public Transform MeleeAttackHitBox
        {
            get
            {
                return _meleeAttackHitBox;
            }
        }
        
        public D_PlayerMeleeAttack MeleeAttackData
        {
            get
            {
                return _playerMeleeAttackData;
            }
        }

        public void MeleeAttack()
        {
            IsMeleeAttacking = true;
        }
        
        public void DealDamage()
        {
            Collider2D[] detectedObjects =
                Physics2D.OverlapCircleAll(_meleeAttackHitBox.position, _playerMeleeAttackData._attackRadius,
                    _playerMeleeAttackData._damageableLayers);

            AttackDetails attackDetails = new AttackDetails
            {
                Position = _playerTransform.position,
                DamageAmount = _playerMeleeAttackData._attackDamage,
                StunDamageAmount = _playerMeleeAttackData._stunDamage
            };
            
            foreach (Collider2D hitObject in detectedObjects)
            {
                hitObject.transform.SendMessageUpwards(EventConstants.DAMAGE, attackDetails);
            }
        }

        public void FinishMeleeAttacking()
        {
            IsMeleeAttacking = false;
        }
        
    }
}

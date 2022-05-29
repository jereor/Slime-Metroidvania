using Player.Data;
using UnityEngine;
using Utility;
using Utility.Component_System;
using Utility.Constants;

namespace Player.Core_Components
{
    public class PlayerCombat : Combat
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _meleeAttackHitBox;
        [SerializeField] private D_PlayerMeleeAttack _playerMeleeAttackData;
        
        public bool IsMeleeAttacking { get; set; }

        public void DealDamage()
        {
            Collider2D[] detectedObjects =
                Physics2D.OverlapCircleAll(_meleeAttackHitBox.position, _playerMeleeAttackData._attackRadius,
                    _playerMeleeAttackData._damageableLayers);

            AttackDetails attackDetails;
            attackDetails.Position = _playerTransform.position;
            attackDetails.DamageAmount = _playerMeleeAttackData._attackDamage;
            attackDetails.StunDamageAmount = _playerMeleeAttackData._stunDamage;

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

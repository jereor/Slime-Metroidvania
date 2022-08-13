using System.Collections.Generic;
using System.Linq;
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
            Vector2 attackPosition = _meleeAttackHitBox.position;
            Collider2D[] detectedObjects =
                Physics2D.OverlapCircleAll(attackPosition, _playerMeleeAttackData._attackRadius,
                    _playerMeleeAttackData._damageableLayers);

            Collider2D wallObject =
                Physics2D.OverlapCircle(attackPosition, _playerMeleeAttackData._attackRadius,
                    1<<PhysicsConstants.GROUND_LAYER_NUMBER);

            bool wallWasHit = wallObject != null;
            if (wallWasHit)
            {
                return;
            }

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

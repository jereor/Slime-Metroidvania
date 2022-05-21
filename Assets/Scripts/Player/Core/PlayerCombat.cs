using Player.Data;
using UnityEngine;
using Utility;

namespace Player.Core
{
    public class PlayerCombat
    {
        private readonly PlayerAdapter _playerAdapter;
        private readonly Transform _meleeAttackHitBox;
        private readonly D_PlayerMeleeAttack _playerMeleeAttackData;
        
        public bool IsMeleeAttacking { get; set; }
        
        public PlayerCombat(PlayerAdapter playerAdapter, Transform meleeAttackHitBox, D_PlayerMeleeAttack playerMeleeAttackData)
        {
            _playerAdapter = playerAdapter;
            _meleeAttackHitBox = meleeAttackHitBox;
            _playerMeleeAttackData = playerMeleeAttackData;
        }

        public void DealDamage()
        {
            Collider2D[] detectedObjects =
                Physics2D.OverlapCircleAll(_meleeAttackHitBox.position, _playerMeleeAttackData._attackRadius,
                    _playerMeleeAttackData._damageableLayers);

            AttackDetails attackDetails;
            attackDetails.Position = _playerAdapter.transform.position;
            attackDetails.DamageAmount = _playerMeleeAttackData._attackDamage;
            attackDetails.StunDamageAmount = _playerMeleeAttackData._stunDamage;

            foreach (Collider2D hitObject in detectedObjects)
            {
                hitObject.transform.SendMessageUpwards(EventConstants.DAMAGE, attackDetails);
            }
        }
        
        // Gizmos
        private void OnDrawGizmos()
        {
            Vector3 attackPosition = _meleeAttackHitBox.position;
            Gizmos.DrawWireSphere(attackPosition, _playerMeleeAttackData._attackRadius);
        }
    }
}
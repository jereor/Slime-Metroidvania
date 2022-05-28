using Player.Core.Parameters;
using Player.Data;
using UnityEngine;
using Utility;

namespace Player.Core.Modules
{
    public class PlayerCombat
    {
        private readonly Transform _playerTransform;
        private readonly Transform _meleeAttackHitBox;
        private readonly D_PlayerMeleeAttack _playerMeleeAttackData;
        
        public PlayerCombat(D_PlayerMeleeAttack playerMeleeAttackData, PlayerCombatParameters playerCombatParameters)
        {
            _playerTransform = playerCombatParameters.PlayerTransform;
            _meleeAttackHitBox = playerCombatParameters.MeleeAttackHitBox;
            _playerMeleeAttackData = playerMeleeAttackData;
        }
        
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
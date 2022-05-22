using UnityEngine;
using Utility;

namespace Player.Core
{
    public class PlayerGizmosAdapter : GizmosAdapter
    {
        [SerializeField] private PlayerAdapter _playerAdapter;
        
        public override void OnDrawGizmos()
        {
            DrawPlayerCombatGizmos();
        }

        private void DrawPlayerCombatGizmos()
        {
            Vector3 attackPosition = _playerAdapter.MeleeAttackHitBox.position;
            Gizmos.DrawWireSphere(attackPosition, _playerAdapter.PlayerMeleeAttackData._attackRadius);
        }
    }
}

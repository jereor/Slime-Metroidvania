using UnityEngine;
using Utility.Component_System;

namespace Player.Core_Components
{
    public class PlayerGizmosAdapter : GizmosAdapter
    {
        [SerializeField] private Player _player;
        
        public override void OnDrawGizmos()
        {
            DrawPlayerCombatGizmos();
        }

        private void DrawPlayerCombatGizmos()
        {
            Vector3 attackPosition = _player.MeleeAttackHitBox.position;
            Gizmos.DrawWireSphere(attackPosition, _player.PlayerMeleeAttackData._attackRadius);
        }
    }
}

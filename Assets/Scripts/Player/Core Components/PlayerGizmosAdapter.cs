using UnityEngine;
using Utility.Component_System;

namespace Player.Core_Components
{
    public class PlayerGizmosAdapter : GizmosAdapter
    {
        [SerializeField] private PlayerBase _playerBase;
        
        public override void OnDrawGizmos()
        {
            DrawPlayerCombatGizmos();
        }

        private void DrawPlayerCombatGizmos()
        {
            Vector3 attackPosition = _playerBase.MeleeAttackHitBox.position;
            Gizmos.DrawWireSphere(attackPosition, _playerBase.PlayerMeleeAttackData._attackRadius);
        }
    }
}

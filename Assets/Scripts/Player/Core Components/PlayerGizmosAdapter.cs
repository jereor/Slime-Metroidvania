using GameFramework.ComponentSystem;
using UnityEngine;

namespace Player.Core_Components
{
    public class PlayerGizmosAdapter : GizmosAdapter
    {
        [SerializeField] private PlayerBase _playerBase;
        
        private PlayerCombat _playerCombat;
        
        private PlayerCombat PlayerCombat
        {
            get { return _playerCombat ??= _playerBase.Core.GetCoreComponent<PlayerCombat>(); }
        }
        
        public override void OnDrawGizmos()
        {
            DrawPlayerCombatGizmos();
        }

        private void DrawPlayerCombatGizmos()
        {
            Vector3 attackPosition = PlayerCombat.MeleeAttackHitBox.position;
            Gizmos.DrawWireSphere(attackPosition, PlayerCombat.MeleeAttackData._attackRadius);
        }
        
    }
}

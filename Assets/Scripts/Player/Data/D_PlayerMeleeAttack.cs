using UnityEngine;
using Utility;

namespace Player.Data
{
    [CreateAssetMenu(fileName = "newPlayerMeleeAttackData", menuName = "Data/Player Data/Melee Attack")]
    public class D_PlayerMeleeAttack : ScriptableObject
    {
        public float _attackRadius = 0.5f;
        public float _attackDamage = 10f;
        public float _stunDamage = 1f;

        public LayerMask _damageableLayers = ~(1 << PhysicsConstants.ENEMY_LAYER_NUMBER);
    }
}
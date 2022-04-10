using UnityEngine;
using Utility;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
    public class D_MeleeAttackState : ScriptableObject
    {
        public float _attackRadius = 0.5f;
        public float _attackDamage = 10f;

        public LayerMask _playerLayer = PhysicsConstants.PLAYER_LAYER_NUMBER;
    }
}

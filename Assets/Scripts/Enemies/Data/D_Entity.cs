using UnityEngine;
using Utility;

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
    public class D_Entity : ScriptableObject
    {
        public float _maxHealth = 30f;

        public float _damageHopSpeed = 10f;
        
        public float _wallCheckDistance = 0.2f;
        public float _ledgeCheckDistance = 0.4f;
        public float _groundCheckRadius = 0.3f;
        
        public float _minAggroDistance = 3f;
        public float _maxAggroDistance = 4f;

        public float _stunResistance = 3f;
        public float _stunRecoveryTime = 2f;

        public float _closeRangeActionDistance = 1f;

        public GameObject _hitParticles;
        
        public LayerMask _groundLayer = PhysicsConstants.GROUND_LAYER_NUMBER;
        public LayerMask _playerLayer = PhysicsConstants.PLAYER_LAYER_NUMBER;
    }
}

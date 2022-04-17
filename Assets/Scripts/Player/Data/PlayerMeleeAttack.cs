using UnityEngine;
using Utility;

namespace Player.Data
{
    public struct PlayerMeleeAttack
    {
        public const float ATTACK_RADIUS = 0.5f;
        public const float ATTACK_DAMAGE = 10f;
        public const float STUN_DAMAGE = 1f;

        public static LayerMask DamageableLayers = ~((1 << PhysicsConstants.ENEMY_LAYER_NUMBER) | (1 << PhysicsConstants.CAMERA_BOUNDS_LAYER_NUMBER));
    }
}
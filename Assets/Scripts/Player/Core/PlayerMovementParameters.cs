using UnityEngine;

namespace Player.Core
{
    public struct PlayerMovementParameters
    {
        public PlayerController PlayerController;
        public Rigidbody2D Rigidbody;
        public Transform GroundCheck;
        public LayerMask GroundLayer;
    }
}

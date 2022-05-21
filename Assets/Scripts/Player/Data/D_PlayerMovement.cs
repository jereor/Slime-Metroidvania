using UnityEngine;

namespace Player.Data
{
    [CreateAssetMenu(fileName = "newPlayerMovementData", menuName = "Data/Player Data/Movement")]
    public class D_PlayerMovement : ScriptableObject
    {
        public float _coyoteTime = 0.2f;
        public float _jumpForce = 8;
        public float _moveSpeed = 8;
        public float _groundCheckRadius;
    }
}

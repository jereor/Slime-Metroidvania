using UnityEngine;

namespace Player.Data
{
    [CreateAssetMenu(fileName = "newPlayerMovementData", menuName = "Data/Player Data/Movement")]
    public class D_PlayerMovement : ScriptableObject
    {
        public float CoyoteTime = 0.2f;
        public float JumpForce = 8;
        public float MoveSpeed = 8;
    }
}

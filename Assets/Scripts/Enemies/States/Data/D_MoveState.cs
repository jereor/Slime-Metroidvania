using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State")]
    public class D_MoveState : ScriptableObject
    {
        public float _movementSpeed = 3f;
        
    }
}

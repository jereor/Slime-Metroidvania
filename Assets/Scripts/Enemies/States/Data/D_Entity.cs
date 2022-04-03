using UnityEngine;

namespace Enemies.States.Data
{
    public class D_Entity : ScriptableObject
    {
        public float _wallCheckDistance;
        public float _ledgeCheckDistance;

        public LayerMask _groundLayer;
    }
}

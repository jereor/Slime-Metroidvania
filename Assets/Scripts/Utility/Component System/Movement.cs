using UnityEngine;

namespace Utility.Component_System
{
    public abstract class Movement : CoreComponent
    {
        // TODO: Move some generic movement stuff from PlayerMovement to here
        [Header("Data")]
        [SerializeField] protected float _moveSpeed;
        
        [Header("Dependencies")]
        [SerializeField] protected Rigidbody2D _rigidBody;
        
        public Vector2 CurrentVelocity { get; set; }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            CurrentVelocity = _rigidBody.velocity;
        }
    }
}
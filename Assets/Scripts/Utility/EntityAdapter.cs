using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class EntityAdapter : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rigidbody2D;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected Transform _groundCheck;
        [SerializeField] protected LayerMask _groundLayer;

        public ILoggerAdapter Logger { get; private set; }

        public Rigidbody2D RigidBody
        {
            get { return _rigidbody2D; }
        }

        public Animator Animator
        {
            get { return _animator; }
        }
    }
}

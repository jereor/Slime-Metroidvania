using JetBrains.Annotations;
using UnityEngine;

namespace GameFramework.ComponentSystem
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;

        protected int LastDamageDirection { get; private set; }

        public virtual void Start()
        {
            _currentHealth = _maxHealth;
        }

        [UsedImplicitly]
        public virtual void Damage(AttackDetails attackDetails)
        {
            _currentHealth -= attackDetails.DamageAmount;
            CheckDeath();
            
            bool attackFromRight = attackDetails.Position.x > transform.position.x;
            LastDamageDirection = attackFromRight ? -1 : 1;
        }

        public virtual void CheckDeath()
        {
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
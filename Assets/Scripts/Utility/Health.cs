using UnityEngine;

namespace Utility
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;

        public virtual void Start()
        {
            _currentHealth = _maxHealth;
        }

        public virtual void Damage(float damageAmount)
        {
            _currentHealth -= damageAmount;
            CheckDeath();
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
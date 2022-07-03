using UnityEngine;

namespace GameFramework.ComponentSystem
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;

        public void Initialize(float currentHealth, float maxHealth)
        {
            _currentHealth = currentHealth;
            _maxHealth = maxHealth;
        }
        
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
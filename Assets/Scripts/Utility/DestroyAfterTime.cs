using System.Collections;
using UnityEngine;

namespace Utility
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] private float _lifetime;

        private void Start()
        {
            StartCoroutine(DestroyAfter(_lifetime));
        }

        private IEnumerator DestroyAfter(float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }
    }
}

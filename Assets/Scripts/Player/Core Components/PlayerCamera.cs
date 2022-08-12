using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Player.Core_Components
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] [Range(5, 10)] private float _zoomOutLevel = 6.5f;
    
        // Main camera
        private CinemachineVirtualCamera _virtualCam;

        private void Awake()
        {
            _virtualCam = GetComponent<CinemachineVirtualCamera>();
        }
    
        private void OnEnable()
        {
            _virtualCam.m_Lens.OrthographicSize = _zoomOutLevel;
        }

        public void CameraShake(float intensity, float time)
        {
            if (gameObject.activeInHierarchy)
                StartCoroutine(Shake(intensity, time));
        }

        private IEnumerator Shake(float intensity, float time)
        {
            var perlin = _virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            perlin.m_AmplitudeGain = intensity;
            yield return new WaitForSeconds(time);
            perlin.m_AmplitudeGain = 0f;
        }
    }
}

using UnityEngine;

namespace Camera
{
    public class Parallax : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _parallaxMultiplier;
        [SerializeField] private bool _isHorizontalOnly;
        [SerializeField] private bool _canCalculateInfiniteHorizontalPosition;
        [SerializeField] private bool _canCalculateInfiniteVerticalPosition;
        [SerializeField] private bool _isInfinite;

        private GameObject _camera;
        private Vector3 _startPosition;
        private Vector3 _startCameraPosition;
        private float _length;
     
        private void Start()
        {
            _camera = UnityEngine.Camera.main!.gameObject;

            _startPosition = transform.position;
            _startCameraPosition = _camera.transform.position;

            if (_isInfinite)
            {
                _length = GetComponent<SpriteRenderer>().bounds.size.x;
            }

            CalculateStartPosition();
        }

        private void CalculateStartPosition()
        {
            Vector3 mainCameraPosition = _camera.transform.position;
            Vector3 myPosition = transform.position;
            
            float parallaxEffectX = (mainCameraPosition.x - myPosition.x) *_parallaxMultiplier;
            float parallaxEffectY = (mainCameraPosition.y - myPosition.y) * _parallaxMultiplier;
            Vector3 newStartPosition = new Vector3(_startPosition.x, _startPosition.y);

            if (_canCalculateInfiniteHorizontalPosition)
            {
                newStartPosition.x = transform.position.x + parallaxEffectX;
            }
            if (_canCalculateInfiniteVerticalPosition)
            {
                newStartPosition.y = transform.position.y + parallaxEffectY;
            }

            _startPosition = newStartPosition;
        }

        private void Update()
        {
            UpdateParallaxEffect();

            if (_isInfinite)
            {
                UpdateInfiniteEffect();
            }
        }

        private void UpdateParallaxEffect()
        {
            Vector3 position = _startPosition;

            if (_isHorizontalOnly)
            {
                position.x += _parallaxMultiplier * (_camera.transform.position.x - _startCameraPosition.x);
            }
            else
            {
                position += _parallaxMultiplier * (_camera.transform.position - _startCameraPosition);
            }

            transform.position = position;
        }

        private void UpdateInfiniteEffect()
        {
            float distanceRelativeToCamera = _camera.transform.position.x * (1 - _parallaxMultiplier);
            if (distanceRelativeToCamera > _startPosition.x + _length)
            {
                _startPosition.x += _length;
            }
            else if (distanceRelativeToCamera < _startPosition.x - _length)
            {
                _startPosition.x -= _length;
            }
        }
    }
}

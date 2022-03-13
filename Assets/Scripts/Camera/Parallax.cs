using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _parallaxModifier;

    private float _length;
    private float _startPosition;

    private void Start()
    {
        _startPosition = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = _camera.transform.position.x * _parallaxModifier;

        transform.position = 
            new Vector3(_startPosition + distance, transform.position.y, transform.position.z);
    }
}

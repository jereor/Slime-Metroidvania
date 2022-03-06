using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShooter : MonoBehaviour
{
    public static SlingShooter Instance;

    [Header("Layers Settings:")]
    [SerializeField] private bool _grappleToAll = false;
    [SerializeField] private int _grappableLayerNumber = 9;

    [Header("Main Camera:")]
    [SerializeField] private Camera _camera;

    [Header("Transform Ref:")]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _slingShooter;
    public Transform OriginPoint;

    [Header("Physics Ref:")]
    [SerializeField] private SpringJoint2D _springJoint;
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool _rotateOverTime = true;
    [SerializeField, Range(0, 60)] private float _rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool _hasMaxDistance = false;
    [SerializeField] private float _maxDistance = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool _launchToPoint = true;
    [SerializeField] private LaunchType _launchType = LaunchType.Physics_Launch;
    [SerializeField] private float _launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool _autoConfigureDistance = false;
    [SerializeField] private float _targetDistance = 3;
    [SerializeField] private float _targetFrequncy = 1;

    [HideInInspector] public Vector2 GrapplePoint;
    [HideInInspector] public Vector2 GrappleDistanceVector;
    private bool _pulling;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SlimeSling.Instance.enabled = false;
        _springJoint.enabled = false;
    }

    private void Update()
    {
        HandleSlingPull();
    }

    internal void CancelPull()
    {
        SlimeSling.Instance.enabled = false;
        _springJoint.enabled = false;
        _rigidbody.gravityScale = 1;
    }

    public void HandleSlingPull()
    {
        if (_pulling == false)
        {
            return;
        }
        
        if (SlimeSling.Instance.enabled)
        {
            RotateGun(GrapplePoint, false);
        }
        else
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RotateGun(mousePos, true);
        }

        if (_launchToPoint && SlimeSling.Instance.IsGrappling)
        {
            if (_launchType == LaunchType.Transform_Launch)
            {
                Vector2 firePointDistnace = OriginPoint.position - _player.localPosition;
                Vector2 targetPos = GrapplePoint - firePointDistnace;
                _player.position = Vector2.Lerp(_player.position, targetPos, Time.deltaTime * _launchSpeed);
            }
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - _slingShooter.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (_rotateOverTime && allowRotationOverTime)
        {
            _slingShooter.rotation = Quaternion.Lerp(_slingShooter.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * _rotationSpeed);
        }
        else
        {
            _slingShooter.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    internal void SetGrapplePoint()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 distanceVector = mousePos - _slingShooter.position;

        if (Physics2D.Raycast(OriginPoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(OriginPoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == _grappableLayerNumber || _grappleToAll)
            {
                if (Vector2.Distance(_hit.point, OriginPoint.position) <= _maxDistance || !_hasMaxDistance)
                {
                    GrapplePoint = _hit.point;
                    GrappleDistanceVector = GrapplePoint - (Vector2)_slingShooter.position;
                    SlimeSling.Instance.enabled = true;
                }
            }
        }
    }

    internal void StartPull()
    {
        _pulling = true;
    }

    public void Grapple()
    {
        _springJoint.autoConfigureDistance = false;
        if (!_launchToPoint && !_autoConfigureDistance)
        {
            _springJoint.distance = _targetDistance;
            _springJoint.frequency = _targetFrequncy;
        }
        if (!_launchToPoint)
        {
            if (_autoConfigureDistance)
            {
                _springJoint.autoConfigureDistance = true;
                _springJoint.frequency = 0;
            }

            _springJoint.connectedAnchor = GrapplePoint;
            _springJoint.enabled = true;
        }
        else
        {
            switch (_launchType)
            {
                case LaunchType.Physics_Launch:
                    _springJoint.connectedAnchor = GrapplePoint;

                    Vector2 distanceVector = OriginPoint.position - _player.position;

                    _springJoint.distance = distanceVector.magnitude;
                    _springJoint.frequency = _launchSpeed;
                    _springJoint.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    _rigidbody.gravityScale = 0;
                    _rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (OriginPoint != null && _hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(OriginPoint.position, _maxDistance);
        }
    }

}

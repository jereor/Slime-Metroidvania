using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Core.Slime_Sling
{
    public class SlingShooter : MonoBehaviour
    {
        public static SlingShooter Instance;
        
        public Transform OriginPoint
        {
            get { return _originPoint; }
        }
        
        [NonSerialized] public Vector2 GrapplePoint;
        [NonSerialized] public Vector2 GrappleDistanceVector;

        [Header("Layers Settings:")]
        [SerializeField] private bool _grappleToAll;
        [SerializeField] private LayerMask _groundLayer;

        [Header("Main Camera:")]
        [SerializeField] private UnityEngine.Camera _camera;

        [Header("Transform Ref:")]
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _slingShooter;
        [SerializeField] private Transform _originPoint;

        [Header("Physics Ref:")]
        [SerializeField] private SpringJoint2D _springJoint;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Rotation:")]
        [SerializeField] private bool _rotateOverTime = true;
        [SerializeField, Range(0, 60)] private float _rotationSpeed = 4;

        [Header("Distance:")]
        [SerializeField] private bool _hasMaxDistance;
        [SerializeField] private float _maxDistance = 20;

        private enum LaunchType
        {
            TransformLaunch,
            PhysicsLaunch
        }

        [Header("Launching:")]
        [SerializeField] private bool _isLaunchedToPoint = true;
        [SerializeField] private LaunchType _launchType = LaunchType.PhysicsLaunch;
        [SerializeField] private float _launchSpeed = 1;

        [Header("No Launch To Point")]
        [SerializeField] private bool _autoConfigureDistance;
        [SerializeField] private float _targetDistance = 3;
        [SerializeField] private float _targetFrequency = 1;
        
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
            _rigidbody.gravityScale = PhysicsConstants.DEFAULT_GRAVITY_SCALE;
        }

        private void HandleSlingPull()
        {
            if (_pulling == false)
            {
                return;
            }
        
            if (SlimeSling.Instance.enabled)
            {
                Debug.Log("Slime Sling instance enabled!");
                RotateShooterTo(GrapplePoint);
            }
            else
            {
                Vector3 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                RotateShooterTo(mousePos);
            }

            // TODO: See if this if statement can be made more readable
            if (!_isLaunchedToPoint
                || !SlimeSling.Instance.IsGrappling
                || _launchType != LaunchType.TransformLaunch)
            {
                return;
            }
            
            Vector2 firePointDistance = _originPoint.position - _player.localPosition;
            Vector2 targetPos = GrapplePoint - firePointDistance;
            _player.position = Vector2.Lerp(_player.position, targetPos, Time.deltaTime * _launchSpeed);
        }

        private void RotateShooterTo(Vector3 lookPoint)
        {
            Vector3 distanceVector = lookPoint - _slingShooter.position;

            // TODO: See if this if statement can be made more readable
            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
            _slingShooter.rotation = _rotateOverTime 
                ? Quaternion.Lerp(_slingShooter.rotation, 
                    Quaternion.AngleAxis(angle, Vector3.forward), 
                    Time.deltaTime * _rotationSpeed) 
                : Quaternion.AngleAxis(angle, Vector3.forward);
        }

        internal void SetGrapplePoint()
        {
            Debug.Log("Setting up grapple point...");
            Vector3 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 distanceVector = mousePos - _slingShooter.position;

            if (!Physics2D.Raycast(_originPoint.position, distanceVector.normalized))
            {
                return;
            }
            
            // TODO: See if this if statement can be made more readable
            RaycastHit2D hit = Physics2D.Raycast(_originPoint.position, distanceVector.normalized);
            
            if (hit.transform.gameObject.layer != _groundLayer.value && !_grappleToAll)
            {
                Debug.Log("Did not hit any ground.");
                return;
            }
            
            if (!(Vector2.Distance(hit.point, _originPoint.position) <= _maxDistance) && _hasMaxDistance)
            {
                Debug.Log("Distance too great.");
                return;
            }
                
            GrapplePoint = hit.point;
            GrappleDistanceVector = GrapplePoint - (Vector2)_slingShooter.position;
            SlimeSling.Instance.enabled = true;
        }

        internal void StartPull()
        {
            _pulling = true;
        }

        public void Grapple()
        {
            _springJoint.autoConfigureDistance = false;
            if (!_isLaunchedToPoint && !_autoConfigureDistance)
            {
                _springJoint.distance = _targetDistance;
                _springJoint.frequency = _targetFrequency;
            }
            if (!_isLaunchedToPoint)
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
                    case LaunchType.PhysicsLaunch:
                        _springJoint.connectedAnchor = GrapplePoint;

                        Vector2 distanceVector = _originPoint.position - _player.position;

                        _springJoint.distance = distanceVector.magnitude;
                        _springJoint.frequency = _launchSpeed;
                        _springJoint.enabled = true;
                        break;
                    case LaunchType.TransformLaunch:
                        _rigidbody.gravityScale = 0;
                        _rigidbody.velocity = Vector2.zero;
                        break;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_originPoint == null || !_hasMaxDistance)
            {
                return;
            }
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_originPoint.position, _maxDistance);
        }

    }
}

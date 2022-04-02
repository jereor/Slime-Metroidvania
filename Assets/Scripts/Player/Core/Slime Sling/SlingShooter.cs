using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

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

        [Header("Main Camera:")] [SerializeField]
        private UnityEngine.Camera _camera;

        [Header("Transform Ref:")] [SerializeField]
        private Transform _player;

        [SerializeField] private Transform _slingShooter;
        [SerializeField] private Transform _originPoint;

        [Header("Physics Ref:")] [SerializeField]
        private SpringJoint2D _springJoint;

        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Launching:")] [SerializeField]
        private bool _isLaunchedToPoint = true;

        [SerializeField] private float _launchSpeed = 1;

        [Header("No Launch To Point")] [SerializeField]
        private bool _autoConfigureDistance;

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
            if (_pulling == false)
            {
                return;
            }

            HandleSlingPull();
            HandleSlingRotation();
        }

        private void HandleSlingRotation()
        {
            if (SlimeSling.Instance.enabled)
            {
                RotateShooterTo(GrapplePoint);
            }
            else
            {
                Vector3 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                RotateShooterTo(mousePos);
            }
        }

        internal void CancelPull()
        {
            SlimeSling.Instance.enabled = false;
            _springJoint.enabled = false;
            _rigidbody.gravityScale = PhysicsConstants.DEFAULT_GRAVITY_SCALE;
        }

        private void HandleSlingPull()
        {
            // ReSharper disable once InvertIf
            if (_isLaunchedToPoint
                && SlimeSling.Instance.IsGrappling)
            {
                PullPlayer();
            }
        }

        private void PullPlayer()
        {
            Vector2 firePointDistance = _originPoint.position - _player.localPosition;
            Vector2 targetPos = GrapplePoint - firePointDistance;
            _player.position = Vector2.Lerp(_player.position, targetPos, Time.deltaTime * _launchSpeed);
        }

        private void RotateShooterTo(Vector3 lookPoint)
        {
            Vector3 distanceVector = lookPoint - _slingShooter.position;
            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
            _slingShooter.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        internal void SetGrapplePoint()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 slingShooterPosition = _slingShooter.position;
            Vector2 distanceVector = mousePos - slingShooterPosition;

            if (!Physics2D.Raycast(_originPoint.position, distanceVector.normalized))
            {
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(_originPoint.position, distanceVector.normalized);
            if (hit.transform.gameObject.layer != PhysicsConstants.GROUND_LAYER_NUMBER)
            {
                return;
            }

            GrapplePoint = hit.point;
            GrappleDistanceVector = GrapplePoint - (Vector2) slingShooterPosition;
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
                _rigidbody.gravityScale = 0;
                _rigidbody.velocity = Vector2.zero;
            }
        }
    }
}
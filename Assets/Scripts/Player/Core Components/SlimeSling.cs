using System;
using UnityEngine;

namespace Player.Core_Components
{
    public class SlimeSling : MonoBehaviour
    {
        [Header("Sling Shooter")] 
        [SerializeField] private SlingShooter _slingShooter;
        
        [Header("General References")]
        [SerializeField] private LineRenderer _lineRenderer;

        [Header("General Settings")]
        [SerializeField] private int _lineVertexCount = 40;
        [SerializeField, Range(0, 20)] private float _straightenLineSpeed = 5;

        [Header("Rope Animation Settings")]
        [SerializeField] private AnimationCurve _ropeAnimationCurve;
        [SerializeField, Range(0.01f, 4)] private float _startWaveSize = 2;
        private float _waveSize;

        [Header("Rope Progression")]
        [SerializeField] private AnimationCurve _ropeProgressionCurve;
        [SerializeField, Range(1, 50)] private float _ropeProgressionSpeed = 1;

        private float _timer;
        private bool _isStraightLine = true;
        private const double TOLERANCE = 0.1;

        public bool IsGrappling { get; private set; } = true;

        private void OnEnable()
        {
            _timer = 0;
            _lineRenderer.positionCount = _lineVertexCount;
            _waveSize = _startWaveSize;
            _isStraightLine = false;

            if (_slingShooter != null)
            {
                LinePointsToFirePoint();
            }

            _lineRenderer.enabled = true;
        }
        
        private void LinePointsToFirePoint()
        {
            for (int i = 0; i < _lineVertexCount; i++)
            {
                _lineRenderer.SetPosition(i, _slingShooter.OriginPoint.position);
            }
        }

        private void OnDisable()
        {
            _lineRenderer.enabled = false;
            IsGrappling = false;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            DrawRope();
        }

        private void DrawRope()
        {
            if (!_isStraightLine)
            {
                HandleRopeShootAnimation();
            }
            else
            {
                HandleGrapplePull();
            }
        }

        private void HandleGrapplePull()
        {
            if (!IsGrappling)
            {
                _slingShooter.Grapple();
                IsGrappling = true;
            }

            if (_waveSize > 0)
            {
                _waveSize -= Time.deltaTime * _straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                RemoveWaves();
                DrawRopeNoWaves();
            }
        }

        private void HandleRopeShootAnimation()
        {
            bool ropeHasReachedGrapplePoint =
                Math.Abs(_lineRenderer.GetPosition(_lineVertexCount - 1).x - _slingShooter.GrapplePoint.x) < TOLERANCE;
            if (ropeHasReachedGrapplePoint)
            {
                _isStraightLine = true;
            }
            else
            {
                DrawRopeWaves();
            }
        }

        private void RemoveWaves()
        {
            _waveSize = 0;

            if (_lineRenderer.positionCount != 2)
            {
                _lineRenderer.positionCount = 2;
            }
        }

        private void DrawRopeWaves()
        {
            for (int i = 0; i < _lineVertexCount; i++)
            {
                float distanceFromFirePoint = i / (_lineVertexCount - 1f);
                float vertexHeightInCurve = _ropeAnimationCurve.Evaluate(distanceFromFirePoint);

                Vector2 perpendicularDirection = Vector2.Perpendicular(_slingShooter.GrappleDistanceVector).normalized;
                Vector3 originPointPosition = _slingShooter.OriginPoint.position;

                Vector2 offset = perpendicularDirection * (vertexHeightInCurve * _waveSize);
                Vector2 targetPosition = Vector2.Lerp(originPointPosition, _slingShooter.GrapplePoint,
                    distanceFromFirePoint) + offset;

                Vector2 currentPosition = Vector2.Lerp(originPointPosition, targetPosition,
                    _ropeProgressionCurve.Evaluate(_timer) * _ropeProgressionSpeed);
                _lineRenderer.SetPosition(i, currentPosition);
            }
        }

        private void DrawRopeNoWaves()
        {
            _lineRenderer.SetPosition(0, _slingShooter.OriginPoint.position);
            _lineRenderer.SetPosition(1, _slingShooter.GrapplePoint);
        }
    }
}

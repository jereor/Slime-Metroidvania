using System;
using UnityEngine;

namespace Player.Core.Slime_Sling
{
    public class SlimeSling : MonoBehaviour
    {
        public static SlimeSling Instance;
        
        [NonSerialized] public bool IsGrappling = true;

        [Header("General References:")]
        [SerializeField] private LineRenderer _lineRenderer;

        [Header("General Settings:")]
        [SerializeField] private int _precision = 40;
        [SerializeField, Range(0, 20)] private float _straightenLineSpeed = 5;

        [Header("Rope Animation Settings:")]
        [SerializeField] private AnimationCurve _ropeAnimationCurve;
        [SerializeField, Range(0.01f, 4)] private float _startWaveSize = 2;
        private float _waveSize;

        [Header("Rope Progression:")]
        [SerializeField] private AnimationCurve _ropeProgressionCurve;
        [SerializeField, Range(1, 50)] private float _ropeProgressionSpeed = 1;

        private float _timer;
        private bool _isStraightLine = true;

        private void OnEnable()
        {
            _timer = 0;
            _lineRenderer.positionCount = _precision;
            _waveSize = _startWaveSize;
            _isStraightLine = false;

            if (SlingShooter.Instance != null)
            {
                LinePointsToFirePoint();
            }

            _lineRenderer.enabled = true;
        }

        private void OnDisable()
        {
            _lineRenderer.enabled = false;
            IsGrappling = false;
        }

        private void LinePointsToFirePoint()
        {
            for (int i = 0; i < _precision; i++)
            {
                _lineRenderer.SetPosition(i, SlingShooter.Instance.OriginPoint.position);
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
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
                SlingShooter.Instance.Grapple();
                IsGrappling = true;
            }

            if (_waveSize > 0)
            {
                _waveSize -= Time.deltaTime * _straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                _waveSize = 0;

                if (_lineRenderer.positionCount != 2)
                {
                    _lineRenderer.positionCount = 2;
                }

                DrawRopeNoWaves();
            }
        }

        private void HandleRopeShootAnimation()
        {
            bool ropeHasReachedGrapplePoint =
                Math.Abs(_lineRenderer.GetPosition(_precision - 1).x - SlingShooter.Instance.GrapplePoint.x) < TOLERANCE;
            if (ropeHasReachedGrapplePoint)
            {
                _isStraightLine = true;
            }
            else
            {
                DrawRopeWaves();
            }
        }

        private const double TOLERANCE = .1;

        private void DrawRopeWaves()
        {
            for (int i = 0; i < _precision; i++)
            {
                float delta = i / (_precision - 1f);
                Vector2 offset = Vector2.Perpendicular(SlingShooter.Instance.GrappleDistanceVector).normalized * (_ropeAnimationCurve.Evaluate(delta) * _waveSize);
                Vector3 originPointPosition = SlingShooter.Instance.OriginPoint.position;
                
                Vector2 targetPosition = Vector2.Lerp(originPointPosition, SlingShooter.Instance.GrapplePoint, delta) + offset;
                Vector2 currentPosition = Vector2.Lerp(originPointPosition, targetPosition, _ropeProgressionCurve.Evaluate(_timer) * _ropeProgressionSpeed);

                _lineRenderer.SetPosition(i, currentPosition);
            }
        }

        private void DrawRopeNoWaves()
        {
            _lineRenderer.SetPosition(0, SlingShooter.Instance.OriginPoint.position);
            _lineRenderer.SetPosition(1, SlingShooter.Instance.GrapplePoint);
        }
    }
}

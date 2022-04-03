using Pathfinding;
using UnityEngine;

namespace Worm
{
    public class WormAI : MonoBehaviour
    {
        [Header("Pathfinding")] 
        [SerializeField] private Transform _target;
        [SerializeField] private float _activateDistance = 50f;
        [SerializeField] private float _pathUpdateSeconds = 0.5f;
        
        [Header("Physics")]
        [SerializeField] private float _speed = 200f;
        [SerializeField] private float _nextWaypointDistance = 3f;
        [SerializeField] private float _jumpNodeHeightRequirement = 0.8f;
        [SerializeField] private float _jumpModifier = 0.3f;
        [SerializeField] private float _jumpCheckOffset = 0.1f;

        [Header("Custom Behavior")] 
        [SerializeField] private bool _followEnabled = true;
        [SerializeField] private bool _jumpEnabled = true;
        [SerializeField] private bool _directionLookEnabled = true;

        private Path _path;
        private int _currentWaypoint;
        private Seeker _seeker;
        private Rigidbody2D _rb;
        private bool _isFacingRight;

        private void Start()
        {
            _seeker = GetComponent<Seeker>();
            _rb = GetComponent<Rigidbody2D>();
            
            InvokeRepeating(nameof(UpdatePath), 0f, _pathUpdateSeconds);
        }
        
        private void UpdatePath()
        {
            if (_followEnabled && TargetInDistance() && _seeker.IsDone())
            {
                _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
            }
        }

        private void OnPathComplete(Path path)
        {
            if (path.error)
            {
                return;
            }

            _path = path;
            _currentWaypoint = 0;
        }

        private void FixedUpdate()
        {
            if (_followEnabled && TargetInDistance())
            {
                PathFollow();
            }
        }

        private void PathFollow()
        {
            if (_path == null)
            {
                Debug.Log("Path null!");
                return;
            }

            if (_currentWaypoint >= _path.vectorPath.Count)
            {
                Debug.Log("End of path reached!");
                return;
            }

            Vector2 movementDirection = ((Vector2) _path.vectorPath[_currentWaypoint] - _rb.position).normalized;
            Vector2 forceDirection = movementDirection * (_speed * Time.deltaTime);

            if (_jumpEnabled && IsGrounded())
            {
                HandleJumping(movementDirection.y);
            }
            
            _rb.AddForce(forceDirection);
            CalculateNextWaypoint();
            
            if (_directionLookEnabled && HasMoveDirectionChanged(movementDirection))
            {
                FlipSprite();
            }
        }
        
        private void CalculateNextWaypoint()
        {
            float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
            if (distance < _nextWaypointDistance)
            {
                _currentWaypoint++;
            }
        }

        private void FlipSprite()
        {
            _isFacingRight = !_isFacingRight;
            Transform currentTransform = transform;
            Vector3 localScale = currentTransform.localScale;
            
            localScale.x *= -1f;
            currentTransform.localScale = localScale;
        }

        private void HandleJumping(float movementOnYAxis)
        {
            if (movementOnYAxis > _jumpNodeHeightRequirement)
            {
                _rb.AddForce(Vector2.up * (_speed * _jumpModifier));
            }
        }
        
        // --------- BOOLS ---------
        private bool IsGrounded()
        {
            Vector3 startOffset = transform.position -
                                  new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + _jumpCheckOffset);
            
            return Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        }
        
        private bool TargetInDistance()
        {
            return Vector2.Distance(transform.position, _target.transform.position) < _activateDistance;
        }
        
        private bool HasMoveDirectionChanged(Vector2 movementDirection)
        {
            bool facingRightButNowMovingLeft = _isFacingRight && movementDirection.x < 0f;
            bool facingLeftButNowMovingRight = !_isFacingRight && movementDirection.x > 0f;

            return facingRightButNowMovingLeft
                   || facingLeftButNowMovingRight;
        }
    }
}

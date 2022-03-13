using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] private float _moveSpeed;

    private PlayerStateMachine _context;
    private Vector2 _currentVelocity;

    public static PlayerMovement Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void HandleMovement(PlayerStateMachine currentContext)
    {
        _context = currentContext;

        if (_context.CurrentMovementInput == 0)
        {
            Stop();
            return;
        }

        _context.RigidBody.velocity = 
            new Vector2(x: _context.CurrentMovementInput * _moveSpeed, 
                        y: _context.RigidBody.velocity.y);
    }

    private void Stop()
    {
        _currentVelocity = new Vector2(x: 0, y: _currentVelocity.y);
    }
}

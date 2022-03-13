using UnityEngine;

public class PlayerFlipper : MonoBehaviour
{
    private PlayerStateMachine _context;

    public static PlayerFlipper Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void HandleDirectionChange(PlayerStateMachine currentContext)
    {
        _context = currentContext;

        if (_context.HasMoveDirectionChanged)
        {
            FlipSprite();
            FlipSlingShooter();
        }
    }

    private void FlipSprite()
    {
        _context.IsFacingRight = !_context.IsFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private static void FlipSlingShooter()
    {
        Vector3 slingShooterLocalScale = SlingShooter.Instance.transform.localScale;
        slingShooterLocalScale.x *= -1f;
        SlingShooter.Instance.transform.localScale = slingShooterLocalScale;
    }
}

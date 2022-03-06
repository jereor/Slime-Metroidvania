using UnityEngine;

public class SlimeSling : MonoBehaviour
{
    public static SlimeSling Instance;

    [Header("General Refernces:")]
    public LineRenderer LineRenderer;

    [Header("General Settings:")]
    [SerializeField] private int precision = 40;
    [SerializeField, Range(0, 20)] private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings:")]
    public AnimationCurve RopeAnimationCurve;
    [SerializeField, Range(0.01f, 4)] private float StartWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression:")]
    public AnimationCurve RopeProgressionCurve;
    [SerializeField, Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;

    [HideInInspector] public bool IsGrappling = true;

    private bool _isStraightLine = true;

    private void OnEnable()
    {
        moveTime = 0;
        LineRenderer.positionCount = precision;
        waveSize = StartWaveSize;
        _isStraightLine = false;

        if (SlingShooter.Instance != null)
        {
            LinePointsToFirePoint();
        }

        LineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        LineRenderer.enabled = false;
        IsGrappling = false;
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
        {
            LineRenderer.SetPosition(i, SlingShooter.Instance.OriginPoint.position);
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
        moveTime += Time.deltaTime;
        DrawRope();
    }

    void DrawRope()
    {
        if (!_isStraightLine)
        {
            if (LineRenderer.GetPosition(precision - 1).x == SlingShooter.Instance.GrapplePoint.x)
            {
                _isStraightLine = true;
            }
            else
            {
                DrawRopeWaves();
            }
        }
        else
        {
            if (!IsGrappling)
            {
                SlingShooter.Instance.Grapple();
                IsGrappling = true;
            }
            if (waveSize > 0)
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;

                if (LineRenderer.positionCount != 2) { LineRenderer.positionCount = 2; }

                DrawRopeNoWaves();
            }
        }
    }

    void DrawRopeWaves()
    {
        for (int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1f);
            Vector2 offset = Vector2.Perpendicular(SlingShooter.Instance.GrappleDistanceVector).normalized * RopeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(SlingShooter.Instance.OriginPoint.position, SlingShooter.Instance.GrapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(SlingShooter.Instance.OriginPoint.position, targetPosition, RopeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            LineRenderer.SetPosition(i, currentPosition);
        }
    }

    void DrawRopeNoWaves()
    {
        LineRenderer.SetPosition(0, SlingShooter.Instance.OriginPoint.position);
        LineRenderer.SetPosition(1, SlingShooter.Instance.GrapplePoint);
    }
}

using UnityEngine;

public class ShakeUI : MonoBehaviour
{
    public RectTransform target;
    public float shakeRange = 10f;
    public float updateInterval = 0.3f;

    [SerializeField] private AnimationCurve curve;

    private Vector2 originalPos;
    private Vector2 fromOffset;
    private Vector2 toOffset;
    private float timer;

    void Start()
    {
        if (target == null)
            target = GetComponent<RectTransform>();

        originalPos = target.anchoredPosition;
        SetNewTargetOffset();
    }

    void Update()
    {
        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / updateInterval);
        float curvedT = curve.Evaluate(t);

        // 보간된 위치 계산
        Vector2 currentOffset = Vector2.Lerp(fromOffset, toOffset, curvedT);
        target.anchoredPosition = originalPos + currentOffset;

        if (timer >= updateInterval)
        {
            timer = 0f;
            SetNewTargetOffset();
        }
    }

    void SetNewTargetOffset()
    {
        fromOffset = toOffset;
        toOffset = new Vector2(
            Random.Range(-shakeRange, shakeRange),
            Random.Range(-shakeRange, shakeRange)
        );
    }
}
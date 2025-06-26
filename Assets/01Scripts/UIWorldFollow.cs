using UnityEngine;

public class UIWorldFollow : MonoBehaviour
{
    public Transform target; // 플레이어의 머리
    public Vector3 offset;   // 머리 위 오프셋
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
        rectTransform.position = screenPos;
    }
}
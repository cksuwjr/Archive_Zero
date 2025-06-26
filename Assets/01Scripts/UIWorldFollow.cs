using UnityEngine;

public class UIWorldFollow : MonoBehaviour
{
    public Transform target; // �÷��̾��� �Ӹ�
    public Vector3 offset;   // �Ӹ� �� ������
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
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform camTransform;
    private Collider targetCollider;

    public float topViewThreshold = 60f;

    private void Start()
    {
        camTransform = Camera.main.transform;
        targetCollider = GetComponentInParent<Collider>();
    }

    private void LateUpdate()
    {
        if (targetCollider == null) return;

        // ������Ʈ ���� UI ��ġ
        float height = targetCollider.bounds.size.y;
        transform.position = transform.parent.position + Vector3.up * height;

        Vector3 camForward = camTransform.forward;
        float angleFromUp = Vector3.Angle(Vector3.down, camForward); // ������ �����ٺ����� ���� ����

        if (angleFromUp < topViewThreshold)
        {
            Vector3 dir = transform.position - camTransform.position;
            dir.y = 0f; // Y ����
            if (dir != Vector3.zero)
                transform.forward = dir.normalized;
        }
        else
        {
            transform.forward = (transform.position - camTransform.position).normalized;
        }
    }
}
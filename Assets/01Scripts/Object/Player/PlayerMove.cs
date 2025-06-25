using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMove
{
    [SerializeField] private float moveSpeed = 5f; // 기본값 5

    // 이동량 계산을 위한 벡터
    private Vector3 moveDelta;

    public void Move(Vector3 direction)
    {
        moveDelta.x = direction.x;
        moveDelta.y = 0;
        moveDelta.z = direction.z;

        moveDelta *= (moveSpeed * Time.deltaTime);
        moveDelta += transform.position;

        transform.position = moveDelta;
    }
}
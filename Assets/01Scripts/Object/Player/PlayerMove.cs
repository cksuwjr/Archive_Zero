using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMove
{
    [SerializeField] private Transform checkWallPosY;
    [SerializeField] private LayerMask wallLayer; 
    [SerializeField] private float moveSpeed = 5f; // 기본값 5

    private Animator animator;

    // 이동량 계산을 위한 벡터
    private Vector3 moveDelta;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void Move(Vector3 direction)
    {
        moveDelta.x = direction.x;
        moveDelta.y = 0;
        moveDelta.z = direction.z;

        animator?.SetBool("Move", direction.sqrMagnitude > 0.01f);
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }

        if (!Physics.Raycast(checkWallPosY.transform.position, transform.forward, 1, wallLayer))
        {
            moveDelta.Normalize();
            moveDelta *= (moveSpeed * Time.deltaTime);
            moveDelta += transform.position;

            transform.position = moveDelta;
        }
    }
}
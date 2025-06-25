using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMovingBackground : MonoBehaviour
{
    private Renderer render;
    private Vector2 offset = Vector2.zero;
    private Transform playerTransform;
    private Vector3 playerPos;
    private Vector3 dist;

    [SerializeField] private float speed;

    private void Awake()
    {
        TryGetComponent<Renderer>(out render);
    }

    private void Start()
    {
        playerTransform = GameManager.Instance.Player.transform;
        render.material.mainTextureScale = Vector2.one;
        dist = transform.position - playerTransform.position;
    }

    public void Move(float x, float y)
    {
        offset.x += x * speed * Time.fixedDeltaTime;
        offset.y += y * speed * Time.fixedDeltaTime;
        render.material.mainTextureOffset = offset;
    }

    private void LateUpdate()
    {
        Move(-(playerTransform.position.x - playerPos.x), -(playerTransform.position.z - playerPos.z));
        playerPos = playerTransform.position;
        transform.position = playerPos + dist;
    }
}
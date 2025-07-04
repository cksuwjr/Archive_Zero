using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolObject, IWeapon
{
    private Rigidbody rb;
    private bool isInit = false;
    private float damage = 0;
    private float speed = 0;
    private Vector3 direction;

    private GameObject owner;
    [SerializeField] private bool onePass = true;

    private Action OnHit;

    private void Awake()
    {
        TryGetComponent<Rigidbody>(out rb);
    }

    public void Init(GameObject attacker, Vector3 direction, float damage, float speed, bool isOneHit = false, Action OnHit = null)
    {
        SetEnable(true);
        SetOwner(attacker);
        transform.forward = direction;
        this.direction = direction;
        this.damage = damage;
        this.speed = speed;

        onePass = isOneHit;
        this.OnHit += OnHit;

        Invoke("ReturnToPool", 2);
    }


    public void Fire()
    {
        if (!isInit) return;

        rb.velocity = direction * speed;
    }

    public void SetEnable(bool enable)
    {
        isInit = enable;
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {

            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                if (other.TryGetComponent<Environment>(out var environment))
                {
                    ReturnToPool();
                    CancelInvoke("ReturnToPool");
                    return;
                }





                if (other.TryGetComponent<Entity>(out var entity))
                {
                    entity.GetDamage(owner.GetComponent<Entity>(), damage);
                    //if (GameManager.Instance.Player.TryGetComponent<CodecBlast>(out var co))
                    //    co.CooltimeDecline(0.1f);
                    OnHit?.Invoke();
                    OnHit = null;
                    isInit = false;
                    if (onePass)
                    {
                        ReturnToPool();
                        CancelInvoke("ReturnToPool");
                    }
                }
            }
        }
        catch { }
    }
}

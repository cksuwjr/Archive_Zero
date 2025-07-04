using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heal,
    Exp,
    Archive
}
public class Item : PoolObject
{
    private bool isFlying = false; 
    public void AcquireTo(Entity entity)
    {
        if(!isFlying)
            StartCoroutine("Acquire", entity);
    }

    public virtual void Acquired(Entity entity)
    {
        isFlying = false;
        ReturnToPool();
    }

    private IEnumerator Acquire(Entity entity)
    {
        var pos = transform.position;
        var timer = 0f;

        isFlying = true;
        while (timer < 1)
        {
            transform.position = Vector3.Lerp(pos, entity.transform.position, timer);
            yield return null;
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                Acquired(player);
            }
        }
    }
}

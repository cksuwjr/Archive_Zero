using UnityEngine;

public class Environment : Entity
{
    protected override void Die()
    {
        status.HP = status.MaxHP;
        EntitySpawner.RePosition(this);
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EntityOuter"))
        {
            EntitySpawner.RePosition(this);

            for(int i= 0; i< transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);

            transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
        }
    }
}

using UnityEngine;

public class DibboAttack : MonoBehaviour
{
    [SerializeField] float damage = 20;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            if (other.gameObject.TryGetComponent<Monster>(out var mon))
                mon.GetDamage(GameManager.Instance.Player.GetComponent<Entity>(), damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            if (other.TryGetComponent<Monster>(out var mon))
                mon.GetDamage(GameManager.Instance.Player.GetComponent<Entity>(), damage);
        }
    }
}

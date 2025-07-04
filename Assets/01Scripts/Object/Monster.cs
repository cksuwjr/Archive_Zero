using System;
using System.Collections;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Moving,
    Attack,
}


public class Monster : Entity, IMove
{
    private Rigidbody rb;
    private MonsterState monsterState;
    private GameObject player;

    private bool movable = false;
    private Vector3 direction;
    private Coroutine changeDirectionCoroutine;

    private bool isBinded = false;

    public static event Action<Monster> OnMonsterDie;
    public static event Action<Monster> OnMonsterHit;

    protected override void DoAwake()
    {
        TryGetComponent<Rigidbody>(out rb);
    }

    public void Init(float hp, float speed, float attackPower)
    {
        status.MaxHP = hp;
        status.MoveSpeed = speed;
        status.AttackPower = attackPower;

        status.HP = status.MaxHP;

        player = GameManager.Instance.Player;

        movable = true;
        isBinded = false;

        ChangeState(MonsterState.Moving);
    }

    public void Move(Vector3 direction)
    {
        if(movable)
            rb.velocity = direction * status.MoveSpeed;
    }

    public void ChangeState(MonsterState state)
    {
        StopCoroutine(monsterState.ToString());
        monsterState = state;
        StartCoroutine(monsterState.ToString());
    }

    private IEnumerator Idle()
    {
        rb.velocity = Vector3.zero;
        while (true)
            yield return null;
    }

    private IEnumerator Moving()
    {
        while (player != null)
        {
            if (!isBinded)
                SetMoveTarget(player.transform.position);
            yield return YieldInstructionCache.WaitForSeconds(0.6f);
        }
        ChangeState(MonsterState.Idle);
    }

    private IEnumerator Attack()
    {
        yield return null;
    }

    private IEnumerator ChangeDirection(Vector3 before, Vector3 after)
    {
        float timer = 0f;

        if (after.sqrMagnitude < 0.001f)
            yield break; // 회전할 방향이 없음

        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.LookRotation(after);

        while (timer < 0.2f)
        {
            if (!isBinded)
            {
                transform.rotation = Quaternion.Slerp(startRot, targetRot, timer / 0.2f);
            }
            timer += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRot;
    }

    private void SetMoveTarget(Vector3 newPos)
    {
        var origin = direction;
        Vector3 targetDir = newPos - transform.position;
        targetDir.y = 0;

        if (targetDir.sqrMagnitude < 0.001f)
            return; // 너무 가까우면 무시

        direction = targetDir.normalized;

        // 회전 코루틴이 돌고 있으면 중지
        if (changeDirectionCoroutine != null)
            StopCoroutine(changeDirectionCoroutine);

        changeDirectionCoroutine = StartCoroutine(ChangeDirection(origin, direction));

        rb.velocity = direction * status.MoveSpeed;
    }

    public override void GetDamage(Entity attacker, float damage, float knockbackTime = 3f)
    {
        //rb.velocity = Vector3.zero;

        KnockBack(attacker.gameObject, knockbackTime);
        OnMonsterHit?.Invoke(this);
        base.GetDamage(this, damage);
    }

    public void KnockBack(GameObject fromwho, float howmuch)
    {
        StartCoroutine(CC(0.2f));

        Vector3 dir = Vector3.zero;
        try { dir = (fromwho.transform.position - transform.position).normalized; } catch { }
        //transform.rotation = Quaternion.LookRotation(dir);
        rb.velocity = new Vector3(-dir.x * howmuch, rb.velocity.y, -dir.z * howmuch);
    }

    public IEnumerator CC(float time)
    {
        isBinded = true;
        yield return YieldInstructionCache.WaitForSeconds(time);
        isBinded = false;
    }

    protected override void Die()
    {
        //GameManager.Instance.Player.GetComponent<PlayerController>().GetExp(5);
        OnMonsterDie?.Invoke(this);
        base.Die();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (collision.gameObject.gameObject.TryGetComponent<Entity>(out var entity))
            entity.GetDamage(this, status.AttackPower);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EntityOuter"))
        {
            EntitySpawner.RePosition(this);
        }
    }
}

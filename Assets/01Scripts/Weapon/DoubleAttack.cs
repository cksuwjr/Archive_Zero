using System.Collections;
using UnityEngine;

public class DoubleAttack : Skill
{
    public int count = 2;
    public float damage = 10;
    protected override IEnumerator Cast_()
    {
        var damageConst = GameManager.Instance.Player.GetComponent<Status>().AttackPower;
        var dir = transform.forward;
        for (int i = 0; i < count; i++)
        {
            var proj = PoolManager.Instance.basicAttackPool.GetPoolObject();
            if (proj.TryGetComponent<Projectile>(out var pro))
            {
                proj.transform.position = transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));
                pro.Init(GameManager.Instance.Player, dir, damage * damageConst, 20
                    );
                pro.Fire();
            }
            yield return YieldInstructionCache.WaitForSeconds(0.6f / count);
        }
        yield return null;
    }

    public override void Upgrade()
    {
        base.Upgrade();

        switch (skill_Level)
        {
            case 1:
                count = 2;
                damage = 10;
                break;
            case 2:
                count = 2;
                damage = 20;
                break;
            case 3:
                count = 3;
                damage = 35;
                break;
            case 4:
                count = 4;
                damage = 60;
                break;
            case 5:
                count = 5;
                damage = 100;
                break;

        }
    }
    // 40  60  100  180  320 codec
    // 60  90  120  150  180 dot
    // 20  40  60  80  100   basic
} 


using System.Collections;
using UnityEngine;

public class DoubleAttack : Skill
{
    public int count = 2;
    public float damage = 10;
    protected override IEnumerator Cast_()
    {
        var dir = transform.forward;
        for (int i = 0; i < count; i++)
        {
            var proj = PoolManager.Instance.basicAttackPool.GetPoolObject();
            if (proj.TryGetComponent<Projectile>(out var pro))
            {
                proj.transform.position = transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));
                pro.Init(dir, damage, 20);
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
                damage = 20;
                break;
            case 4:
                count = 4;
                damage = 20;
                break;
            case 5:
                count = 5;
                damage = 20;
                break;

        }
    }
}


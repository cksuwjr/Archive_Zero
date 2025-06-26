using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodecBlast : Skill
{
    public float damage = 15;
    public int count = 1;

    protected override IEnumerator Cast_()
    {

        var damageConst = GameManager.Instance.Player.GetComponent<Status>().AttackPower;
        GameManager.Instance.Player.GetComponent<PlayerController>().GetCC(1f);
        GameManager.Instance.Player.GetComponentInChildren<Animator>().SetTrigger("EzAttack");
        var dir = transform.forward;
        yield return YieldInstructionCache.WaitForSeconds(1f);

        bool isRight = false;
        float angle = 0;

        for (int i = 0; i < count; i++)
        {
            var proj = PoolManager.Instance.codecBlastPool.GetPoolObject();
            angle = isRight ? angle : -angle;
            var newDir = Quaternion.AngleAxis(angle, Vector3.up) * dir;

            if (proj.TryGetComponent<Projectile>(out var pro))
            {
                proj.transform.position = transform.position;
                pro.Init(GameManager.Instance.Player, newDir, damage * damageConst, 20);
                pro.Fire();
            }
            angle += 15f;
            yield return YieldInstructionCache.WaitForSeconds(0.3f);
        };
        yield return null;
    }

    public override void Upgrade()
    {
        base.Upgrade();

        switch (skill_Level)
        {
            case 1:
                damage = 15;
                count = 1;
                break;
            case 2:
                damage = 20;
                count = 3;
                break;
            case 3:
                damage = 20;
                count = 5;
                break;
            case 4:
                damage = 20;
                count = 9;
                break;
            case 5:
                damage = 20;
                count = 16;
                break;
        }
    }
}
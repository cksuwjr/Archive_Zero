using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodecBlast : Skill
{
    public float damage = 40;
    public int count = 1;

    protected override IEnumerator Cast_()
    {
        // origin
        var damageConst = GameManager.Instance.Player.GetComponent<Status>().AttackPower;
        GameManager.Instance.Player.GetComponent<PlayerController>().GetCC(1f);
        GameManager.Instance.Player.GetComponentInChildren<Animator>().SetTrigger("EzAttack");
        var dir = transform.forward;
        yield return YieldInstructionCache.WaitForSeconds(0.85f);

        //bool isRight = false;
        float angle = 0;
        angle = 3.5f * count;

        for (int i = 0; i < count; i++)
        {
            var proj = PoolManager.Instance.codecBlastPool.GetPoolObject();
            var newDir = Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.up) * dir;


            if (proj.TryGetComponent<Projectile>(out var pro))
            {
                proj.transform.position = transform.position;
                pro.Init(GameManager.Instance.Player, newDir, (damage * damageConst) / count, 20);
                pro.Fire();
            }
            yield return YieldInstructionCache.WaitForSeconds(0.3f - (0.015f * i));
            //isRight = !isRight;
            //angle += (5f - 0.1f * count);
        };
        yield return null;

        //// Ezreal
        //var damageConst = GameManager.Instance.Player.GetComponent<Status>().AttackPower;
        //GameManager.Instance.Player.GetComponent<PlayerController>().GetCC(1f);
        //GameManager.Instance.Player.GetComponentInChildren<Animator>().SetTrigger("EzAttack");
        //var dir = transform.forward;
        //yield return YieldInstructionCache.WaitForSeconds(0.85f);

        ////bool isRight = false;
        //float angle = 0;
        //angle = 3.5f * count;

        //for (int i = 0; i < count; i++)
        //{
        //    var proj = PoolManager.Instance.codecBlastPool.GetPoolObject();
        //    var newDir = Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.up) * dir;


        //    if (proj.TryGetComponent<Projectile>(out var pro))
        //    {
        //        proj.transform.position = transform.position;
        //        pro.Init(GameManager.Instance.Player, newDir, damage * damageConst, 20);
        //        pro.Fire();
        //    }
        //    yield return YieldInstructionCache.WaitForSeconds(0.3f - (0.015f * i));
        //    //isRight = !isRight;
        //    //angle += (5f - 0.1f * count);
        //};
        //yield return null;

        // Russian
        //var damageConst = GameManager.Instance.Player.GetComponent<Status>().AttackPower;
        //GameManager.Instance.Player.GetComponent<PlayerController>().GetCC(1f);
        //GameManager.Instance.Player.GetComponentInChildren<Animator>().SetTrigger("EzAttack");
        //var dir = transform.forward;
        //yield return YieldInstructionCache.WaitForSeconds(0.85f / damageConst);

        ////bool isRight = false;
        //float angle = 0;
        //angle = 3.5f * count;

        //for (int i = 0; i < count; i++)
        //{
        //    var proj = PoolManager.Instance.codecBlastPool.GetPoolObject();
        //    //var newDir = Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.up) * dir;


        //    if (proj.TryGetComponent<Projectile>(out var pro))
        //    {
        //        proj.transform.position = transform.position;
        //        pro.Init(GameManager.Instance.Player, dir, damage * damageConst, 20);
        //        pro.Fire();
        //    }
        //    yield return YieldInstructionCache.WaitForSeconds(0.3f - (0.015f * i));
        //    //isRight = !isRight;
        //    //angle += (5f - 0.1f * count);
        //};
        //yield return null;
    }

    public override void Upgrade()
    {
        base.Upgrade();

        //switch (skill_Level)
        //{
        //    case 1:
        //        damage = 40;
        //        count = 1;
        //        break;
        //    case 2:
        //        damage = 20;
        //        count = 3;
        //        break;
        //    case 3:
        //        damage = 21f;
        //        count = 5;
        //        break;
        //    case 4:
        //        damage = 27.77f;
        //        count = 9;
        //        break;
        //    case 5:
        //        damage = 31.25f;
        //        count = 16;
        //        break;
        //}

        // Ezreal
        switch (skill_Level)
        {
            case 1:
                damage = 40;
                count = 1;
                break;
            case 2:
                damage = 105;
                count = 3;
                break;
            case 3:
                damage = 250f;
                count = 5;
                break;
            case 4:
                damage = 600f;
                count = 9;
                break;
            case 5:
                damage = 1000f;
                count = 16;
                break;
        }
    }
    // 40  60  100  180  320
    // => 40  60  105  250  500
}
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Entity
{
    private IMove movement;
    private IInputHandle inputHandle;
    private WeaponManager weaponManager;

    private int level = 0;
    private float[] expNeeds = {5,10,20,35,50,80,120,150,200,250,300,400,500,600,700,800,900,1000,1000 };

    private bool hittable = true;

    public Action<float, float> OnChangeExp;
    public Action<float, float, float> OnChangeArchive;
    public Action<float, float, float> OnChangeHp;


    public float archive = 0f;
    public const float maxArchive = 100f;

    private bool isBinded = false;


    protected override void DoAwake()
    {
        TryGetComponent<IMove>(out movement);
        TryGetComponent<IInputHandle>(out inputHandle);
        TryGetComponent<WeaponManager>(out weaponManager);
    }

    public override void GetDamage(Entity attacker, float damage, float knockbackTime = 3f)
    {
        if (!hittable) return;

        var prevHp = status.HP;
        base.GetDamage(this, damage);

        OnChangeHp?.Invoke(prevHp, status.HP, status.MaxHP);
        StartCoroutine("Invinsible");
    }


    protected override void Die()
    {
        GameManager.Instance.Player.GetComponentInChildren<Animator>().SetTrigger("Die");
        base.Die();
    }


    private void Update()
    {
        if (!isBinded)
        {
            movement?.Move(inputHandle.GetInput());

            if (inputHandle.GetKeyInput(KeyInput.Fire1))
                weaponManager.Fire(KeyInput.Fire1);
            if (inputHandle.GetKeyInput(KeyInput.Fire2))
                weaponManager.Fire(KeyInput.Fire2);
            if (inputHandle.GetKeyInput(KeyInput.Fire3))
                weaponManager.Fire(KeyInput.Fire3);
            if (inputHandle.GetKeyInput(KeyInput.Fire4))
                weaponManager.Fire(KeyInput.Fire4);
            if (inputHandle.GetKeyInput(KeyInput.Fire5))
                weaponManager.Fire(KeyInput.Fire5);
        }
        //weapon.Fire();
    }

    public void GetCC(float time)
    {
        StartCoroutine("CC", time);
    }

    public IEnumerator CC(float time)
    {
        isBinded = true;
        yield return YieldInstructionCache.WaitForSeconds(time);
        isBinded = false;
    }

    public void Attack()
    {
        
    }

    public void GetExp(float exp)
    {
        StartCoroutine("GetExpCo", exp);
        //var prevExp = status.Exp;
        //status.Exp += exp;
        //if (expNeeds[level] <= status.Exp)
        //{
        //    status.Exp -= expNeeds[level];
        //    LevelUp();
        //}
        //OnChangeExp?.Invoke(status.Exp, expNeeds[level]);
    }

    private IEnumerator GetExpCo(float exp)
    {
        float value = 0;
        while (value <= exp) {
            status.Exp += Time.deltaTime * 3;
            value += Time.deltaTime * 3;
            if (expNeeds[level] <= status.Exp)
            {
                status.Exp -= expNeeds[level];
                LevelUp();
            }
            yield return null;
            OnChangeExp?.Invoke(status.Exp, expNeeds[level]);
        }
    }

    public void GetHeal(float value)
    {
        var prevHp = status.HP;
        status.HP += value;
        if(status.HP >= status.MaxHP)
            status.HP = status.MaxHP;

        OnChangeHp?.Invoke(prevHp, status.HP, status.MaxHP);

    }

    public void GetArchive(float value)
    {
        var prevArchive = archive;
        archive += value;
        if (archive >= maxArchive)
        {
            archive = maxArchive;
            UIManager.Instance.FloatFillArchive();
        }
        OnChangeArchive?.Invoke(prevArchive, archive, maxArchive);
    }

    private void LevelUp()
    {
        level += 1;
        UIManager.Instance.SkillSelect();
    }


    private IEnumerator Invinsible()
    {
        hittable = false;
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        hittable = true;
    }
}
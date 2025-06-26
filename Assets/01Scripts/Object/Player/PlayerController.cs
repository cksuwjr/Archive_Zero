using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerController : Entity
{
    private IMove movement;
    private IInputHandle inputHandle;
    private WeaponManager weaponManager;

    private int level = 1;
    private float[] expNeeds = {10,20,30,40,50,60,70,80,90,100,100,100,100,100,100,100,100,100,100 };

    private bool hittable = true;

    public Action<float, float> OnChangeExp;
    public Action<float, float> OnChangeArchive;
    public Action<float, float> OnChangeHp;


    public float archive = 0f;
    public const float maxArchive = 100f;

    private bool isBinded = false;


    protected override void DoAwake()
    {
        TryGetComponent<IMove>(out movement);
        TryGetComponent<IInputHandle>(out inputHandle);
        TryGetComponent<WeaponManager>(out weaponManager);
    }

    public override void GetDamage(Entity attacker, float damage)
    {
        if (!hittable) return;

        base.GetDamage(this, damage);

        OnChangeHp?.Invoke(status.HP, status.MaxHP);
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
        status.Exp += exp;
        if (expNeeds[level] <= status.Exp)
        {
            status.Exp -= expNeeds[level];
            LevelUp();
        }
        OnChangeExp?.Invoke(status.Exp, expNeeds[level]);
    }

    public void GetHeal(float value)
    {
        status.HP += value;
        if(status.HP >= status.MaxHP)
            status.HP = status.MaxHP;

        OnChangeHp?.Invoke(status.HP, status.MaxHP);

    }

    public void GetArchive(float value)
    {
        archive += value;
        if (archive >= maxArchive)
            archive = maxArchive;
        OnChangeArchive?.Invoke(archive, maxArchive);
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
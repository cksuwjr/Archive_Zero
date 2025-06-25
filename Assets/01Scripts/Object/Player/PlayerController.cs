using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class PlayerController : Entity
{
    private IMove movement;
    private IInputHandle inputHandle;
    private WeaponManager weaponManager;

    private int level = 1;
    private float[] expNeeds = {10,20,30,40,50,60,70,80,90,100,100,100,100,100,100,100,100,100,100 };

    private bool hittable = true;

    public Action<float, float> OnChangeExp;

    protected override void DoAwake()
    {
        TryGetComponent<IMove>(out movement);
        TryGetComponent<IInputHandle>(out inputHandle);
        TryGetComponent<WeaponManager>(out weaponManager);
    }

    public override void GetDamage(float damage)
    {
        if (!hittable) return;

        base.GetDamage(damage);
        StartCoroutine("Invinsible");
    }

    private void Update()
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
        //weapon.Fire();
    }

    public void Attack()
    {
        
    }

    public void GetExp(float exp)
    {
        status.Exp += exp;
        if (expNeeds[level] < status.Exp)
        {
            status.Exp -= expNeeds[level];
            LevelUp();
        }
        OnChangeExp?.Invoke(status.Exp, expNeeds[level]);
    }

    private void LevelUp()
    {
        level += 1;

    }


    private IEnumerator Invinsible()
    {
        hittable = false;
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        hittable = true;
    }
}
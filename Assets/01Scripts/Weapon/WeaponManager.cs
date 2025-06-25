using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private bool fire1;
    private bool fire2;
    private bool fire3;
    private bool fire4;

    [SerializeField] private Skill basicAttack;

    [SerializeField] private Skill skill1;
    [SerializeField] private Skill skill2;
    [SerializeField] private Skill skill3;
    [SerializeField] private Skill skill4;


    public void Fire(KeyInput input)
    {
        switch (input)
        {
            case KeyInput.Fire1: fire1 = true; break;
            case KeyInput.Fire2: fire2 = true; break;
            case KeyInput.Fire3: fire3 = true; break;
            case KeyInput.Fire4: fire4 = true; break;
        }
    }

    private void FixedUpdate()
    {
        if (fire1)
        {
            Skill1();
            fire1 = false;
        }
        if (fire2)
        {
            Skill2();
            fire2 = false;
        }
        if (fire3)
        {
            Skill3();
            fire3 = false;
        }
        if (fire4)
        {
            Skill4();
            fire4 = false;
        }

        basicAttack.Cast();
    }

    private void Skill1()
    {
        skill1.Cast();

        Debug.Log("스킬1 사용");
    }

    private void Skill2()
    {
        skill2.Cast();

        Debug.Log("스킬2 사용");
    }

    private void Skill3()
    {
        skill3.Cast();

        Debug.Log("스킬3 사용");
    }

    private void Skill4()
    {
        skill4.Cast();

        Debug.Log("스킬4 사용");
    }
}

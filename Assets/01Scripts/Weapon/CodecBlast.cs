using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodecBlast : Skill
{
    public float damage = 15;
    public float duration = 3f;

    public Vector3 range = Vector3.one;

    protected override IEnumerator Cast_()
    {
        

        yield return null;
    }

    public override void Upgrade()
    {
        base.Upgrade();

        switch (skill_Level)
        {
            case 1:
                damage = 15;
                range = Vector3.one;
                duration = 3f;
                break;
            case 2:
                damage = 20;
                range = Vector3.one;
                duration = 3f;
                break;
            case 3:
                damage = 20;
                range = Vector3.one;
                duration = 5f;
                break;
            case 4:
                damage = 20;
                range = Vector3.one * 1.5f;
                duration = 5f;
                break;
            case 5:
                damage = 20;
                range = Vector3.one * 2.5f;
                duration = 5f;
                break;
        }
    }
}
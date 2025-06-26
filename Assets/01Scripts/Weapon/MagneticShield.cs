using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticShield : Skill
{
    public float damage = 0;
    public float duration = 3;

    [SerializeField] private GameObject barrierPrefab;
    [SerializeField] private GameObject barrierPrefab2;

    private GameObject prefabInstance;

    protected override IEnumerator Cast_()
    {
        var damageConst = GameManager.Instance.Player.GetComponent<Status>().AttackPower;
        if (prefabInstance == null)
        {
            prefabInstance = Instantiate(barrierPrefab, transform);
        }

        prefabInstance.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(duration);
        prefabInstance.SetActive(false);
    }

    public override void Upgrade()
    {
        if (skill_Level > 5) return;

        base.Upgrade();

        switch (skill_Level)
        {
            case 1:
                damage = 0;
                duration = 3f;
                break;
            case 2:
                damage = 0;
                duration = 3f;
                break;
            case 3:
                damage = 40;
                duration = 3f;
                prefabInstance?.SetActive(false);
                prefabInstance = Instantiate(barrierPrefab2, transform);
                prefabInstance.SetActive(false);
                break;
            case 4:
                damage = 40;
                duration = 3f;
                break;
            case 5:
                damage = 80;
                duration = 3f;
                break;
        }
    }
}
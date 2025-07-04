using System.Collections;
using UnityEngine;

// 3   6.75 10.5 14.25 18
// 60  135  210  285  360 dot
public class BlackholeGravitationalField : Skill
{
    public float damage = 0;
    public float duration = 5;

    [SerializeField] private GameObject blackholePrefab;

    private GameObject prefabInstance;

    protected override IEnumerator Cast_()
    {
        var damageConst = GameManager.Instance.Player.GetComponent<Status>().AttackPower;
        if (prefabInstance == null)
        {
            prefabInstance = Instantiate(blackholePrefab);
        }
        prefabInstance.transform.localScale = Vector3.one * ((2.125f + (0.375f * skill_Level)) * GetComponent<Status>().AttackPower);

        prefabInstance.SetActive(true);
        var pos = transform.position + transform.forward * 2;
        pos.y = -0.6f;
        prefabInstance.transform.position = pos;

        for (float i = 0; i < duration;)
        {
            var colliders = Physics.OverlapSphere(prefabInstance.transform.position, ((2.125f + (0.375f * skill_Level)) * GetComponent<Status>().AttackPower));
            for (int j = 0; j < colliders.Length; j++) {
                if (colliders[j].TryGetComponent<Monster>(out var monster))
                {
                    monster.GetDamage(GameManager.Instance.Player.GetComponent<Entity>(), damage, 0.01f);
                    CooltimeDecline(0.1f);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(0.25f);
            i += 0.25f;
        }
        prefabInstance.SetActive(false);
    }

    public override void Upgrade()
    {
        if (skill_Level > 5) return;


        if (prefabInstance == null)
            prefabInstance = Instantiate(blackholePrefab);
        prefabInstance.SetActive(false);


        base.Upgrade();

        switch (skill_Level)
        {
            case 1:
                damage = 3;
                duration = 5f;

                break;
            case 2:
                damage = 5f;
                duration = 5f;
                break;
            case 3:
                damage = 15;
                duration = 5f;
                break;
            case 4:
                damage = 35f;
                duration = 5f;
                break;
            case 5:
                damage = 60;
                duration = 5f;
                break;
        }


        // 40  60  100  180  320
        // 60  100  140  180  220
    }
}

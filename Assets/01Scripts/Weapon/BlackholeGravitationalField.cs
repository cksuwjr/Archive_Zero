using System.Collections;
using UnityEngine;

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

        prefabInstance.SetActive(true);
        var pos = transform.position;
        pos.y = -0.6f;
        prefabInstance.transform.position = pos;

        for (float i = 0; i < duration;)
        {
            i += 0.5f;
            var colliders = Physics.OverlapBox(prefabInstance.transform.position, Vector3.one * 0.5f);
            for (int j = 0; j < colliders.Length; j++) {
                if (colliders[j].TryGetComponent<Monster>(out var monster))
                    monster.GetDamage(GameManager.Instance.Player.GetComponent<Entity>(), damage);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
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
                damage = 5;
                duration = 5f;

                prefabInstance.transform.localScale = Vector3.one;
                break;
            case 2:
                damage = 7;
                duration = 5f;
                prefabInstance.transform.localScale = Vector3.one * 1.25f;
                break;
            case 3:
                damage = 9;
                duration = 5f;
                prefabInstance.transform.localScale = Vector3.one * 1.5f;
                break;
            case 4:
                damage = 12;
                duration = 5f;
                prefabInstance.transform.localScale = Vector3.one * 1.75f;
                break;
            case 5:
                damage = 15;
                duration = 5f;
                prefabInstance.transform.localScale = Vector3.one * 2f;
                break;
        }
    }
}

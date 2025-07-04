using System.Collections;
using UnityEngine;

public class Dibbo : Skill
{
    public float speed = 3;

    [SerializeField] private GameObject dibboPrefab;

    private GameObject prefabInstance;
    private DibboAttack attack;
    private DibboRotater rotater;

    protected override IEnumerator Cast_()
    {
        var damageConst = GameManager.Instance.Player.GetComponent<Status>().AttackPower;

        if (prefabInstance == null)
        {
            prefabInstance = Instantiate(dibboPrefab, GameObject.Find("GroundPlane").transform);
            attack = prefabInstance.GetComponentInChildren<DibboAttack>();
            prefabInstance.TryGetComponent<DibboRotater>(out rotater);
        }

        prefabInstance.SetActive(true);
        prefabInstance.transform.localPosition = Vector3.zero;



        rotater.speed = (85f + skill_Level * 50) * 3f;

        float duration = 3f;
        for (float i = 0; i < duration;)
        {

            var colliders = Physics.OverlapSphere(transform.position, 6);
            for (int j = 0; j < colliders.Length; j++)
            {
                if (colliders[j].TryGetComponent<Item>(out var item))
                {
                    item.AcquireTo(GameManager.Instance.Player.GetComponent<PlayerController>());
                }
            }
            i += Time.deltaTime;
            yield return null;
        }
        //yield return YieldInstructionCache.WaitForSeconds(3f);
        rotater.speed = 85f + skill_Level * 50;
    }

    public override void Upgrade()
    {
        if (skill_Level > 5) return;

        base.Upgrade();

        if (prefabInstance == null)
        {
            prefabInstance = Instantiate(dibboPrefab, GameObject.Find("GroundPlane").transform);
            attack = prefabInstance.GetComponentInChildren<DibboAttack>();
            prefabInstance.TryGetComponent<DibboRotater>(out rotater);
        }
        prefabInstance.SetActive(true);
        prefabInstance.transform.localPosition = Vector3.zero;


        switch (skill_Level)
        {
            case 1:
                attack.SetDamage(20);
                break;
            case 2:
                attack.SetDamage(30);
                break;
            case 3:
                attack.SetDamage(50);
                break;
            case 4:
                attack.SetDamage(100);
                break;
            case 5:
                attack.SetDamage(200);
                break;
        }
        rotater.speed = 85f + skill_Level * 50;

    }
}

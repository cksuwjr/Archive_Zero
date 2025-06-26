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
        yield return null;
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
                attack.SetDamage(30);
                rotater.speed = 135f;
                break;
            case 2:
                attack.SetDamage(40);
                rotater.speed = 185f;
                break;
            case 3:
                attack.SetDamage(50);
                rotater.speed = 235f;
                break;
            case 4:
                attack.SetDamage(60);
                rotater.speed = 285f;
                break;
            case 5:
                attack.SetDamage(70);
                rotater.speed = 335f;
                break;
        }
    }
}

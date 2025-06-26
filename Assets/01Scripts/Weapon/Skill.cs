using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    protected Entity controller;
    public int skill_Level;
    [SerializeField] protected float cooltime;
    protected float ReusableWaitTime;
    protected bool Castable;
    protected bool auto = false;

    public Image skillImage;
    [SerializeField] protected Image cooltimeBox;
    [SerializeField] protected TextMeshProUGUI cooltimeText;

    public string skillName;
    public string inform;


    [SerializeField] private AudioClip sound;

    public void Init()
    {
        if (!TryGetComponent<Entity>(out controller))
            Debug.LogError("�ش� ��� Entity.cs �� �����ϴ�!");

        if (cooltime == 0)
            cooltime = 1f;

        Castable = true;

        if (skill_Level == 0)
            CooltimeUI(1, 0);
        else
            CooltimeUI(0, 0);

        if (auto)
            Cast();
    }

    public bool Cast()
    {
        if (Castable && skill_Level > 0)
        {
            StopCast(); // ������ ����Ǵ� ���� ��ų �ߴ�
            StartCoroutine("Cast_");
            StartCoroutine(Cooltime());
            if(sound)
                SoundManager.Instance.PlaySound(sound);
            return true;
        }
        else
        {
            Debug.Log("�ش� ��ų�� ������ð� �� �Դϴ�!");
            return false;
        }
    }
    public void StopCast()
    {
        StopCoroutine("Cast_");
    }
    protected abstract IEnumerator Cast_();
    protected IEnumerator Cooltime()
    {
        Castable = false;
        ReusableWaitTime = cooltime;
        CooltimeUI(1, 0);
        while (ReusableWaitTime > 0)
        {
            ReusableWaitTime -= Time.deltaTime;
            CooltimeUI(ReusableWaitTime / cooltime, ReusableWaitTime + 1);
            yield return null;
        }
        CooltimeUI(0, 0);
        ReusableWaitTime = 0;
        Castable = true;
        if (auto)
            Cast();
    }

    public void CooltimeUI(float imagefill, float textvalue)
    {
        if (cooltimeBox)
            cooltimeBox.fillAmount = imagefill;
        if (cooltimeText)
        {
            cooltimeText.text = ((int)textvalue).ToString();
            if (textvalue <= 0)
                cooltimeText.text = "";
        }
    }
    public void CooltimeDecline(float howmuch)
    {
        ReusableWaitTime -= howmuch;
        CooltimeUI(ReusableWaitTime / cooltime, ReusableWaitTime + 1);
    }

    public virtual void Upgrade()
    {
        skill_Level += 1;

        if(skill_Level == 1)
            CooltimeUI(0, 0);
    }

    public void SetAuto(bool auto)
    {
        this.auto = auto;
    }
}

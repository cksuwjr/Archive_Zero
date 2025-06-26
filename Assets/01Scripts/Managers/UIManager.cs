using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SingletonDestroy<UIManager>, IManager
{
    private GameObject archive;

    private Image expBar;
    private TextMeshProUGUI archieveText;
    private Image archiveBar;

    private TextMeshProUGUI timeText;

    private GameObject introBackground;
    private GameObject playGuide;
    private TextMeshProUGUI contentText;

    private GameObject pages;
    private int page = 0;
    private TextMeshProUGUI pageText;

    private GameObject selectPannel;

    private GameObject exitUI;

    private GameObject outro;

    public event Action OnGuideEnd;

    private GameObject canvas;
    public void Init()
    {
        canvas = GameObject.Find("Canvas");
        GameManager.Instance.Player.TryGetComponent<PlayerController>(out var player);


        archive = canvas.transform.GetChild(0).gameObject;
        //
        canvas.transform.GetChild(1).GetChild(1).GetChild(0).TryGetComponent<Image>(out expBar);
        //
        canvas.transform.GetChild(2).GetChild(0).TryGetComponent<Image>(out archiveBar);
        canvas.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out archieveText);



        if (player)
        {
            player.OnChangeExp += (exp, maxExp) => { expBar.fillAmount = exp / maxExp; };
            player.OnChangeArchive += (archive, maxArchive) => 
            { 
                archiveBar.fillAmount = archive / maxArchive;
                archieveText.text = $"{Mathf.Floor((archive / maxArchive) * 100)}%";
            };
        }


        //
        if (canvas.transform.GetChild(3).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out timeText))
            TimeManager.Instance.OnTimeChanged += (time) =>
            {
                timeText.text = $"{(int)time / 60:00}:{time % 60:00}";
            };


        



        
        var hpPannel = canvas.transform.GetChild(4).gameObject;
        if (hpPannel.transform.GetChild(0).TryGetComponent<Image>(out var hpBar))
            player.OnChangeHp += (hp, maxhp) => hpBar.fillAmount = hp / maxhp;

        selectPannel = canvas.transform.GetChild(5).gameObject;

        selectPannel.SetActive(false);



        //
        introBackground = canvas.transform.GetChild(6).gameObject;

        introBackground.transform.GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out contentText);
        introBackground.transform.GetChild(0).GetChild(3).TryGetComponent<TextMeshProUGUI>(out pageText);

        //
        playGuide = canvas.transform.GetChild(7).gameObject;

        pages = playGuide.transform.GetChild(0).GetChild(0).gameObject;

        if (playGuide.transform.GetChild(0).GetChild(1).TryGetComponent<Button>(out var nextBtn))
            nextBtn.onClick.AddListener(NextGuidePage);
        if (playGuide.transform.GetChild(0).GetChild(2).TryGetComponent<Button>(out var prevBtn))
            prevBtn.onClick.AddListener(PrevGuidePage);

        //
        exitUI = canvas.transform.GetChild(8).gameObject;

        if (exitUI.transform.GetChild(1).TryGetComponent<Button>(out var exitBtn))
            exitBtn.onClick.AddListener(() =>
            {
                LoadingSceneManager.SetNextScene("TitleScene");
                SceneManager.LoadScene("LoadingScene");
            });

        player.OnDie += () => exitUI.SetActive(true);

        outro = canvas.transform.GetChild(9).gameObject;
    }

    public void SetArchive(bool tf)
    {
        archive.SetActive(tf);
    }

    public void SetStoryPannel(string content)
    {
        contentText.text = content;
    }

    public void OpenStoryPannel()
    {
        introBackground.SetActive(true);
    }

    public void SetStoryPage(int page, int maxPage)
    {
        pageText.text = $"{page} / {maxPage}";
    }

    public void CloseStoryPannel()
    {
        introBackground.SetActive(false);
    }

    public void OpenGuidPage()
    {
        playGuide.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(4).gameObject.SetActive(false);


    }

    public void GuidePage(int index)
    {
        for (int i = 0; i < pages.transform.childCount; i++)
        {
            if(i == index)
                pages.transform.GetChild(i).gameObject.SetActive(true);
            else
                pages.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void PrevGuidePage()
    {
        if (page > 0)
            GuidePage(--page);
    }

    public void NextGuidePage()
    {
        if (page + 1 < pages.transform.childCount)
            GuidePage(++page);
        else
            CloseGuidePage();
    }

    public void CloseGuidePage()
    {
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        canvas.transform.GetChild(4).gameObject.SetActive(true);
        playGuide.SetActive(false);
        OnGuideEnd?.Invoke();
        OnGuideEnd = null;
    }

    public void SkillSelect()
    {
        TimeManager.Instance.Stop();

        var wm = GameManager.Instance.Player.GetComponent<WeaponManager>();

        for (int i = 0; i < selectPannel.transform.childCount; i++)
            selectPannel.transform.GetChild(i).gameObject.SetActive(false);


        SkillCard card;
        GameObject g;
        Skill skill1;
        Skill skill2;
        Skill skill3;
        Skill skill4;
        Skill skill5;
        Button btn;

        skill1 = wm.GetSkillBasic();
        if (skill1.skill_Level < 5)
        {
            g = selectPannel.transform.GetChild(0).gameObject;
            g.SetActive(true);
            if (g.TryGetComponent<SkillCard>(out card))
                card.Init(skill1.skillImage.sprite ,skill1.skillName, skill1.inform);

            if(g.TryGetComponent<Button>(out btn))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    skill1.Upgrade();
                    TimeManager.Instance.Resume();
                    CloseSelectPannel();
                }
                );
            }

        }

        skill2 = wm.GetSkillQ();
        if (skill2.skill_Level < 5)
        {
            g = selectPannel.transform.GetChild(1).gameObject;
            g.SetActive(true);
            if (g.TryGetComponent<SkillCard>(out card))
                card.Init(skill2.skillImage.sprite ,skill2.skillName, skill2.inform);
            if (g.TryGetComponent<Button>(out btn))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    skill2.Upgrade();
                    TimeManager.Instance.Resume();
                    CloseSelectPannel();
                }
                );
            }
        }

        skill3 = wm.GetSkillW();
        if (skill3.skill_Level < 5)
        {
            g = selectPannel.transform.GetChild(2).gameObject;
            g.SetActive(true);
            if (g.TryGetComponent<SkillCard>(out card))
                card.Init(skill3.skillImage.sprite ,skill3.skillName, skill3.inform);
            if (g.TryGetComponent<Button>(out btn))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    skill3.Upgrade();
                    TimeManager.Instance.Resume();
                    CloseSelectPannel();
                }
                );
            }
        }

        skill4 = wm.GetSkillE();
        if (skill4.skill_Level < 5)
        {
            g = selectPannel.transform.GetChild(3).gameObject;
            g.SetActive(true);
            if (g.TryGetComponent<SkillCard>(out card))
                card.Init(skill4.skillImage.sprite ,skill4.skillName, skill4.inform);
            if (g.TryGetComponent<Button>(out btn))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    skill4.Upgrade();
                    TimeManager.Instance.Resume();
                    CloseSelectPannel();
                }
                );
            }
        }

        skill5 = wm.GetSkillR();
        if (skill5.skill_Level < 5)
        {
            g = selectPannel.transform.GetChild(4).gameObject;
            g.SetActive(true);
            if (g.TryGetComponent<SkillCard>(out card))
                card.Init(skill5.skillImage.sprite ,skill5.skillName, skill5.inform);
            if (g.TryGetComponent<Button>(out btn))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    skill5.Upgrade();
                    TimeManager.Instance.Resume();
                    CloseSelectPannel();
                }
                );
            }
        }

        if (wm.GetSkillBasic().skill_Level < 5)
            selectPannel.transform.GetChild(0).gameObject.SetActive(true);

        if (wm.GetSkillQ().skill_Level < 5)
            selectPannel.transform.GetChild(1).gameObject.SetActive(true);

        if (wm.GetSkillW().skill_Level < 5)
            selectPannel.transform.GetChild(2).gameObject.SetActive(true);

        if (wm.GetSkillE().skill_Level < 5)
            selectPannel.transform.GetChild(3).gameObject.SetActive(true);

        if (wm.GetSkillR().skill_Level < 5)
            selectPannel.transform.GetChild(4).gameObject.SetActive(true);

        selectPannel.SetActive(true);
    }

    private void CloseSelectPannel()
    {
        selectPannel.SetActive(false);
    }
    
    public void OpenOutro()
    {
        outro.SetActive(true);
    }

    public void CloseOutro()
    {
        LoadingSceneManager.SetNextScene("TitleScene");
        SceneManager.LoadScene("LoadingScene");
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonDestroy<UIManager>, IManager
{
    private GameObject introBackground;
    private GameObject playGuide;
    private TextMeshProUGUI contentText;

    private GameObject pages;
    private int page = 0; 

    public event Action OnGuideEnd;

    public void Init()
    {
        var canvas = GameObject.Find("Canvas");
        introBackground = canvas.transform.GetChild(0).gameObject;
        
        introBackground.transform.GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out contentText);

        playGuide = canvas.transform.GetChild(1).gameObject;

        pages = playGuide.transform.GetChild(0).GetChild(0).gameObject;

        if (playGuide.transform.GetChild(0).GetChild(1).TryGetComponent<Button>(out var nextBtn))
            nextBtn.onClick.AddListener(NextGuidePage);
        if (playGuide.transform.GetChild(0).GetChild(2).TryGetComponent<Button>(out var prevBtn))
            prevBtn.onClick.AddListener(PrevGuidePage);
    }

    public void SetStoryPannel(string content)
    {
        contentText.text = content;
    }

    public void OpenStoryPannel()
    {
        introBackground.SetActive(true);
    }

    public void CloseStoryPannel()
    {
        introBackground.SetActive(false);
    }

    public void OpenGuidPage()
    {
        playGuide.SetActive(true);
    }

    public void GuidePage(int index)
    {
        Debug.Log(page);
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
        playGuide.SetActive(false);
        OnGuideEnd?.Invoke();
        OnGuideEnd = null;
    }
}

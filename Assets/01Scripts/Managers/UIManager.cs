using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : Singleton<UIManager>, IManager
{
    private GameObject introBackground;
    private TextMeshProUGUI contentText;

    public void Init()
    {
        introBackground = GameObject.Find("Canvas").transform.GetChild(0).GetChild(0).gameObject;
        introBackground.transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out contentText);
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    private TextMeshProUGUI clickText;

    private GameObject menu;
    private GameObject settingPannel;
    private GameObject exitPannel;

    private TextMeshProUGUI bgmText;
    private TextMeshProUGUI fxText;

    private Slider fxSlider;
    private Slider bgmSlider;

    public void Init()
    {
        var ui = GameObject.Find("Canvas");

        
        ui.transform.GetChild(0).GetChild(1).TryGetComponent<TextMeshProUGUI>(out clickText);
        var clickUI = ui.transform.GetChild(0).GetChild(2).gameObject;
        menu = ui.transform.GetChild(0).GetChild(3).gameObject;


        if (clickUI.TryGetComponent<Button>(out var btn))
            btn.onClick.AddListener(() => { menu.SetActive(true); clickText.gameObject.SetActive(false); StopCoroutine("BloorText"); clickUI.SetActive(false); });


        if(menu.transform.GetChild(0).TryGetComponent<Button>(out var btnStart))
        {
            btnStart.onClick.AddListener(OnClickBtnStart);
        }
        if (menu.transform.GetChild(1).TryGetComponent<Button>(out var btnShop))
        {
            btnShop.onClick.AddListener(OnClickBtnShop);
        }
        if (menu.transform.GetChild(2).TryGetComponent<Button>(out var btnSetting))
        {
            btnSetting.onClick.AddListener(OnClickBtnSetting);
        }
        if (menu.transform.GetChild(3).TryGetComponent<Button>(out var btnExit))
        {
            btnExit.onClick.AddListener(OnClickBtnExit);
        }
        //////////////////////////////////////////////////////////////////////////////

        settingPannel = ui.transform.GetChild(1).gameObject;

        var bgm = settingPannel.transform.GetChild(0).GetChild(3);
        bgm.GetChild(1).TryGetComponent<TextMeshProUGUI>(out bgmText);

        if (bgm.GetChild(2).TryGetComponent<Slider>(out bgmSlider))
        {
            bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume", 1);
            bgmText.text = $"{Mathf.Floor(bgmSlider.value * 100)}%";
            bgmSlider.onValueChanged.AddListener(OnChangeBGMSlider);
        }

        var fx = settingPannel.transform.GetChild(0).GetChild(4);
        fx.GetChild(1).TryGetComponent<TextMeshProUGUI>(out fxText);

        if (fx.GetChild(2).TryGetComponent<Slider>(out fxSlider))
        {
            fxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1);
            fxText.text = $"{Mathf.Floor(fxSlider.value * 100)}%";
            fxSlider.onValueChanged.AddListener(OnChangeFXSlider);
        }


        var yesBtn = settingPannel.transform.GetChild(0).GetChild(5);
        if (yesBtn.TryGetComponent<Button>(out var yesBtnOnClick))
        {
            yesBtnOnClick.onClick.AddListener(OnClickSettingApply);
        }

        var noBtn = settingPannel.transform.GetChild(0).GetChild(6);

        if (noBtn.TryGetComponent<Button>(out var noBtnOnClick))
        {
            noBtnOnClick.onClick.AddListener(OnClickSettingCancel);
        }

        /////////////////////////////////////////////////////////////////////////

        exitPannel = ui.transform.GetChild(2).gameObject;

        var exitYes = exitPannel.transform.GetChild(3);

        if (exitYes.TryGetComponent<Button>(out var exitYesBtnOnClick))
        {
            exitYesBtnOnClick.onClick.AddListener(() => { Application.Quit(); });
        }
        var exitNo = exitPannel.transform.GetChild(4);

        if (exitNo.TryGetComponent<Button>(out var exitNoBtnOnClick))
        {
            exitNoBtnOnClick.onClick.AddListener(() => { exitPannel.SetActive(false); });
        }


        StartCoroutine("BloorText");
    }

    private void OnChangeFXSlider(float arg0)
    {
        fxText.text = $"{Mathf.Floor(arg0 * 100)}%";
        SoundManager.Instance.ChangeSFXVolume(arg0);

    }

    private void OnChangeBGMSlider(float arg0)
    {
        bgmText.text = $"{Mathf.Floor(arg0 * 100)}%";
        SoundManager.Instance.ChangeBGMVolume(arg0);
    }

    private void OnClickSettingApply()
    {
        SoundManager.Instance.SaveSoundValue();
        Debug.Log(SoundManager.Instance.sfxVolume);
        Debug.Log("적용");
        settingPannel.SetActive(false);
    }

    private void OnClickSettingCancel()
    {
        Debug.Log("적용취소");
        settingPannel.SetActive(false);
    }

    private void OnClickBtnExit()
    {
        exitPannel.SetActive(true);
    }

    private void OnClickBtnSetting()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume", 1);
        bgmText.text = $"{Mathf.Floor(bgmSlider.value * 100)}%";



        fxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1);
        fxText.text = $"{Mathf.Floor(fxSlider.value * 100)}%";

        settingPannel.SetActive(true);
    }

    private void OnClickBtnShop()
    {
        Debug.Log("Shop");
    }

    private void OnClickBtnStart()
    {
        LoadingSceneManager.SetNextScene("GameScene");
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator BloorText()
    {
        float timer = 0f;
        Color color = Color.white;
        while (timer < 1f)
        {
            color.a = timer / 1f;
            clickText.color = color;
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0;
        while (timer < 1f)
        {
            color.a = 1 - (timer / 1f);
            clickText.color = color;
            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine("BloorText");
    }

}

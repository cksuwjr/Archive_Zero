using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    private Image skillImage;
    private TextMeshProUGUI skillText;
    private TextMeshProUGUI skillDesk;

    private void Awake()
    {
        transform.GetChild(0).TryGetComponent<Image>(out skillImage);
        transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out skillText);
        transform.GetChild(2).TryGetComponent<TextMeshProUGUI>(out skillDesk);
    }

    public void Init(Sprite sprite, string title, string desk)
    {
        skillImage.sprite = sprite;
        skillText.text = title;
        skillDesk.text = desk;
    }
}

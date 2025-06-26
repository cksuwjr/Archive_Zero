using UnityEngine;
using UnityEngine.UI; // 추가
using System.Collections;

public class csDemoSceneCode : MonoBehaviour
{
    public string[] EffectNames;
    public string[] Effect2Names;
    public Transform[] Effect;
    public Text Text1; // 여기 수정됨

    int i = 0;
    int a = 0;

    void Start()
    {
        Instantiate(Effect[i], new Vector3(0, 5, 0), Quaternion.identity);
    }

    void Update()
    {
        Text1.text = (i + 1) + ":" + EffectNames[i];

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (i <= 0)
                i = 99;
            else
                i--;

            PlayEffect();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (i < 99)
                i++;
            else
                i = 0;

            PlayEffect();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayEffect();
        }
    }

    void PlayEffect()
    {
        for (a = 0; a < Effect2Names.Length; a++)
        {
            if (EffectNames[i] == Effect2Names[a])
            {
                Instantiate(Effect[i], new Vector3(0, 0.2f, 0), Quaternion.identity);
                return;
            }
        }

        Instantiate(Effect[i], new Vector3(0, 5, 0), Quaternion.identity);
    }
}

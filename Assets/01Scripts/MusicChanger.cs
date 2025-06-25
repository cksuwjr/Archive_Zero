using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public void ChangeMusic(AudioClip clip)
    {
        SoundManager.Instance.ChangeBGM(clip);
    }
}

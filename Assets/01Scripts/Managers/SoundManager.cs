using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>, IManager
{
    private SoundObject BGMAudioObject;
    private float current, percent;

    public AudioClip BGM;

    public float bgmVolume = 1f;
    public float sfxVolume = 1f;

    private SoundObject prevAudio;

    public void Init()
    {
        GetSoundValue();

        if(BGM != null)
            ChangeBGM(BGM);
    }


    public void StopSound()
    {
        if(prevAudio)
            prevAudio.ReturnToPool();
        //for (int i = 0; i < PoolManager.Instance.soundPool.transform.childCount; i++)
        //{
        //    Transform t = PoolManager.Instance.soundPool.transform.GetChild(i);

        //    if (t.TryGetComponent<SoundObject>(out var sound))
        //    {
        //        if (sound == BGMAudioObject)
        //            continue;
        //        sound.ReturnToPool();
        //    }
        //}
    }


    public void ChangeBGMVolume(float value)
    {
        bgmVolume = value;
        if(BGMAudioObject)
            BGMAudioObject.AudioSource.volume = bgmVolume;
    }

    public void ChangeSFXVolume(float value)
    {
        sfxVolume = value;

        for (int i = 0; i < PoolManager.Instance.soundPool.transform.childCount; i++) 
        {
            Transform t = PoolManager.Instance.soundPool.transform.GetChild(i);
            
            if(t.TryGetComponent<SoundObject>(out var sound)) 
            {
                if (sound == BGMAudioObject)
                    continue;
                sound.AudioSource.volume = sfxVolume;
                prevAudio = sound;
            }
        }
    }

    public void ChangeBGM(AudioClip audioClip)
    {
        if (!BGMAudioObject)
        {
            BGMAudioObject = PlaySound(audioClip, true);
            BGMAudioObject.AudioSource.loop = true;
            BGMAudioObject.AudioSource.pitch = 1f;
            BGMAudioObject.name = "BGM Object";

        }
        else
            StartCoroutine("ChangeBGMClip", audioClip);
    }

    public SoundObject PlaySound(AudioClip audioClip, bool imortal = false)
    {
        if (PoolManager.Instance.soundPool.GetPoolObject().TryGetComponent<SoundObject>(out SoundObject soundObject))
        {
            soundObject.Init(audioClip, imortal);
            return soundObject;
        }
        return null;
    }

    IEnumerator ChangeBGMClip(AudioClip newClip)
    {
        current = percent = 0f;

        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / 1.0f;
            BGMAudioObject.AudioSource.volume = Mathf.Lerp(bgmVolume, 0f, percent);
            yield return null;
        }

        BGMAudioObject.AudioSource.clip = newClip;
        BGMAudioObject.AudioSource.Play();
        current = percent = 0f;

        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / 1.0f;
            BGMAudioObject.AudioSource.volume = Mathf.Lerp(0f, bgmVolume, percent);
            yield return null;
        }
    }

    public void GetSoundValue()
    {
        bgmVolume = PlayerPrefs.GetFloat("bgmVolume", 1);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1);
    }

    public void SaveSoundValue()
    {
        PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }

}
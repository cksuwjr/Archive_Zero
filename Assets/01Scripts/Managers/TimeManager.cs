using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonDestroy<TimeManager>
{
    public Action<float> OnTimeChanged;
    private float time;

    public void StartTimer(int minute, int second)
    {
        StartCoroutine("SetTimer", minute * 60 + second);
    }

    public void StartTimer(int second)
    {
        StartCoroutine("SetTimer", second);
    }

    private IEnumerator SetTimer(int until)
    {
        time = 0;
        while (time < until)
        {
            yield return YieldInstructionCache.WaitForSeconds(1f);
            time++;
            OnTimeChanged?.Invoke(time);
        }
    }

    public void Stop()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
}

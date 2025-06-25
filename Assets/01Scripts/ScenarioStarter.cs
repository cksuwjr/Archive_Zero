using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScenarioStarter : MonoBehaviour
{
    public UnityEvent OnStart;
    public UnityEvent OnStoryEnd;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance.isStoryLoad)
        {
            OnStart?.Invoke();
            ScenarioManager.Instance.OnStoryEnd.AddListener(() => UIManager.Instance.OpenGuidPage());
            UIManager.Instance.OnGuideEnd += () => StoryEnd();
        }
        else
            DataManager.Instance.OnStoryLoaded += () =>
            {
                OnStart?.Invoke();
                ScenarioManager.Instance.OnStoryEnd.AddListener(() => UIManager.Instance.OpenGuidPage());
                UIManager.Instance.OnGuideEnd += () => StoryEnd();
            };
    }

    private void StoryEnd()
    {
        OnStoryEnd?.Invoke();
    }
}

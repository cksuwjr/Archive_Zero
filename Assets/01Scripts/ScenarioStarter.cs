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
            if (UIManager.Instance)
            {


                ScenarioManager.Instance?.OnStoryEnd.AddListener(() => UIManager.Instance.OpenGuidPage());
                UIManager.Instance.OnGuideEnd += () => StoryEnd();
            }
        }
        else
            DataManager.Instance.OnStoryLoaded += () =>
            {
                OnStart?.Invoke();
                if (UIManager.Instance)
                {
                    ScenarioManager.Instance?.OnStoryEnd.AddListener(() => UIManager.Instance.OpenGuidPage());
                    UIManager.Instance.OnGuideEnd += () => StoryEnd();
                }
            };
    }

    private void StoryEnd()
    {
        OnStoryEnd?.Invoke();
    }
}

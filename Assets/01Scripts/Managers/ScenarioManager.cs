using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScenarioManager : SingletonDestroy<ScenarioManager>, IManager
{
    private List<StoryData> stories;
    private bool texting = false;
    private int page = 0;

    public UnityEvent OnStoryEnd;

    public void Init()
    {
    }

    public void NextPage()
    {
        if (texting)
        {
            texting = false;
            return;
        }

        if (stories == null) return;

        if (stories.Count < page + 1)
        {
            StopStory();
            return;
        }

        if (stories[page].sort == stories[page + 1].sort)
        {
            page++;
            LoadPage();
        }
        else
            StopStory();
    }

    public void StartStory(int num)
    {
        stories = DataManager.Instance.GetStory();

        page = num;

        LoadPage();
        UIManager.Instance.OpenStoryPannel();
    }

    public void StopStory()
    {
        SoundManager.Instance.StopSound();
        UIManager.Instance.CloseStoryPannel();

        OnStoryEnd?.Invoke();
        OnStoryEnd = null;
    }

    private void LoadPage()
    {
        SoundManager.Instance.StopSound();
        AudioClip sound = Resources.Load<AudioClip>(stories[page].tts.Trim());

        if (sound)
            SoundManager.Instance.PlaySound(sound);
        StartCoroutine(LoadText(stories[page].content.Replace("  ", "\n\n")));
    }

    private IEnumerator LoadText(string content)
    {
        texting = true;
        string contentText = "";

        int i = 0;
        while (i < content.Length && texting)
        {
            contentText += content[i];
            UIManager.Instance.SetStoryPannel(contentText);
            yield return YieldInstructionCache.WaitForSeconds(0.03f);
            i++;
        }

        UIManager.Instance.SetStoryPannel(content);
        texting = false;
    }


}

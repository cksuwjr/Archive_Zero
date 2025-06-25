using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public enum DataType
{
    Monster,
    Wave,
    Wave_Green,
    Wave_White,
    Story,
}


public class DataManager : Singleton<DataManager>, IManager
{
    public readonly string ADDRESS = "https://docs.google.com/spreadsheets/d/1DLGImZy5xiaD7Uq0xEu_7DqHbbQhLOcGBiU_BwluvSs";
    public readonly string MONSTER_RANGE = "A2:H";
    public readonly long MONSTER_SHEET_ID = 0;

    public readonly string WAVE_RANGE = "A2:G";
    public readonly long WAVE_SHEET_ID = 2098425441;

    public readonly string WAVE_GREEN_RANGE = "A2:G";
    public readonly long WAVE_GREEN_SHEET_ID = 1178414681;

    public readonly string WAVE_WHITE_RANGE = "A2:G";
    public readonly long WAVE_WHITE_SHEET_ID = 1604304226;

    public readonly string STORY_RANGE = "A2:D";
    public readonly long STORY_SHEET_ID = 1485852726;

    [SerializeField] private List<MonsterData> monsterData;
    [SerializeField] private List<WaveData> waveData;
    [SerializeField] private List<WaveData> waveGreenData;
    [SerializeField] private List<WaveData> waveWhiteData;
    [SerializeField] private List<StoryData> storyData;

    public bool isStoryLoad = false;
    public Action OnStoryLoaded;

    private string dataPath;

    public void Init()
    {
        // Computer
        StartCoroutine("LoadSheetData", DataType.Monster);
        StartCoroutine("LoadSheetData", DataType.Wave);
        StartCoroutine("LoadSheetData", DataType.Wave_Green);
        StartCoroutine("LoadSheetData", DataType.Wave_White);
        StartCoroutine("LoadSheetData", DataType.Story);
    }

    private IEnumerator LoadSheetData(DataType type)
    {
        UnityWebRequest www;

        switch (type)
        {
            case DataType.Monster:
                www = UnityWebRequest.Get(ReadSpreadSheet(ADDRESS, MONSTER_RANGE, MONSTER_SHEET_ID));
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                    monsterData = ParsingSheet.GetDatas<MonsterData>(www.downloadHandler.text);
                else
                    LoadStageData(type);
                break;
            case DataType.Wave:
                www = UnityWebRequest.Get(ReadSpreadSheet(ADDRESS, WAVE_RANGE, WAVE_SHEET_ID));
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                    waveData = ParsingSheet.GetDatas<WaveData>(www.downloadHandler.text);
                else
                    LoadStageData(type); 
                break;
            case DataType.Wave_Green:
                www = UnityWebRequest.Get(ReadSpreadSheet(ADDRESS, WAVE_GREEN_RANGE, WAVE_GREEN_SHEET_ID));
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                    waveGreenData = ParsingSheet.GetDatas<WaveData>(www.downloadHandler.text);
                else
                    LoadStageData(type); 
                break;
            case DataType.Wave_White:
                www = UnityWebRequest.Get(ReadSpreadSheet(ADDRESS, WAVE_WHITE_RANGE, WAVE_WHITE_SHEET_ID));
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                    waveWhiteData = ParsingSheet.GetDatas<WaveData>(www.downloadHandler.text);
                else
                    LoadStageData(type); 
                break;
            case DataType.Story:
                www = UnityWebRequest.Get(ReadSpreadSheet(ADDRESS, STORY_RANGE, STORY_SHEET_ID));
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                    storyData = ParsingSheet.GetDatas<StoryData>(www.downloadHandler.text);
                else
                    LoadStageData(type);
                isStoryLoad = true;
                OnStoryLoaded?.Invoke();
                break;
        }
    }

    public void SaveStageData(DataType type)
    {
        switch (type)
        {
            case DataType.Monster:
                dataPath = Application.persistentDataPath + "/MonsterData";
                string data = JsonList.ToJson(monsterData);
                File.WriteAllText(dataPath, data);
                break;
            case DataType.Wave:
                dataPath = Application.persistentDataPath + "/WaveData";
                data = JsonList.ToJson(waveData);
                File.WriteAllText(dataPath, data);
                break;
            case DataType.Wave_Green:
                dataPath = Application.persistentDataPath + "/WaveGreenData";
                data = JsonList.ToJson(waveGreenData);
                File.WriteAllText(dataPath, data);
                break;
            case DataType.Wave_White:
                dataPath = Application.persistentDataPath + "/WaveWhiteData";
                data = JsonList.ToJson(waveWhiteData);
                File.WriteAllText(dataPath, data);
                break;
            case DataType.Story:
                dataPath = Application.persistentDataPath + "/StoryData";
                data = JsonList.ToJson(storyData);
                File.WriteAllText(dataPath, data);
                break;
        }
        Debug.Log(dataPath);
    }

    public void LoadStageData(DataType type)
    {
        switch (type)
        {
            case DataType.Monster:
                dataPath = Application.persistentDataPath + "/MonsterData";
                if (File.Exists(dataPath))
                {
                    string data = File.ReadAllText(dataPath);
                    monsterData = JsonList.FromJson<MonsterData>(data);
                }
                break;
            case DataType.Wave:
                dataPath = Application.persistentDataPath + "/WaveData";
                if (File.Exists(dataPath))
                {
                    string data = File.ReadAllText(dataPath);
                    waveData = JsonList.FromJson<WaveData>(data);
                }
                break;
            case DataType.Wave_Green:
                dataPath = Application.persistentDataPath + "/WaveGreenData";
                if (File.Exists(dataPath))
                {
                    string data = File.ReadAllText(dataPath);
                    waveGreenData = JsonList.FromJson<WaveData>(data);
                }
                break;
            case DataType.Wave_White:
                dataPath = Application.persistentDataPath + "/WaveWhiteData";
                if (File.Exists(dataPath))
                {
                    string data = File.ReadAllText(dataPath);
                    waveWhiteData = JsonList.FromJson<WaveData>(data);
                }
                break;
            case DataType.Story:
                dataPath = Application.persistentDataPath + "/StoryData";
                if (File.Exists(dataPath))
                {
                    string data = File.ReadAllText(dataPath);
                    storyData = JsonList.FromJson<StoryData>(data);
                }
                break;
        }
    }

    public void DeleteData()
    {
        dataPath = Application.persistentDataPath + "/MonsterData";
        File.Delete(dataPath);
        dataPath = Application.persistentDataPath + "/WaveData";
        File.Delete(dataPath);
        dataPath = Application.persistentDataPath + "/WaveGreenData";
        File.Delete(dataPath);
        dataPath = Application.persistentDataPath + "/WaveWhiteData";
        File.Delete(dataPath);
        dataPath = Application.persistentDataPath + "/StoryData";
        File.Delete(dataPath);
    }

    public string ReadSpreadSheet(string address, string range, long sheetID)
    {
        return $"{address}/export?format=tsv&range={range}&gid={sheetID}";
    }


    public MonsterData GetMonsterData(int id)
    {
        return monsterData[id];
    }

    public int GetMonsterDataCount()
    {
        return waveData.Count;
    }


    public WaveData GetWaveData(int id)
    {
        return waveData[id];
    }

    public int GetWaveCount()
    {
        return waveData.Count;
    }


    public WaveData GetWaveGreenData(int id)
    {
        return waveGreenData[id];
    }

    public int GetWaveGreenDataCount()
    {
        return waveGreenData.Count;
    }

    public WaveData GetWaveWhiteData(int id)
    {
        return waveWhiteData[id];
    }

    public int GetWaveWhiteCount()
    {
        return waveWhiteData.Count;
    }

    public List<StoryData> GetStory()
    {
        return storyData;
    }

    public StoryData GetStoryData(int id)
    {
        return storyData[id];
    }

    public int GetStoryCount()
    {
        return storyData.Count;
    }
}
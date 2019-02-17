using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataManager : Singleton<PersistentDataManager>
{
    static string levelFinishedSuffix = "_finished";
    static string levelStarSuffix = "_star";

    public int totalStar { get; private set; }
    public Dictionary<string, bool> isFinishedByLevelId;
    Dictionary<string, int> starByLevelId;
    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        isFinishedByLevelId = new Dictionary<string, bool>();
        starByLevelId = new Dictionary<string, int>();
        foreach (string levelIdentifier in LevelManager.Instance.levelInfoByIdentifier.Keys)
        {
            isFinishedByLevelId[levelIdentifier] = PlayerPrefs.GetInt(levelIdentifier + levelFinishedSuffix) == 1;
            starByLevelId[levelIdentifier] = PlayerPrefs.GetInt(levelIdentifier + levelStarSuffix);
            totalStar += starByLevelId[levelIdentifier];
        }
        LevelManager.Instance.UpdateWithPersistentData();
    }
}

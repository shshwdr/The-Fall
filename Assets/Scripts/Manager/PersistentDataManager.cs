using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataManager : Singleton<PersistentDataManager>
{
    static string levelFinishedSuffix = "_finished";
    static string levelStarSuffix = "_star";

    public int totalStar { get; private set; }
    public Dictionary<string, bool> isFinishedByLevelId { get; private set; }
    public Dictionary<string, int> starByLevelId { get; private set; }
    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        isFinishedByLevelId = new Dictionary<string, bool>();
        starByLevelId = new Dictionary<string, int>();
        totalStar = 0;
        foreach (string levelIdentifier in LevelManager.Instance.levelInfoByIdentifier.Keys)
        {
            LoadPersistentData(levelIdentifier);
            totalStar += starByLevelId[levelIdentifier];
        }
        LevelManager.Instance.UpdateWithPersistentData();
    }
    void LoadPersistentData(string levelIdentifier)
    {
        isFinishedByLevelId[levelIdentifier] = PlayerPrefs.GetInt(levelIdentifier + levelFinishedSuffix) == 1;
        starByLevelId[levelIdentifier] = PlayerPrefs.GetInt(levelIdentifier + levelStarSuffix);
    }
    void SavePersistentData(string levelIdentifier,int starNum)
    {
        PlayerPrefs.SetInt(levelIdentifier + levelFinishedSuffix, 1);
        PlayerPrefs.SetInt(levelIdentifier+levelStarSuffix, starNum);
    }
    public void RecordStar(string levelId, int star) {
        if (!starByLevelId.ContainsKey(levelId))
        {
            Debug.Log(starByLevelId + " does not contain " + levelId);
        }
        if (star > starByLevelId[levelId])
        {
            int diff = star - starByLevelId[levelId];
            totalStar += diff;
            SavePersistentData(levelId, star);
            LoadPersistentData(levelId);
            LevelManager.Instance.UpdateWithPersistentData();
            AchievementManager.Instance.CollectStar(diff);
            LeaderboardManager.Instance.ReportScore(GPGSIds.leaderboard_total_stars, totalStar);
        }
    }
    public void UnlockAll()
    {
        foreach (string levelIdentifier in LevelManager.Instance.levelInfoByIdentifier.Keys)
        {
            SavePersistentData(levelIdentifier, 3);
        }
    }
}

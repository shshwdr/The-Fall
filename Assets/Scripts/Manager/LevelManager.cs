using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public Dictionary<string, LevelInfo> levelInfoByIdentifier;
    public Dictionary<string, StageInfo> stageInfoByIdentifier;
    public List<StageInfo> stageInfoList;
    public Dictionary<string, List<LevelInfo>> levelInfoByStageId;
    public Dictionary<string, bool> stageUnlockDict;
    public Dictionary<string, bool> levelUnlockDict;
    public Dictionary<string, string> levelToNextLevel;
    public string currentLevelId;
    // Start is called before the first frame update
    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        stageInfoList = CsvUtil.LoadObjects<StageInfo>("stage.csv");
        List<LevelInfo> levelInfoList = CsvUtil.LoadObjects<LevelInfo>("level.csv");
        levelInfoByStageId = new Dictionary<string, List<LevelInfo>>();
        levelInfoByIdentifier = new Dictionary<string, LevelInfo>();
        stageInfoByIdentifier = new Dictionary<string, StageInfo>();
        stageUnlockDict = new Dictionary<string, bool>();
        levelUnlockDict = new Dictionary<string, bool>();
        levelToNextLevel = new Dictionary<string, string>();
        string lastLevelId = null;
        foreach (StageInfo stageInfo in stageInfoList)
        {
            levelInfoByStageId[stageInfo.identifier] = new List<LevelInfo>();
            stageInfoByIdentifier[stageInfo.identifier] = stageInfo;
        }
        foreach (LevelInfo levelInfo in levelInfoList)
        {
            if (!levelInfoByStageId.ContainsKey(levelInfo.stageIdentifier))
            {
                Debug.LogError("stage id " + levelInfo.stageIdentifier + " does not exist on level " + levelInfo.identifier);
                continue;
            }

            levelInfoByIdentifier[levelInfo.identifier] = levelInfo;
            levelInfoByStageId[levelInfo.stageIdentifier].Add(levelInfo);

            if (lastLevelId != null)
            {
                levelToNextLevel[lastLevelId] = levelInfo.identifier;
            }
            lastLevelId = levelInfo.identifier;
        }
    }
    public void UpdateWithPersistentData()
    {
        foreach (StageInfo stageInfo in stageInfoList)
        {
            if (stageInfo.starsToUnlock <= PersistentDataManager.Instance.totalStar)
            {
                stageUnlockDict[stageInfo.identifier] = true;
            }
            else
            {
                stageUnlockDict[stageInfo.identifier] = false;
            }
        }
        foreach (LevelInfo levelInfo in levelInfoByIdentifier.Values)
        {
            if (stageUnlockDict[levelInfo.stageIdentifier] &&
                (levelInfo.preLevelIdentifier == null || levelInfo.preLevelIdentifier.Length == 0 ||
                PersistentDataManager.Instance.isFinishedByLevelId[levelInfo.preLevelIdentifier]))
            {
                levelUnlockDict[levelInfo.identifier] = true;
            }
            else
            {
                levelUnlockDict[levelInfo.identifier] = false;
            }
        }
    }
    public void LoadLevel(string levelId)
    {
        currentLevelId = levelId;
        SceneManager.LoadScene(levelId);
    }
    public bool HasNextLevel()
    {
        return levelToNextLevel.ContainsKey(currentLevelId);
    }
    public bool NextLevelUnklocked()
    {
        return levelToNextLevel!=null && levelToNextLevel.ContainsKey(currentLevelId) && levelUnlockDict[levelToNextLevel[currentLevelId]];
    }
    public void LoadNextLevel() {
        string nextLevelId = levelInfoByIdentifier[levelToNextLevel[currentLevelId]].identifier;
        LoadLevel(nextLevelId);
    }
}

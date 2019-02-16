using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class LevelManager : Singleton<LevelManager>
{
    public Dictionary<string, LevelInfo> levelInfoByIdentifier;
    public Dictionary<string, StageInfo> stageInfoByIdentifier;
    public List<StageInfo> stageInfoList;
    public Dictionary<string, List<LevelInfo>> levelInfoByStageId;
    // Start is called before the first frame update
    private void OnEnable()
    {
        stageInfoList = CsvUtil.LoadObjects<StageInfo>("stage.csv");
        List<LevelInfo> levelInfoList = CsvUtil.LoadObjects<LevelInfo>("level.csv");
        levelInfoByStageId = new Dictionary<string, List<LevelInfo>>();
        levelInfoByIdentifier = new Dictionary<string, LevelInfo>();
        stageInfoByIdentifier = new Dictionary<string, StageInfo>();
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

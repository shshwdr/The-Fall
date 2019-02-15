﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class MainMenuViewController : MonoBehaviour
{
    public Transform stageTable;
    public GameObject stagePrefab;
    Dictionary<string, LevelInfo> levelInfoByIdentifier;
    public Dictionary<string, StageInfo> stageInfoByIdentifier;
    List<StageInfo> stageInfoList;
    public Dictionary<string, List<LevelInfo>> levelInfoByStageId;
    // Start is called before the first frame update
    void Start()
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
            if(!levelInfoByStageId.ContainsKey(levelInfo.stageIdentifier))
            {
                Debug.LogError("stage id " + levelInfo.stageIdentifier + " does not exist on level " + levelInfo.identifier);
                continue;
            }

            levelInfoByIdentifier[levelInfo.identifier] = levelInfo;
            levelInfoByStageId[levelInfo.stageIdentifier].Add(levelInfo);
        }
        SetupView();
    }

    // Update is called once per frame
    void SetupView()
    {
        foreach(StageInfo stageInfo in stageInfoList)
        {
            GameObject stageObject = Instantiate(stagePrefab, stageTable);
            StageCell stageCell = stageObject.GetComponent<StageCell>();
            stageCell.InitWithStageInfo(stageInfo);
        }
    }
}

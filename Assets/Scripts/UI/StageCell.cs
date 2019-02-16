using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageCell : MonoBehaviour
{
    TextMeshProUGUI title;
    StageInfo stageInfo;
    Image bg;

    public Transform levelTable;
    GameObject levelCellPrefab;
    // Start is called before the first frame update
    public void InitWithStageInfo(StageInfo _stageInfo)
    {
        title = GetComponentInChildren<TextMeshProUGUI>();
        bg = GetComponent<Image>();
        levelCellPrefab = Resources.Load<GameObject>("Prefabs/UI/Level");
        stageInfo = _stageInfo;

        SetupStageCell();
        SetupLevelTable();
    }
    void SetupStageCell()
    {

        title.text = stageInfo.name;
        title.color = stageInfo.color;
        bg.color = new Color(stageInfo.color.r, stageInfo.color.g, stageInfo.color.b, bg.color.a);
    }
    void SetupLevelTable()
    {
        foreach (LevelInfo levelInfo in  LevelManager.Instance.levelInfoByStageId[stageInfo.identifier])
        {
            GameObject stageObject = Instantiate(levelCellPrefab, levelTable);
            LevelCell levelCell = stageObject.GetComponent<LevelCell>();
            levelCell.InitWithLevelInfo(levelInfo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

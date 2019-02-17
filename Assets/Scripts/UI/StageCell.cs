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

    public GameObject lockImage;
    public Color lockColor;
    public GameObject requirementPanel;
    public TextMeshProUGUI requirementText;

    bool isUnlocked;

    public Transform levelTable;
    GameObject levelCellPrefab;
    // Start is called before the first frame update
    public void InitWithStageInfo(StageInfo _stageInfo)
    {
        title = GetComponentInChildren<TextMeshProUGUI>();
        bg = GetComponent<Image>();
        levelCellPrefab = Resources.Load<GameObject>("Prefabs/UI/Level");
        stageInfo = _stageInfo;

        isUnlocked = LevelManager.Instance.stageUnlockDict[stageInfo.identifier];

        SetupStageCell();
        SetupLevelTable();
    }
    void SetupStageCell()
    {
        title.text = stageInfo.name;
        Color stageColor = isUnlocked?stageInfo.color:lockColor;
        title.color = stageColor;
        bg.color = new Color(stageColor.r, stageColor.g, stageColor.b, bg.color.a);
        lockImage.SetActive(!isUnlocked);
        if (!isUnlocked)
        {
            requirementPanel.SetActive(true);
            requirementText.text = stageInfo.starsToUnlock.ToString();
        }
        else
        {
            requirementPanel.SetActive(false);
        }
    }
    void SetupLevelTable()
    {
        foreach (LevelInfo levelInfo in  LevelManager.Instance.levelInfoByStageId[stageInfo.identifier])
        {
            GameObject stageObject = Instantiate(levelCellPrefab, levelTable);
            LevelCell levelCell = stageObject.GetComponent<LevelCell>();
            levelCell.InitWithLevelInfo(levelInfo, isUnlocked);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

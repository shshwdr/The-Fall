using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuViewController : MonoBehaviour
{
    public Transform stageTable;
    GameObject stagePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        stagePrefab = Resources.Load<GameObject>("Prefabs/UI/Stage");
        SetupView();
    }

    // Update is called once per frame
    void SetupView()
    {
        foreach(StageInfo stageInfo in LevelManager.Instance. stageInfoList)
        {
            GameObject stageObject = Instantiate(stagePrefab, stageTable);
            StageCell stageCell = stageObject.GetComponent<StageCell>();
            stageCell.InitWithStageInfo(stageInfo);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageCell : MonoBehaviour
{
    TextMeshProUGUI title;
    StageInfo stageInfo;
    // Start is called before the first frame update
    public void InitWithStageInfo(StageInfo _stageInfo)
    {
        title = GetComponentInChildren<TextMeshProUGUI>();
        stageInfo = _stageInfo;
        title.text = stageInfo.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

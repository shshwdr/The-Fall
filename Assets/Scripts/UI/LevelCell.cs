using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    public Image icon;
    public GameObject[] stars;

    LevelInfo levelInfo;
    public void InitWithLevelInfo(LevelInfo _levelInfo)
    {
        levelInfo = _levelInfo;
        
        SetupCell();
    }
    void SetupCell()
    {
        icon.sprite = levelInfo.icon;


    }
    
}

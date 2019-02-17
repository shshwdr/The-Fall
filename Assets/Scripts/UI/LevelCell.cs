﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCell : MonoBehaviour
{
    public Image icon;
    public GameObject[] stars;
    Button button;

    public GameObject lockImage;
    public Color lockColor;

    LevelInfo levelInfo;
    bool isStageUnLocked;
    public void InitWithLevelInfo(LevelInfo _levelInfo,bool _isStageUnLocked)
    {
        button = GetComponent<Button>();
        levelInfo = _levelInfo;
        isStageUnLocked = _isStageUnLocked;
        SetupCell();
    }
    void SetupCell()
    {
        bool isUnlocked = LevelManager.Instance.levelUnlockDict[levelInfo.identifier];
        icon.sprite = levelInfo.icon;
        if (!isUnlocked)
        {
            foreach(Image image in GetComponentsInChildren<Image>())
            {
                image.color = lockColor;
            }
        }
        button.onClick.AddListener(delegate { SceneManager.LoadScene(levelInfo.identifier); });
        lockImage.SetActive(!isUnlocked && isStageUnLocked);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCell : MonoBehaviour
{
    public Image icon;
    public GameObject[] stars;
    Button button;

    LevelInfo levelInfo;
    public void InitWithLevelInfo(LevelInfo _levelInfo)
    {
        button = GetComponent<Button>();
        levelInfo = _levelInfo;
        
        SetupCell();
    }
    void SetupCell()
    {
        icon.sprite = levelInfo.icon;
        button.onClick.AddListener(delegate { SceneManager.LoadScene(levelInfo.identifier); });

    }
    
}

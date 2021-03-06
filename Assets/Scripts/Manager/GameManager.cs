﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameOver { get; private set; }
    public bool IsWin { get; private set; }
    public int starNum { get; private set; }
    public bool IsGameEnd { get { return IsGameOver || IsWin; } }
    // Start is called before the first frame update
    void Start()
    {
        IsGameOver = false;
        IsWin = false;
        starNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectStar()
    {
        starNum++;
    }

    public void Win()
    {
        if (IsWin || IsGameOver)
        {
            return;
        }
        IsWin = true;
        PersistentDataManager.Instance.RecordStar(LevelManager.Instance.currentLevelId, starNum);
        AchievementManager.Instance.UnlockAchievement(GPGSIds.achievement_grow_your_first_seed);
    }

    public void UIGameOver()
    {
        IsGameOver = true;
        Seed.Instance.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        if (IsWin || IsGameOver)
        {
            return;
        }
        GameEndViewController.CreateGameFailedView();
        IsGameOver = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(LevelManager.Instance.currentLevelId);
    }
}

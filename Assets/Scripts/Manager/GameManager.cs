using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameOver { get; private set; }
    public bool IsWin { get; private set; }
    int starNum;
    // Start is called before the first frame update
    void Start()
    {
        IsGameOver = false;
        IsWin = false;
        starNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetStar()
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
    }



    public void GameOver()
    {
        if (IsWin || IsGameOver)
        {
            return;
        }
        GameOverViewController.CreateGameOverView();
        IsGameOver = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

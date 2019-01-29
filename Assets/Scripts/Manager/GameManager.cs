using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameOver {  get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        IsGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        GameOverViewController.CreateGameOverView();
        IsGameOver = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

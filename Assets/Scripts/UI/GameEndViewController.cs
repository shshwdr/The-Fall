using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEndViewController : MonoBehaviour
{
    public Button nextLevelButton;
    public Button restartButton;
    public Button backToMenuButton;
    public TextMeshProUGUI resultText;
    public Button resumeButton;

    public ResultPageStar[] stars;
    public AnimationCurve animationCurve;
    public float starAnimBegin = 0.5f;
    public float starAnimInterval = 0.5f;
    bool isPause;

    //private void Start()
    //{
    //    Init();
    //}

    public static void CreateGameWinView()
    {
        GameObject prefab = Resources.Load("Prefabs/UI/GameEnd", typeof(GameObject)) as GameObject;
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        GameEndViewController script = go.GetComponent<GameEndViewController>();
        script.Init(true);
    }
    public static void CreateGameFailedView()
    {
        GameObject prefab = Resources.Load("Prefabs/UI/GameEnd", typeof(GameObject)) as GameObject;
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        GameEndViewController script = go.GetComponent<GameEndViewController>();
        script.Init(false);
    }

    public static void CreatePauseView()
    {
        Time.timeScale = 0;
        GameObject prefab = Resources.Load("Prefabs/UI/GameEnd", typeof(GameObject)) as GameObject;
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        GameEndViewController script = go.GetComponent<GameEndViewController>();

        script.isPause = true;
        script.Init(false);
    }

    void Init(bool win)
    {
        backToMenuButton.onClick.AddListener(delegate { GameManager.Instance.UIGameOver(); SceneManager.LoadScene("mainMenu"); Time.timeScale = 1; });
        restartButton.onClick.AddListener(delegate { GameManager.Instance.UIGameOver(); GameManager.Instance.Restart(); Time.timeScale = 1; });
        if (isPause)
        {
            foreach(ResultPageStar star in stars)
            {
                star.gameObject.SetActive(false);
            }
            resumeButton.onClick.AddListener(delegate { Destroy(gameObject); Time.timeScale = 1; });
        }
        else
        {
            resumeButton.interactable = false;
        }
        if (win)
        {

            ShowStars();
        }
        resultText.text = isPause? "Resume" :(win ? "Succeed" : "failed");
        if (LevelManager.Instance.NextLevelUnklocked()) { 
            nextLevelButton.onClick.AddListener(delegate { GameManager.Instance.UIGameOver(); LevelManager.Instance.LoadNextLevel(); Time.timeScale = 1; });
        }
        else
        {
            nextLevelButton.gameObject.SetActive(false);
        }
    }
    void ShowStars()
    {
        for(int i = 0; i < GameManager.Instance.starNum; i++)
        {
            StartCoroutine(stars[i].ShowStarAnim(i*starAnimInterval + starAnimBegin,animationCurve));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

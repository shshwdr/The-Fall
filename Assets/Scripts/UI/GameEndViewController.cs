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

    public ResultPageStar[] stars;
    public AnimationCurve animationCurve;
    public float starAnimBegin = 0.5f;
    public float starAnimInterval = 0.5f;

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

    void Init(bool win)
    {
        backToMenuButton.onClick.AddListener(delegate { SceneManager.LoadScene("mainMenu"); });
        restartButton.onClick.AddListener(delegate { GameManager.Instance.Restart(); });
        if (win)
        {

            ShowStars();
        }
        resultText.text = win ? "Succeed" : "failed";
        if (LevelManager.Instance.NextLevelUnklocked()) { 
        nextLevelButton.onClick.AddListener(delegate { LevelManager.Instance.LoadNextLevel(); });
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

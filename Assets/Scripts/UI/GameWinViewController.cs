using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinViewController : MonoBehaviour
{
    public Button nextLevelButton;
    public Button restartButton;
    public Button backToMenuButton;

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
        GameObject prefab = Resources.Load("Prefabs/UI/GameWin", typeof(GameObject)) as GameObject;
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        GameWinViewController script = go.GetComponent<GameWinViewController>();
        script.Init();
    }
    
    void Init()
    {
        nextLevelButton.onClick.AddListener(delegate { GameManager.Instance.Restart(); });
        restartButton.onClick.AddListener(delegate { GameManager.Instance.Restart(); });
        backToMenuButton.onClick.AddListener(delegate { GameManager.Instance.Restart(); });
        ShowStars();
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

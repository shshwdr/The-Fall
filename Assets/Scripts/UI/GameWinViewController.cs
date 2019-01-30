using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinViewController : MonoBehaviour
{
    public Button nextLevelButton;
    public Button restartButton;

    public static void CreateGameWinView()
    {
        GameObject prefab = Resources.Load("Prefabs/UI/GameWin", typeof(GameObject)) as GameObject;
        GameObject go = Instantiate(prefab, ViewControllerManager.Instance.viewControllerCanvas.transform) as GameObject;
        //GameOverViewController script = go.GetComponent<GameOverViewController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton.onClick.AddListener(delegate { GameManager.Instance.Restart(); });
        restartButton.onClick.AddListener(delegate { GameManager.Instance.Restart(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

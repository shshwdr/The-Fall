using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour
{
    public Button PauseButton;
    // Start is called before the first frame update
    void Start()
    {
        PauseButton.onClick.AddListener(delegate { GameEndViewController.CreatePauseView(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

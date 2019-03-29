using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayViewController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SelectDropdown(int index)
    {
        switch (index)
        {
            case 0:
                Loginin();
                break;
            case 1:
                Leaderboard();
                break;
            case 2:
                Achievement();
                break;
            case 3:
                Logout();
                break;
            default:
                Debug.LogError("index " + index + " does not support in dropdown box");
                break;
        }
    }
    public void Loginin()
    {
        Debug.Log("Loginin");

    }
    public void Logout()
    {
        Debug.Log("Logout");

    }
    public void Leaderboard()
    {
        Debug.Log("Leaderboard");
    }
    public void Achievement()
    {
        Debug.Log("Achievement");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

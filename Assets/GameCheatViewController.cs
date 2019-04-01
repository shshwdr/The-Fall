using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCheatViewController : MonoBehaviour
{
    public Transform cheatTable;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cheatButtonPrefab = Resources.Load("Prefabs/UI/cheatButton") as GameObject;
        GameObject finishLevel = Instantiate(cheatButtonPrefab, cheatTable);
        finishLevel.GetComponent<Button>().onClick.AddListener(delegate { Win(); });
    }

    public void Win()
    {
        GameManager.Instance.Win();
    }
}

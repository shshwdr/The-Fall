using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestMenuViewController : MonoBehaviour
{
    public Button clearPersistentData;
    public Button unlockAllPersistentData;
    // Start is called before the first frame update
    void Start()
    {
        clearPersistentData.onClick.AddListener(delegate { PlayerPrefs.DeleteAll(); SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
        unlockAllPersistentData.onClick.AddListener(delegate { PersistentDataManager.Instance.UnlockAll(); SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

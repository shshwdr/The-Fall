using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMenuViewController : MonoBehaviour
{
    public Button clearPersistentData;
    // Start is called before the first frame update
    void Start()
    {
        clearPersistentData.onClick.AddListener(delegate { PlayerPrefs.DeleteAll(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

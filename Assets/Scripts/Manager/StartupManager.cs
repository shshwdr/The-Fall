using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupManager : MonoBehaviour
{
    private void OnEnable()
    {
        LevelManager.Instance.Init();
        PersistentDataManager.Instance.Init();
    }
}

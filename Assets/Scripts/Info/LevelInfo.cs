using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    public string identifier;
    public string name;
    public string stageIdentifier;
    public string preLevelIdentifier;
    public string iconName;
    public Sprite icon { get { return Resources.Load<Sprite>("levelIcons/" + iconName); } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    public string identifier;
    public string name;
    public int starsToUnlock;
    public List<float> colorList;
    public Color color { get { return new Color(colorList[0], colorList[1], colorList[2], 1); } }
}

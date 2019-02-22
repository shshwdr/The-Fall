using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Lightning : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public LineRenderer light;
    public LightningBoltScript[] lightningBolt;
    public GameObject screenLight;

    enum LightningState { inactive,showLight,fadeoutLight,showLightning,screenLight,end};
    LightningState state;
    // Start is called before the first frame update
    void Start()
    {
        


        state = LightningState.showLight;
        light.SetPosition(0, startPoint.position);
        light.SetPosition(1, endPoint.position);
        foreach(LightningBoltScript bolt in lightningBolt)
        {

            bolt.StartPosition = startPoint.position;
            bolt.EndPosition = endPoint.position;
        }
    }



    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case LightningState.inactive:
                break;
            case LightningState.showLight:
                break;
            case LightningState.fadeoutLight:
                break;
            case LightningState.showLightning:
                break;
            case LightningState.screenLight:
                break;
            case LightningState.end:
                break;
        }
    }
}

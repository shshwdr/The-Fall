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

    Color lightFadeoutColor;
    Color lightFullColor;

    float countTime;

    float lightFadeInTime = 0.5f;
    float lightStayTime = 1f;
    float lightFadeOutTime = 1f;

    enum LightningState { inactive,showLight,fadeoutLight,showLightning,screenLight,end};
    LightningState state;
    // Start is called before the first frame update
    void Start()
    {
        
        state = LightningState.inactive;
        light.SetPosition(0, startPoint.position);
        light.SetPosition(1, endPoint.position);
        lightFullColor = light.startColor;
        lightFadeoutColor  = new Color(light.startColor.r, light.startColor.g, light.startColor.b, 0);

        foreach (LightningBoltScript bolt in lightningBolt)
        {

            bolt.StartPosition = startPoint.position;
            bolt.EndPosition = endPoint.position;
        }

        StartCoroutine(StartActivity());
    }

    IEnumerator StartActivity()
    {
        yield return new WaitForSeconds(1);
        state = LightningState.showLight;
        countTime = 0;
    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        switch (state)
        {
            case LightningState.inactive:
                light.startColor = lightFadeoutColor;
                light.endColor = lightFadeoutColor;
                //lightningBolt
                break;
            case LightningState.showLight:
                countTime += Time.deltaTime;
                if (countTime < lightFadeInTime)
                {
                    light.startColor = Color.Lerp(lightFadeoutColor, lightFullColor,countTime/lightFadeInTime);
                    light.endColor = light.startColor;
                }
                if (countTime > lightFadeInTime + lightStayTime)
                {
                    state = LightningState.fadeoutLight;
                }
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

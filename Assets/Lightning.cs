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
    LineRenderer[] lightningBoltRender;
    public GameObject screenLight;

    Color lightFadeoutColor;
    Color lightFullColor;

    float countTime;

    float lightFadeInTime = 0.2f;
    float lightStayTime = 0.5f;
    float lightFadeOutTime = 0.5f;
    
    float lightningBoltMoveTime = 0.5f;

    float screenLightStayTime = 0.3f;
    float screenLightFadeOutTime = 0.5f;
    float lightningBoltFadeOutTime = 0.8f;

    Color screenLightFadeOutColor;
    Color screenLightFullColor;

    Vector3 midPosition;

    enum LightningState { inactive,showLight,fadeoutLight,showLightning,screenLight,end};
    LightningState state;
    // Start is called before the first frame update
    void Start()
    {
        
        state = LightningState.inactive;

        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        endPoint.position = new Vector3(endPoint.position.x, startPoint.position.y - (halfHeight * 2*1.1f), endPoint.position.z);
        midPosition = (startPoint.position + endPoint.position) / 2;

        light.SetPosition(0, startPoint.position);
        light.SetPosition(1, endPoint.position);
        lightFullColor = light.startColor;
        lightFadeoutColor  = new Color(light.startColor.r, light.startColor.g, light.startColor.b, 0);
        screenLightFullColor = screenLight.GetComponent<SpriteRenderer>().color;
        screenLightFadeOutColor = new Color(screenLightFullColor.r, screenLightFullColor.g, screenLightFullColor.b, 0);

        foreach (LightningBoltScript bolt in lightningBolt)
        {

            bolt.StartPosition = startPoint.position;
            bolt.EndPosition = endPoint.position;
        }

        //StartCoroutine(StartActivity());
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
        //if (Input.GetMouseButtonDown(0))
        //{
        //    StartCoroutine(StartActivity());
        //}

        Debug.Log(state);
        countTime += Time.deltaTime;
        switch (state)
        {
            case LightningState.inactive:
                light.startColor = lightFadeoutColor;
                light.endColor = lightFadeoutColor;
                foreach(LightningBoltScript bolt in lightningBolt)
                {
                    bolt.EndPosition = startPoint.position ;
                    bolt.gameObject.SetActive(false);
                }
                screenLight.SetActive(false);


                if (Camera.main.transform.position.y < midPosition.y)
                {
                    Debug.Log("pass middle" + midPosition.y + " " + Camera.main.transform.position.y);
                    state = LightningState.showLight;
                    countTime = 0;
                }
                break;
            case LightningState.showLight:
                if (countTime < lightFadeInTime)
                {
                    light.startColor = Color.Lerp(lightFadeoutColor, lightFullColor,countTime/lightFadeInTime);
                    light.endColor = light.startColor;
                }
                if (countTime > lightFadeInTime + lightStayTime)
                {
                    countTime = 0;
                    state = LightningState.fadeoutLight;
                }
                break;
            case LightningState.fadeoutLight:
                if (countTime < lightFadeOutTime)
                {
                    light.startColor = Color.Lerp( lightFullColor, lightFadeoutColor, countTime / lightFadeOutTime);
                    light.endColor = light.startColor;
                }
                else
                {
                    countTime = 0;
                    state = LightningState.showLightning;
                }
                break;
            case LightningState.showLightning:
                if (countTime < lightningBoltMoveTime)
                {
                    foreach (LightningBoltScript bolt in lightningBolt)
                    {
                        bolt.gameObject.SetActive(true);
                        bolt.EndPosition = Vector3.Lerp(startPoint.position, endPoint.position, countTime / lightningBoltMoveTime);
                    }
                }
                else
                {
                    countTime = 0;
                    state = LightningState.screenLight;
                    screenLight.SetActive(true);
                }
                break;
            case LightningState.screenLight:
                if (countTime < screenLightFadeOutTime+screenLightStayTime && countTime>=screenLightStayTime)
                {
                    screenLight.GetComponent<SpriteRenderer>().color = Color.Lerp(screenLightFullColor, screenLightFadeOutColor, countTime / screenLightFadeOutTime);
                    
                }
                else if (countTime> screenLightFadeOutTime + screenLightStayTime)
                {
                    foreach (LightningBoltScript bolt in lightningBolt)
                    {
                        bolt.gameObject.SetActive(false);
                        //bolt.EndPosition = Vector3.Lerp(startPoint.position, endPoint.position, countTime / lightningBoltMoveTime);
                    }
                    state = LightningState.end;
                }
                break;
            case LightningState.end:
                break;
        }
    }
}

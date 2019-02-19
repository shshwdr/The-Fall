using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;

public class RainController : MonoBehaviour
{
    RainControlPoint[] controlPoints;
    RainScript2D rainScript;
    WindZone windZone;
    Camera camera;
    int currentIndex;
    float windBase = 300f;
    Rigidbody2D seedRb;
    public float sideWindForce = 10;
    public float downRainForce = 10;
    // Start is called before the first frame update
    void Start()
    {
        rainScript = GetComponentInChildren<RainScript2D>();
        windZone = GetComponentInChildren<WindZone>();
        controlPoints = GetComponentsInChildren<RainControlPoint>();
        camera = Camera.main;
        currentIndex = 1;
        seedRb = FindObjectOfType<Seed>().GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlPoints[currentIndex].transform.position.y > camera.transform.position.y)
        {
            currentIndex++;
        }
        rainScript.RainIntensity = controlPoints[currentIndex].intensity;
        windZone.windMain = controlPoints[currentIndex].direction * windBase;
    }
    private void FixedUpdate()
    {
        seedRb.AddForce(-Vector2.left*sideWindForce* controlPoints[currentIndex].direction* controlPoints[currentIndex].intensity);
        seedRb.AddForce(-Vector2.up * downRainForce * controlPoints[currentIndex].intensity);
    }
}

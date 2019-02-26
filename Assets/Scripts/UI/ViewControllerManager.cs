using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControllerManager : Singleton<ViewControllerManager>
{
    public GameObject viewControllerCanvas;
    // Start is called before the first frame update
    void Start()
    {
        viewControllerCanvas = GameObject.Find("Canvas") .GetComponent< Canvas>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

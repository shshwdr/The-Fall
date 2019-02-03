using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float delay = 1;
    public float speed = 3;
    public Vector3 direction;
    public Vector3 backPosition;

    bool startFlyTo;
    bool startFlyBack;
    float currentTime;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        backPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!startFlyTo) {
            currentTime += Time.deltaTime;
            if (currentTime > delay)
            {
                Vector3 target = GameObject.Find("seed").transform.position;
                direction = (target - backPosition).normalized;
                startFlyTo = true;
            }
        } else if (!startFlyBack)
        {
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            //if(transform.)
        }
        else
        {

        }
    }
}

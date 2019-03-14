using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public float leafFlyTriggerHeight;
     Rigidbody2D rb;
    float originalGravity;

    public float windForce = 1f;
    bool direction;
    public float windInterval = 0.5f;
    float currentTime = 0f;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Seed.Instance.transform.position.y < transform.position.y - leafFlyTriggerHeight)
        {
            rb.gravityScale = originalGravity;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyObject : MonoBehaviour
{

    public float slowDownRate = 1.5f;
    public float immediateFallChange = 2f;
    public float immediateJumpChange = 3f;
    public float angularDrag = 0.5f;
    public bool attachSeedOnIt = false;
    float originalAngularDrag = 0.05f;
    public bool caughtSeed;
    public Transform root;

    Collider2D _collision;
    
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "seed")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(rb.velocity.x * immediateJumpChange, rb.velocity.y * immediateJumpChange, 0);
            rb.angularDrag = originalAngularDrag;
            if (attachSeedOnIt)
            {

                rb.transform.SetParent(null, true);
            }
            caughtSeed = false;
            _collision = null;
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "seed")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(rb.velocity.x/immediateFallChange, rb.velocity.y / immediateFallChange, 0);
            if (attachSeedOnIt)
            {
                rb.transform.SetParent(root? root : transform, true);
            }
            rb.angularDrag = angularDrag;
            caughtSeed = true;
            _collision = collision;
        }
    }

    private void FixedUpdate()
    {
        if (_collision != null)
        {
            Rigidbody2D rb = _collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(rb.velocity.x/slowDownRate, rb.velocity.y / slowDownRate, 0);
        }
    }
}

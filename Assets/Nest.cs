using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{

    public float slowDownRate = 1.5f;
    public float immediateFallChange = 2f;
    public float immediateJumpChange = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("on trigger");
        if (collision.tag == "seed")
        {
            Debug.Log("trigger seed");
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(0, rb.velocity.y * immediateJumpChange, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("on trigger");
        if (collision.tag == "seed")
        {
            Debug.Log("trigger seed");
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(0, rb.velocity.y / immediateFallChange, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("on trigger");
        if(collision.tag == "seed")
        {
            Debug.Log("trigger seed");
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(0, rb.velocity.y/ slowDownRate, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

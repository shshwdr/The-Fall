using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float force = 2f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "seed")
        {

            Debug.Log("hit seed");
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            print("forward" + transform.up);
            rb.AddForce(transform.up * force,ForceMode2D.Impulse);
        }
    }
}

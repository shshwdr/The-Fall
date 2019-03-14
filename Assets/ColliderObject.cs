using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderObject : MonoBehaviour
{
    public Rigidbody2D rb;
    PolygonCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();
    }
    
    public void RemoveCollider()
    {
        collider.enabled = false;
        rb.simulated = false;
    }

    public void RecoverCollider()
    {
        collider.enabled = true;
        collider.isTrigger = false;
        rb.simulated = true;
    }
}

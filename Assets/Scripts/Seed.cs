using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    Rigidbody2D rb;
    public float force = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 touchPosition;
        if (Input.GetMouseButton(0))
        {
            touchPosition = Input.mousePosition;
        } else if (Input.touchCount == 1)
        {
            touchPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }
        touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        Vector2 forceVector = (transform.position - touchPosition).normalized * force;
        rb.AddForce(forceVector);
    }

    private void OnBecameInvisible()
    {
        GameManager.Instance.GameOver();
    }
}

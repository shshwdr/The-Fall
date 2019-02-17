using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class Cloud : MonoBehaviour
{
    public float movingTime = 5f;
    public Transform[] patrols;
    public float slowDownRate = 1.5f;
    public float immediateFallChange = 2f;
    public float immediateJumpChange = 3f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = patrols[0].position;
        TweenMove();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "seed")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(0, rb.velocity.y * immediateJumpChange, 0);
            rb.angularDrag = 0.05f;
            rb.transform.parent = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "seed")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(0, rb.velocity.y / immediateFallChange, 0);
            rb.transform.parent = transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "seed")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(0, rb.velocity.y / slowDownRate, 0);
            rb.angularDrag = 0.5f;
        }
    }

    private void TweenMove()
    {
        System.Action<ITween<Vector3>> updateCirclePos = (t) =>
        {
            transform.position = t.CurrentValue;
            //GetComponent<Tran>().MovePosition(t.CurrentValue);
        };

        System.Action<ITween<Vector3>> circleMoveCompleted = (t) =>
        {
            TweenMove();
        };

        Vector3 startPos = patrols[0].position;
        Vector3 endPos = patrols[1].position;

        // completion defaults to null if not passed in
        gameObject.Tween("moveCloud", startPos, endPos, movingTime, TweenScaleFunctions.Linear, updateCirclePos)
            .ContinueWith(new Vector3Tween().Setup(endPos, startPos, movingTime, TweenScaleFunctions.Linear, updateCirclePos, circleMoveCompleted));
    }
    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class Star : MonoBehaviour
{
    Transform starCollectTarget;
    // Start is called before the first frame update
    void Start()
    {
        starCollectTarget = GameObject.Find("starCollectTarget").transform;
    }

    private void TweenMove()
    {
        System.Action<ITween<Vector3>> updateCirclePos = (t) =>
        {
            GetComponent<Rigidbody2D>().MovePosition(t.CurrentValue);
        };

        System.Action<ITween<Vector3>> circleMoveCompleted = (t) =>
        {
            Debug.Log("star move completed");
            Destroy(gameObject);
        };

        Vector3 currentPos = transform.position;
        Vector3 starMoveTarget = Camera.main.ScreenToWorldPoint(starCollectTarget.position);
        //currentPos.z = startPos.z = midPos.z = endPos.z = 0.0f;

        // completion defaults to null if not passed in
        gameObject.Tween("MoveCircle", currentPos, starMoveTarget, 1f, TweenScaleFunctions.CubicEaseIn, updateCirclePos, circleMoveCompleted);
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "seed")
        {
            GameManager.Instance.GetStar();
            GetComponent<PolygonCollider2D>().enabled = false;
            TweenMove();
            //Vector3 starMoveTarget = Camera.main.ScreenToWorldPoint(starCollectTarget.position);
            //GetComponent<Rigidbody2D>().MovePosition(starMoveTarget);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

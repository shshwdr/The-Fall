using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class PatrolObject: MonoBehaviour
{
    public float movingSpeed = 5f;
    public float movingTime = 0f;
    public bool useStartPosition = false;
    public Transform[] patrols;
    // Start is called before the first frame update
    void Start()
    {
        if (!useStartPosition)
        {

            transform.position = patrols[0].position;
        }
        Vector3 startPos = patrols[0].position;
        Vector3 endPos = patrols[1].position;
        float length = Vector3.Distance(startPos, endPos);
        if (movingSpeed != 0)
        {
            movingTime = length / movingSpeed;
        }
        TweenMove();
    }
    

    private void TweenMove()
    {
        System.Action<ITween<Vector3>> updateCirclePos = (t) =>
        {
            if(this != null)
            {

                transform.position = t.CurrentValue;
            }
            //GetComponent<Tran>().MovePosition(t.CurrentValue);
        };

        System.Action<ITween<Vector3>> circleMoveCompleted = (t) =>
        {
            if (this != null)
            {
                TweenMove();
            }
        };
        Vector3 startPos = transform.position;
        Vector3 endPos = patrols[1].position;
        // completion defaults to null if not passed in
        gameObject.Tween("moveCloud"+transform.parent.name, startPos, endPos, movingTime, TweenScaleFunctions.Linear, updateCirclePos)
            .ContinueWith(new Vector3Tween().Setup(endPos, startPos, movingTime, TweenScaleFunctions.Linear, updateCirclePos, circleMoveCompleted));
    }
    // Update is called once per frame
    void Update()
    {

    }
}

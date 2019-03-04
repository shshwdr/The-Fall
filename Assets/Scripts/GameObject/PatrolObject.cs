using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class PatrolObject: MonoBehaviour
{
    public float movingTime = 5f;
    public Transform[] patrols;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = patrols[0].position;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using TMPro;

public class Star : MonoBehaviour
{
    Transform starCollectTarget;
    EasyTween starNumTween;
    public float animTime = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        starCollectTarget = GameObject.Find("starCollectTarget").transform;
        starNumTween = GameObject.Find("starNum").GetComponent<EasyTween>();
    }

    private void TweenMove()
    {
        System.Action<ITween<Vector3>> updateCirclePos = (t) =>
        {
            if (this != null)
            {

                GetComponent<Rigidbody2D>().MovePosition(t.CurrentValue);
            }
        };

        System.Action<ITween<Vector3>> circleMoveCompleted = (t) =>
        {
            if (this != null)
            {
                //Debug.Log("star move completed");
                Destroy(gameObject);
            }
        };

        Vector3 currentPos = transform.position;
        Vector3 starMoveTarget = Camera.main.ScreenToWorldPoint(starCollectTarget.position);
        //currentPos.z = startPos.z = midPos.z = endPos.z = 0.0f;

        // completion defaults to null if not passed in
        gameObject.Tween("MoveStar", currentPos, starMoveTarget, animTime, TweenScaleFunctions.CubicEaseIn, updateCirclePos, circleMoveCompleted);
        Invoke("AnimStarNum", animTime-0.1f);
    }

    private void AnimStarNum(){
        starNumTween.GetComponent<TextMeshProUGUI>().text = "x " + GameManager.Instance.starNum;
        starNumTween.PlayOpenAnimations();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.IsGameEnd)
        {
            return;
        }
        if (collision.tag == "seed")
        {
            AchievementManager.Instance.UnlockAchievement(GPGSIds.achievement_twinkle_twinkle_little_star_i);
            GameManager.Instance.CollectStar();
            GetComponent<PolygonCollider2D>().enabled = false;
            TweenMove();
            //Vector3 starMoveTarget = Camera.main.ScreenToWorldPoint(starCollectTarget.position);
            //GetComponent<Rigidbody2D>().MovePosition(starMoveTarget);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    starNumTween.PlayOpenAnimations();
        //}
    }
}

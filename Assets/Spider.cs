using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float detectWidthRange = 1;
    public float moveDownSpeed = 5;
    public float moveUpSpeed = 1;
    public float detectHeightRange = 7f;
    public StickyObject web;
    Vector3 originPosition;
    GameObject targetObject;
    float targetY = 0f;
    string targetTag;
    Rigidbody2D rb;
    enum SpiderStateEnum { wait, rushToSeed, moveBack}
    SpiderStateEnum spiderStateEnum;
    // Start is called before the first frame update
    void Start()
    {
        targetObject = GameObject.Find("seed");
        targetTag = "seed";
        rb = GetComponent<Rigidbody2D>();
        spiderStateEnum = SpiderStateEnum.wait;
        originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (spiderStateEnum)
        {
            case SpiderStateEnum.wait:
                if (targetObject.transform.position.y < transform.position.y &&
                    targetObject.transform.position.y > transform.position.y-detectHeightRange&&
                    targetObject.transform.position.x > transform.position.x - detectWidthRange &&
                    targetObject.transform.position.x < transform.position.x + detectWidthRange&&
                    !web.caughtSeed)
                {
                    spiderStateEnum = SpiderStateEnum.rushToSeed;
                    targetY = targetObject.transform.position.y - 1;
                }
                break;
            case SpiderStateEnum.rushToSeed:
                rb.MovePosition(transform.position + -Vector3.up * moveDownSpeed * Time.deltaTime);
                if (transform.position.y < targetY)
                {
                    spiderStateEnum = SpiderStateEnum.moveBack;
                }
                break;
            case SpiderStateEnum.moveBack:
                rb.MovePosition(transform.position + Vector3.up * moveUpSpeed * Time.deltaTime);
                if (transform.position.y >=originPosition.y)
                {
                    ColliderObject colliderObject = targetObject.GetComponent<ColliderObject>();
                    colliderObject.RecoverCollider();
                    colliderObject.rb.transform.SetParent(null, true);
                    spiderStateEnum = SpiderStateEnum.wait;
                }
                break;
        }
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (spiderStateEnum == SpiderStateEnum.rushToSeed&& collision.tag == targetTag)
        {

            ColliderObject colliderObject = collision.GetComponent<ColliderObject>();
            colliderObject.RemoveCollider();
            colliderObject.rb.transform.SetParent(transform, true);
            spiderStateEnum = SpiderStateEnum.moveBack;
        }
    }
}

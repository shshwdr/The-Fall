﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float delayMin = 0;
    public float delayMax = 1;
    public float speed = 3;
    public Transform nestStop;
    public float birdFlyTriggerHeight = 3;
    Vector3 direction;
    Vector3 flyStartPosition;
    Vector3 target;

    float delay = 0;
    enum BirdStateEnum { wait,flyToSeed,flyToNest,pauseOnNest,flyAway}
    BirdStateEnum birdStateEnum;
    GameObject nestCover;
    Seed seed;
    
    float currentTime;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        birdStateEnum = BirdStateEnum.wait;
        delay = Random.Range(delayMin, delayMax);

        nestCover = transform.parent.Find("nest_cover").gameObject;
    }

    void StartFly()
    {
        birdStateEnum = BirdStateEnum.flyToSeed;

        flyStartPosition = transform.position;
        target = GameObject.Find("seed").transform.position;
        direction = (target - flyStartPosition).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (birdStateEnum == BirdStateEnum.flyToSeed && collision.tag == "seed")
        {
            seed = collision.GetComponent<Seed>();
            seed.RemoveCollider();
            seed.transform.rotation = Quaternion.identity;
            seed.transform.position = transform.Find("beak").position;
            seed.transform.parent = transform;
            birdStateEnum = BirdStateEnum.flyToNest;

            //hide nest cover
            nestCover.SetActive(false);

            flyStartPosition = transform.position;
            target = nestStop.position;
            direction = (target - flyStartPosition).normalized;
            speed /= 2f;
        }
    }

    void DropSeed()
    {
        seed.RecoverCollider();
        seed.transform.parent = null;
        seed.rb.velocity = new Vector3(0, -1, 0);
        birdStateEnum = BirdStateEnum.flyAway;
        nestCover.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameEnd)
        {
            return;
        }
        switch (birdStateEnum)
        {
            case BirdStateEnum.wait:
                if(Seed.Instance.transform.position.y<transform.position.y - birdFlyTriggerHeight) { 
                    Invoke("StartFly", delay);
                }
                break;
            case BirdStateEnum.flyToSeed:
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                break;
            case BirdStateEnum.flyToNest:
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                Vector3 vectorCur = transform.position - flyStartPosition;
                Vector3 vectorTarget = target - flyStartPosition;
                float lengthCur = Vector3.Dot(vectorCur, direction);
                if (lengthCur* lengthCur >= vectorTarget.sqrMagnitude)
                {
                    birdStateEnum = BirdStateEnum.pauseOnNest;
                    Invoke( "DropSeed",0.5f);
                }
                break;
            case BirdStateEnum.pauseOnNest:
                break;
            case BirdStateEnum.flyAway:
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                break;
        }
    }
}

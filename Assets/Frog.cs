using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class Frog : MonoBehaviour
{
    public float waitTime = 1f;
    public float jumpTime = 1.5f;
    bool isFlipped;

    CircleCollider2D collider;
    SpriteAnim spriteAnim;
    PathFollower pathFollower;
    float currentTime;

    enum State { wait,jump};
    State state;
    
    // Start is called before the first frame update
    void Start()
    {
        pathFollower = GetComponentInChildren<PathFollower>();
        pathFollower.Pause();
        collider = GetComponentInChildren<CircleCollider2D>();
        spriteAnim = GetComponentInChildren<SpriteAnim>();
        spriteAnim.Init();

        pathFollower.speed = pathFollower.pathCreator.path.length / jumpTime;
        spriteAnim. frameSeconds = jumpTime / spriteAnim.spriteNum;
        state = State.wait;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        switch (state)
        {
            case State.wait:
                if (currentTime >= waitTime)
                {
                    state = State.jump;
                    pathFollower.Resume(isFlipped);
                    spriteAnim.Resume();
                    currentTime = 0;
                }
                break;
            case State.jump:
                if (currentTime >= jumpTime)
                {
                    state = State.wait;
                    isFlipped = !isFlipped;
                    pathFollower.Pause();
                    spriteAnim.Pause();
                    currentTime = 0;
                }
                break;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "seed")
        {
            //Debug.Log("hit on seed!");
            GameManager.Instance.GameOver();
        }
    }
}

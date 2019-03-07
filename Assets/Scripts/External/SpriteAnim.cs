using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnim : MonoBehaviour
{
    public bool loop;
    public float frameSeconds = 1;
    //The file location of the sprites within the resources folder
    public string location;
    private SpriteRenderer spr;
    Sprite[] sprites;
    public int spriteNum
    {
        get { return sprites.Length; }
    }
    private int frame = 0;
    private float deltaTime = 0;
    public bool isFliped;
    bool finishedInit;
    bool isPaused;
    public bool initByOthers;
    // Use this for initialization

    private void Start()
    {
        if (!initByOthers) {
        spr = GetComponent<SpriteRenderer>();

        sprites = Resources.LoadAll<Sprite>("animSprite/" + location);
        finishedInit = true;
        }
    }
    public void Init()
    {
        spr = GetComponent<SpriteRenderer>();
        
        sprites = Resources.LoadAll<Sprite>("animSprite/"+location);
        finishedInit = true;
        isPaused = true;
    }

    public void ResetAnim()
    {
        frame = 0;
    }

    public void Pause()
    {
        ResetAnim();
        spr.sprite = sprites[frame];
        FlipAnim();
        isPaused = true;
    }

    public void Resume()
    {
        ResetAnim();
        isPaused = false;
    }

    public void FlipAnim()
    {
        isFliped = !isFliped;
        transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!finishedInit)
        {
            return;
        }
        if (isPaused)
        {
            return;
        }
        //Keep track of the time that has passed
        deltaTime += Time.deltaTime;

        /*Loop to allow for multiple sprite frame 
         jumps in a single update call if needed
         Useful if frameSeconds is very small*/
        while (deltaTime >= frameSeconds)
        {
            deltaTime -= frameSeconds;
            frame++;
            if (loop)
                frame %= sprites.Length;
            //Max limit
            else if (frame >= sprites.Length)
                frame = sprites.Length - 1;
        }
        //Animate sprite with selected frame
        spr.sprite = sprites[frame];
    }
}

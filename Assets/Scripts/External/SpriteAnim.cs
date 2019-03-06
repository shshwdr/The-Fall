using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnim : MonoBehaviour
{
    public bool loop;
    public float frameSeconds = 1;
    public float duration = 0;
    //The file location of the sprites within the resources folder
    public string location;
    private SpriteRenderer spr;
    Sprite[] sprites;
    private int frame = 0;
    private float deltaTime = 0;
    public bool isFliped;

    // Use this for initialization
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        
        sprites = Resources.LoadAll<Sprite>("animSprite/"+location);
        if (duration > 0)
        {
            frameSeconds = duration / sprites.Length;
        }
    }

    public void ResetAnim()
    {
        frame = 0;
    }

    public void FlipAnim()
    {
        isFliped = !isFliped;
        transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
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

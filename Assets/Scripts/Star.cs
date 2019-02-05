using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    Transform starCollectTarget;
    // Start is called before the first frame update
    void Start()
    {
        starCollectTarget = GameObject.Find("starCollectTarget").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "seed")
        {
            GameManager.Instance.GetStar();
            GetComponent<PolygonCollider2D>().enabled = false;
            Canvas c;
            Vector3 starMoveTarget = Camera.main.ScreenToWorldPoint(starCollectTarget.position);
            GetComponent<Rigidbody2D>().MovePosition(starMoveTarget);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

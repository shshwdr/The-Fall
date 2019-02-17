using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public float movingSpeed = 2;
    GameObject ground;
    bool hasSeenGround;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 seedTrans = GameObject.Find("seed").transform.position;
        ground = GameObject.FindGameObjectWithTag("ground");
        transform.position = new Vector3(seedTrans.x, seedTrans.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSeenGround)
        {
            return;
        }
        Vector3 groundScreenPosition = GetComponent<Camera>().WorldToScreenPoint(ground.transform.position);
        if (groundScreenPosition.y > 0)
        {
            hasSeenGround = true;
        }
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y- movingSpeed * Time.deltaTime,transform.position.z);
    }
}

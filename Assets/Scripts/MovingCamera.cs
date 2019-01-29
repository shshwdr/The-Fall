using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public float movingSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y- movingSpeed * Time.deltaTime,transform.position.z);
    }
}

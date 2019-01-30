using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    Rigidbody2D rb;
    public float force = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ground")
        {
            Debug.Log("hit ground");
            Vector3 dir = (collision.transform.position - transform.position).normalized;
            Vector3 hitPosition = transform.position;
            Debug.Log("colli " + collision.transform.position + " self " + transform.position);
            StartCoroutine(GenerateSprout(hitPosition));
            GameManager.Instance.Win();
        }
    }


    IEnumerator GenerateSprout(Vector3 hitPosition)
    {
        yield return new WaitForSeconds(1);
        GameObject prefab = Resources.Load("Prefabs/sprout", typeof(GameObject)) as GameObject;
        GameObject go = Instantiate(prefab);
        go.transform.position = hitPosition;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 touchPosition;
        if (Input.GetMouseButton(0))
        {
            touchPosition = Input.mousePosition;
        } else if (Input.touchCount == 1)
        {
            touchPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }
        touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        Vector2 forceVector = (transform.position - touchPosition).normalized * force;
        rb.AddForce(forceVector);
    }

    private void OnBecameInvisible()
    {
        Debug.Log("can't see me");
        GameManager.Instance.GameOver();
    }
}

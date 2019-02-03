using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public Rigidbody2D rb;
    PolygonCollider2D collider;
    public float force = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ground")
        {
            //Debug.Log("hit ground");
            Vector3 dir = (collision.transform.position - transform.position).normalized;
            Vector3 hitPosition = transform.position;
            //Debug.Log("colli " + collision.transform.position + " self " + transform.position);
            StartCoroutine(GenerateSprout(hitPosition+dir*0.5f));
            GameManager.Instance.Win();
        }
    }

    public void RemoveCollider()
    {
        collider.enabled = false;
        rb.simulated = false;
    }

    public void RecoverCollider()
    {
        collider.enabled = true;
        rb.simulated = true;
    }


    IEnumerator GenerateSprout(Vector3 hitPosition)
    {
        yield return new WaitForSeconds(1f);
        GameObject prefab = Resources.Load("Prefabs/sprout", typeof(GameObject)) as GameObject;
        GameObject go = Instantiate(prefab);
        go.transform.position = hitPosition;
        StartCoroutine(ShowWinScreen());
    }
    IEnumerator ShowWinScreen()
    {
        yield return new WaitForSeconds(1f);
        GameWinViewController.CreateGameWinView();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsWin || GameManager.Instance.IsGameOver)
        {
            return;
        }
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
        //Debug.Log("can't see me");
        GameManager.Instance.GameOver();
    }
}

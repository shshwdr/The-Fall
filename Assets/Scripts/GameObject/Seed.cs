using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Seed : Singleton<Seed>
{
    public Rigidbody2D rb;
    PolygonCollider2D collider;
    public float force = 1;
    public float distanceToShowHitPuff = 1f;
    GameObject normalPuff;
    GameObject hitPuff;
    GameObject hitPuffObject;
    GameObject normalPuffObject;
    GameObject puffObject;
    
    public float lengthStartScale = 2f;
    public float minHitPuffLength = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();

        normalPuff = Resources.Load("Prefabs/normalPuff") as GameObject;
        hitPuff = Resources.Load("Prefabs/hitPuff") as GameObject;
        hitPuffObject = Instantiate(hitPuff, Vector3.zero, Quaternion.identity);
        hitPuffObject.SetActive(false);
        normalPuffObject = Instantiate(normalPuff, Vector3.zero, Quaternion.identity);
        normalPuffObject.SetActive(false);
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
        GameEndViewController.CreateGameWinView();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if(GameManager.Instance.IsGameEnd)
        {
            hitPuffObject.SetActive(false);
            normalPuffObject.SetActive(false);
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
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
            normalPuffObject.SetActive(false);
            hitPuffObject.SetActive(false);
            return;
        }
        
        touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        Vector3 puffPosition = new Vector3(touchPosition.x, touchPosition.y, 0);
        float distanceToTouch = Vector2.Distance(touchPosition, transform.position);
        if (distanceToTouch > distanceToShowHitPuff)
        {
            if (puffObject != normalPuffObject)
            {
                hitPuffObject.SetActive(false);
                normalPuffObject.GetComponentInChildren<SpriteAnim>().ResetAnim();
            }
            if(distanceToTouch< lengthStartScale)
            {
                Vector3 scale = normalPuffObject.transform.localScale;
                scale.y = distanceToTouch / lengthStartScale;
                normalPuffObject.transform.localScale = scale;
            }
            puffObject = normalPuffObject;
        }
        else
        {
            if (puffObject != hitPuffObject)
            {
                normalPuffObject.SetActive(false);
                hitPuffObject.GetComponentInChildren<SpriteAnim>().ResetAnim();
            }
                Vector3 scale = hitPuffObject.transform.localScale;
                scale.y = distanceToTouch / distanceToShowHitPuff;
            scale.y = Mathf.Max(scale.y, minHitPuffLength);
            scale.x = Mathf.Min(1.8f, 1 / scale.y);
            hitPuffObject.transform.localScale = scale;


            puffObject = hitPuffObject;
        }
        if (!puffObject.active)
        {
            puffObject.GetComponentInChildren<SpriteAnim>().ResetAnim();
        }
            puffObject.transform.position = puffPosition;
            Vector3 direction = (touchPosition - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction, new Vector3(0,0,1));
            puffObject.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            //Debug.Log("puff rotation"+puffObject.transform.rotation);
            puffObject.SetActive(true);
        Vector2 forceVector = (transform.position - touchPosition).normalized * force;
        rb.AddForce(forceVector);
    }

    public void HitByLightning()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        GameManager.Instance.GameOver();
    }


    private void OnBecameInvisible()
    {
        //Debug.Log("can't see me");
        GameManager.Instance.GameOver();
    }
}

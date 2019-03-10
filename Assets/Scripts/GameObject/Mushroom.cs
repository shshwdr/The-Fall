using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Color poisonColor;
    public float poisonDuration = 3f;
    public float poisonVFXDuration = 1f;
    Color originColor;
    float poisonTime;
    SpriteAnim[] poisonVFXs;
    int poisonVFXIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        originColor = Camera.main.GetComponentInChildren<SpriteRenderer>().color;
        poisonVFXs = Camera.main.GetComponentsInChildren<SpriteAnim>();
        foreach (SpriteAnim vfx in poisonVFXs)
        {
            vfx.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (EventSystem.current.IsPointerOverGameObject())
    //    {
    //        return;
    //    }
    //    Vector3 touchPosition;
    //    if (Input.GetMouseButton(0))
    //    {
    //        touchPosition = Input.mousePosition;
    //    }
    //    else if (Input.touchCount == 1)
    //    {
    //        touchPosition = Input.GetTouch(0).position;
    //    }
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit2D[] hits = Physics2D.RaycastAll(
    //    foreach(RaycastHit2D hit in hits)
    //    {
    //        Debug.Log(hit.collider.name);
    //    }

    //    touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
    //}
    private void OnMouseEnter()
    {
        //Debug.Log("mouse enter");
        AddPoison();
    }
    private void OnMouseDrag()
    {
        //Debug.Log("mouse drag");
        AddPoison();
    }

    void AddPoison()
    {
        if (poisonTime <= 0)
        {
            Camera.main.GetComponentInChildren<SpriteRenderer>().color = poisonColor;
            StartCoroutine(ShowVFX());
        }
        poisonTime = poisonDuration;
        
    }

    IEnumerator ShowVFX()
    {
        poisonVFXs[poisonVFXIndex].gameObject.SetActive(true);
        poisonVFXs[poisonVFXIndex].ResetAnim();
        float[,] showPositions = {{-2.3f, 0, -4.5f, 0}, { 0,2.3f, 0,4.5f }, { -2.3f, 0, 0, 4.5f }, { 0, 2.3f, -4.5f, 0 } };
        Vector3 position = new Vector3(Random.Range(showPositions[poisonVFXIndex%4,0], showPositions[poisonVFXIndex % 4, 1]), 
            Random.Range(showPositions[poisonVFXIndex % 4, 2], showPositions[poisonVFXIndex % 4, 3]), 
            poisonVFXs[poisonVFXIndex].gameObject.transform.position.z);
        poisonVFXs[poisonVFXIndex].gameObject.transform.position = position;
        yield return new WaitForSeconds(poisonVFXDuration/(float)(poisonVFXs.Length));
        poisonVFXIndex = (poisonVFXIndex + 1) % poisonVFXs.Length;
        StartCoroutine(ShowVFX());

    }
    void RemovePoision()
    {
        Camera.main.GetComponentInChildren<SpriteRenderer>().color = originColor;
        StopAllCoroutines();
        foreach (SpriteAnim vfx in poisonVFXs)
        {
            vfx.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (poisonTime >= 0)
        {
            poisonTime -= Time.deltaTime;

            if (poisonTime < 0)
            {
                RemovePoision();
            }
        }
    }
}

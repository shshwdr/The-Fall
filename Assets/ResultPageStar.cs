using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPageStar : MonoBehaviour
{
    GameObject star ;
    GameObject shiny ;
    private void Start()
    {
        star = transform.Find("star").gameObject;
        shiny = transform.Find("shiny").gameObject;
        HideStar();
    }
    
    public IEnumerator ShowStarAnim(float delay)
    {
        yield return new WaitForSeconds(delay);
        star.SetActive(true);
        shiny.SetActive(true);
    }
    public void HideStar()
    {
        star.SetActive(false);
        shiny.SetActive(false);
    }
}

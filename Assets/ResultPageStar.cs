using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPageStar : MonoBehaviour
{
    GameObject star ;
    GameObject shiny ;

    EasyTween TweenToControl;
    
    private void Start()
    {
        star = transform.Find("star").gameObject;
        TweenToControl = star.GetComponent<EasyTween>();
        shiny = transform.Find("shiny").gameObject;
        HideStar();
    }
    
    public IEnumerator ShowStarAnim(float delay, AnimationCurve animationCurve)
    {
        Start();
        yield return new WaitForSeconds(delay);
        //Debug.Log("v1 " + TweenToControl.rectTransform.anchoredPosition + " v2 " + GetComponent<RectTransform>().anchoredPosition);
        TweenToControl.SetAnimationPosition(TweenToControl.rectTransform.anchoredPosition, Vector3.zero, animationCurve, null);
        TweenToControl.SetAnimationScale(Vector3.zero, new Vector3(1, 1, 1), animationCurve, null);
        TweenToControl.OpenCloseObjectAnimation();
        star.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ShowShiny();
    }

    public void ShowShiny()
    {
        shiny.GetComponent<EasyTween>().OpenCloseObjectAnimation();

    }
    public void HideStar()
    {
        star.SetActive(false);
       // shiny.SetActive(false);
    }
}

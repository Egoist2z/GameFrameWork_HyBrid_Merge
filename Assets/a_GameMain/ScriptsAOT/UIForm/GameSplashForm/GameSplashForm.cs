using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameSplashForm : MonoBehaviour
{
    [SerializeReference]
    private Image point;
    [SerializeReference]
    private Text lable;
    [SerializeReference]
    private CanvasGroup group;

    public const string title= "Demo_1 ";

    private void Start()
    {
        point.fillAmount = 0;
        lable.text = "";
        group.alpha = 1;
    }

    public void Play(Action action) 
    {
        StartCoroutine(Splash(action));
    }
    
    private IEnumerator Splash(Action action) 
    {        
        while (point.fillAmount<1)
        {
            point.fillAmount += 0.05f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        for (int i = 0; i < title.Length; i++)
        {
            var str = title.Substring(0,i);
            lable.text = str;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        while (group.alpha>0)
        {
            group.alpha -= 0.1f;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        yield return new WaitForSecondsRealtime(0.3f);
        if (action!=null)
        {
            action.Invoke();
        }
        DestroyImmediate(this.gameObject);
        yield return null;
    }
}

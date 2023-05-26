using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework;

public class MeumForm : UGUIFormLogic
{
    [SerializeReference]
    private Button red;

    [SerializeReference]
    private Button black;

    [SerializeReference]
    private Button quit;

    [SerializeReference]
    private Image colorPanel;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        red.onClick.AddListener(()=> { colorPanel.color = Color.red; colorPanel.gameObject.SetActive(true); });
        black.onClick.AddListener(() => { colorPanel.color = Color.black; colorPanel.gameObject.SetActive(true); });
        quit.onClick.AddListener(() => { GameEntry.HotUpdate.RunMethod("PrintLog","Run",null,null); });
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }


    

}

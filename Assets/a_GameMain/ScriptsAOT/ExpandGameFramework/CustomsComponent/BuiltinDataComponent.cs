//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;

public class BuiltinDataComponent : GameFrameworkComponent
{
    [SerializeField]
    private TextAsset m_BuildInfoTextAsset = null;

    private LodingForm m_lodingForm = null;    

    //[SerializeField]
    //private TextAsset m_DefaultDictionaryTextAsset = null;

    //[SerializeField]
    //private Loding m_UpdateResourceFormTemplate = null;

    private BuildInfo m_BuildInfo = null;

    public BuildInfo BuildInfo
    {
        get
        {
            return m_BuildInfo;
        }
    }

    public LodingForm LodingFormTemplate
    {
        get
        {
            return m_lodingForm;
        }
    }
    
    public void InitBuildInfo()
    {
        if (m_BuildInfoTextAsset == null || string.IsNullOrEmpty(m_BuildInfoTextAsset.text))
        {
            Log.Info("Build info can not be found or empty.");
            return;
        }
        m_BuildInfo = Utility.Json.ToObject<BuildInfo>(m_BuildInfoTextAsset.text);
        if (m_BuildInfo == null)
        {
            Log.Warning("Parse build info failure.");
            return;
        }
    }

    public void InitLodingForm()
    {
        GameObject form = Resources.Load<GameObject>("LodingForm");
        if (form == null)
        {
            Debug.LogError("LodingForm Lost");
            return;
        }
        m_lodingForm =GameObject.Instantiate(form, GameEntry.UI.UIInstanceRoot).GetComponent<LodingForm>();        
        RectTransform rect = (RectTransform)m_lodingForm.transform;
        rect.localPosition = Vector3.zero;
        rect.sizeDelta = Vector2.zero;
        m_lodingForm.ResetForm();
    }

    public void UnLoadLodingForm()
    {
        DestroyImmediate(m_lodingForm.gameObject);
        m_lodingForm = null;
    }

    public void InitSplash(Action action)
    {
        GameObject form = Resources.Load<GameObject>("GameSplashForm");
        if (form==null)
        {
            Debug.LogError("GameSplashForm Lost");
            return;
        }
        form = GameObject.Instantiate(form, GameEntry.UI.UIInstanceRoot);
        form.transform.localPosition = Vector3.zero;
        form.transform.localScale = Vector3.one;
        form.GetComponent<GameSplashForm>().Play(action);
    }


    //public void InitDefaultDictionary()
    //{
    //    if (m_DefaultDictionaryTextAsset == null || string.IsNullOrEmpty(m_DefaultDictionaryTextAsset.text))
    //    {
    //        Log.Info("Default dictionary can not be found or empty.");
    //        return;
    //    }

    //    if (!GameEntry.Localization.ParseData(m_DefaultDictionaryTextAsset.text))
    //    {
    //        Log.Warning("Parse default dictionary failure.");
    //        return;
    //    }
    //}
}

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
using AOT_UIForm;
public class BuiltinDataComponent : GameFrameworkComponent
{
    [SerializeField]
    private TextAsset m_BuildInfoTextAsset = null;

    [SerializeField]
    private TextAsset m_DefaultDictionaryTextAsset = null;


    private LodingForm m_lodingForm = null;

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

    public void InitDefaultDictionary()
    {
        if (m_DefaultDictionaryTextAsset == null || string.IsNullOrEmpty(m_DefaultDictionaryTextAsset.text))
        {
            Log.Info("Default dictionary can not be found or empty.");
            return;
        }

        if (!GameEntry.Localization.ParseData(m_DefaultDictionaryTextAsset.text))
        {
            Log.Warning("Parse default dictionary failure.");
            return;
        }
    }

    /// <summary>
    /// 加载界面
    /// </summary>
    public void OpenLodingForm()
    {
        GameObject form = Resources.Load<GameObject>("LodingForm");
        if (form == null)
        {
            Log.Error("LodingForm Lost");
            return;
        }
        m_lodingForm = GameObject.Instantiate(form, GameEntry.UI.UIInstanceRoot).GetComponent<LodingForm>();
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

    /// <summary>
    /// 游戏进入动画
    /// </summary>
    /// <param name="action"></param>
    public void OpenSplash(Action action)
    {
        GameObject form = Resources.Load<GameObject>("GameSplashForm");
        if (form == null)
        {
            Log.Error("GameSplashForm Lost");
            return;
        }
        form = GameObject.Instantiate(form, GameEntry.UI.UIInstanceRoot);
        form.transform.localPosition = Vector3.zero;
        form.transform.localScale = Vector3.one;
        form.GetComponent<GameSplashForm>().Play(action);
    }

    /// <summary>
    /// 弹出确认界面
    /// </summary>
    /// <param name="data"></param>
    public void OpenGotoUpdateForm(GotoUpdateFormData data)
    {
        GameObject form = Resources.Load<GameObject>("GotoUpdateForm");
        if (form == null)
        {
            Log.Error("GameSplashForm Lost");
            return;
        }
        form = GameObject.Instantiate(form, GameEntry.UI.UIInstanceRoot);
        form.transform.localPosition = Vector3.zero;
        form.transform.localScale = Vector3.one;
        form.GetComponent<GotoUpdateForm>().InitForm(data);
    }


}

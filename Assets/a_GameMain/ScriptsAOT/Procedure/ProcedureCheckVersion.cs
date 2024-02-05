//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class ProcedureCheckVersion : ProcedureBase
{
    private bool m_CheckVersionComplete = false;
    private bool m_NeedUpdateVersion = false;
    private VersionInfo m_VersionInfo = null;

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        m_CheckVersionComplete = false;
        m_NeedUpdateVersion = false;
        m_VersionInfo = null;

        GameEntry.Event.Subscribe(WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
        GameEntry.Event.Subscribe(WebRequestFailureEventArgs.EventId, OnWebRequestFailure);
        GameEntry.BuiltinData.LodingFormTemplate.SetLodingState("检查版本信息中...");
        GameEntry.WebRequest.AddWebRequest(Utility.Text.Format(GameEntry.BuiltinData.BuildInfo.CheckVersionUrl, GetPlatformPath()), this);
    }

    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
        GameEntry.Event.Unsubscribe(WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
        GameEntry.Event.Unsubscribe(WebRequestFailureEventArgs.EventId, OnWebRequestFailure);
        base.OnLeave(procedureOwner, isShutdown);
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        if (!m_CheckVersionComplete)
        {
            return;
        }

        if (m_NeedUpdateVersion)
        {
            procedureOwner.SetData<VarInt32>("VersionListLength", m_VersionInfo.VersionListLength);
            procedureOwner.SetData<VarInt32>("VersionListHashCode", m_VersionInfo.VersionListHashCode);
            procedureOwner.SetData<VarInt32>("VersionListCompressedLength", m_VersionInfo.VersionListCompressedLength);
            procedureOwner.SetData<VarInt32>("VersionListCompressedHashCode", m_VersionInfo.VersionListCompressedHashCode);
            ChangeState<ProcedureUpdateVersion>(procedureOwner);
        }
        else
        {
            ChangeState<ProcedureVerifyResources>(procedureOwner);
        }
    }

    private void GotoUpdateApp()
    {
        string url = null;
#if UNITY_EDITOR
        return;
#elif UNITY_WIN
        url = GameEntry.BuiltinData.BuildInfo.WindowsAppUrl;
#elif UNITY_IOS
            url = GameEntry.BuiltinData.BuildInfo.IOSAppUrl;
#elif UNITY_ANDROID
        url = GameEntry.BuiltinData.BuildInfo.AndroidAppUrl;
#endif
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
    }

    private void OnWebRequestSuccess(object sender, GameEventArgs e)
    {
        WebRequestSuccessEventArgs ne = (WebRequestSuccessEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        // 解析版本信息
        byte[] versionInfoBytes = ne.GetWebResponseBytes();
        string versionInfoString = Utility.Converter.GetString(versionInfoBytes);
        m_VersionInfo = Utility.Json.ToObject<VersionInfo>(versionInfoString);
        if (m_VersionInfo == null)
        {
            Log.Error("Parse VersionInfo failure.");
            return;
        }

        Log.Info("Latest game version is '{0}', local game version is '{1}'", m_VersionInfo.InternalGameVersion.ToString(), Version.GameVersion);


        //当前应用版本低于 CDN配置版本 需要更新整个应用
        if (GameVersionToIntValue(Version.GameVersion) < GameVersionToIntValue(m_VersionInfo.InternalGameVersion))
        {
            // 需要强制更新游戏应用
            var data = new AOT_UIForm.GotoUpdateFormData()
            {
                title = GameEntry.Localization.GetStringOrNull(LocalizationDicKey.UpdateFormTitle),
                content = GameEntry.Localization.GetStringOrNull(LocalizationDicKey.UpdateFormContents),
                quit = () => { UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit); },
                update = GotoUpdateApp,
            };
            GameEntry.BuiltinData.OpenGotoUpdateForm(data);
            return;
        }

        // 设置资源更新下载地址
        GameEntry.Resource.UpdatePrefixUri = Utility.Path.GetRegularPath(m_VersionInfo.UpdatePrefixUri);
        m_CheckVersionComplete = true;
        m_NeedUpdateVersion = GameEntry.Resource.CheckVersionList(m_VersionInfo.InternalResourceVersion) == CheckVersionListResult.NeedUpdate;
    }

    private void OnWebRequestFailure(object sender, GameEventArgs e)
    {
        WebRequestFailureEventArgs ne = (WebRequestFailureEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        Log.Warning("Check version failure, error message is '{0}'.", ne.ErrorMessage);
    }

    private string GetPlatformPath()
    {
#if UNITY_EDITOR_WIN || UNITY_WIN
        return "Windows";
#elif UNITY_ANDROID
        return "Android";
#elif UNITY_IOS
          return "IOS";
#endif
        //return "Android";
        //switch (Application.platform)
        //{
        //    case RuntimePlatform.WindowsEditor:
        //    case RuntimePlatform.WindowsPlayer:
        //        return "Windows";
        //    case RuntimePlatform.OSXEditor:
        //    case RuntimePlatform.OSXPlayer:
        //        return "MacOS";
        //    case RuntimePlatform.IPhonePlayer:
        //        return "IOS";
        //    case RuntimePlatform.Android:
        //        return "Android";
        //    default:
        //        throw new System.NotSupportedException(Utility.Text.Format("Platform '{0}' is not supported.", Application.platform));
        //}
    }


    private int GameVersionToIntValue(string gameVersion)
    {
        var list = gameVersion.Split('.');
        int number = 0;
        foreach (var item in list)
        {
            number += int.Parse(item);
        }
        return number;
    }
}

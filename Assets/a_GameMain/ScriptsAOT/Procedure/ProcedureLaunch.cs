//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Localization;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;



public class ProcedureLaunch : ProcedureBase
{

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        // 构建信息：发布版本时，把一些数据以 Json 的格式写入 Assets/GameMain/Configs/BuildInfo.txt，供游戏逻辑读取
        GameEntry.BuiltinData.InitBuildInfo();

        // 语言配置：设置当前使用的语言，如果不设置，则默认使用操作系统语言
        InitLanguageSettings();

        // 变体配置：根据使用的语言，通知底层加载对应的资源变体
        InitCurrentVariant();
        
        // 默认字典：加载默认字典文件 Assets/GameMain/Configs/DefaultLocalization
        // 此字典文件记录了资源更新前使用的各种语言的字符串，会随 App 一起发布，故不可更新
        GameEntry.BuiltinData.InitDefaultDictionary();
        
        // 运行一帧即切换到 Splash 展示流程
        ChangeState<ProcedureSplash>(procedureOwner);
    }    


    /// <summary>
    /// 初始化语言设置
    /// </summary>
    private void InitLanguageSettings()
    {
        if (GameEntry.Base.EditorResourceMode && GameEntry.Base.EditorLanguage != Language.Unspecified)
        {
            // 编辑器资源模式直接使用 Inspector 上设置的语言
            return;
        }

        var saveLanguage = GameEntry.Setting.GetGameLanguage();        
        Language language = GameEntry.Localization.SystemLanguage;
        
        if (saveLanguage!=Language.Unspecified)
        {
            language = saveLanguage;
        }
        
        if (language != Language.English && language != Language.ChineseSimplified && language != Language.ChineseTraditional)
        {
            // 若是暂不支持的语言，则使用英语
            language = Language.English;                        
        }
        
        GameEntry.Localization.Language = language;
        GameEntry.Setting.SetGameLanguage(language);
        Log.Info("Init language settings complete, current language is '{0}'.", GameEntry.Localization.Language.ToString());
    }

    private void InitCurrentVariant()
    {
        if (GameEntry.Base.EditorResourceMode)
        {
            // 编辑器资源模式不使用 AssetBundle，也就没有变体了
            return;
        }

        string currentVariant = null;
        switch (GameEntry.Localization.Language)
        {
            case Language.English:
                currentVariant = "en-us";
                break;

            case Language.ChineseSimplified:
                currentVariant = "zh-cn";
                break;

            case Language.ChineseTraditional:
                currentVariant = "zh-tw";
                break;            
            default:
                currentVariant = "zh-cn";
                break;
        }

        GameEntry.Resource.SetCurrentVariant(currentVariant);
        Log.Info("Init current variant complete.");        
    }

}


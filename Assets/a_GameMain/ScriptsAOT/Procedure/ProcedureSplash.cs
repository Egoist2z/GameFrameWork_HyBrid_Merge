//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using GameFramework.Resource;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class ProcedureSplash : ProcedureBase
{
    private bool playOver=false;

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
        if (GameEntry.Base.EditorResourceMode)
        {
            playOver = true;
            return;
        }

        // TODO: 这里播放一个 Splash 动画        
        playOver = false;
        GameEntry.BuiltinData.OpenSplash(()=> { playOver = true;});
     }
    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);        
        if (!playOver)
        {
            return;
        }

        GameEntry.BuiltinData.OpenLodingForm();

        if (GameEntry.Base.EditorResourceMode)
        {
            // 编辑器模式
            Log.Info("Editor resource mode detected.");
            ChangeState<ProcedureLoadHotUpdateDll>(procedureOwner);
        }
        else if (GameEntry.Resource.ResourceMode == ResourceMode.Package)
        {
            // 单机模式
            Log.Info("Package resource mode detected.");
            ChangeState<ProcedureInitResources>(procedureOwner);
        }
        else
        {
            // 可更新模式
            Log.Info("Updatable resource mode detected.");            
            ChangeState<ProcedureCheckVersion>(procedureOwner);
        }
    }   
}


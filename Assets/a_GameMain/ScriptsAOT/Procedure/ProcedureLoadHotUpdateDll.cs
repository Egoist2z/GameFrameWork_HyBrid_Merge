//------ ------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using AOT;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using GameFramework.Event;

/// <summary>
/// 加载游戏热更dll
/// </summary>
public class ProcedureLoadHotUpdateDll : ProcedureBase
{  
    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
        //加载热更dll
        GameEntry.HotUpdate.InitHotAssembly();
        GameEntry.BuiltinData.LodingFormTemplate.SetLodingState(GameEntry.Localization.GetStringOrNull(LocalizationDicKey.LodingForm.Preload));
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        return;
        if (GameEntry.HotUpdate.LoadHotAssemblySuccess)
        {
            ChangeState<ProcedurePreload>(procedureOwner);
        }
    }

    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);    
    }    
}


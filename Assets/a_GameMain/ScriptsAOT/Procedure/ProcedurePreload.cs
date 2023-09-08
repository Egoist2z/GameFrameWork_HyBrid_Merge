//------ ------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using GameFramework.Resource;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;


/// <summary>
/// 游戏预加载流程
/// </summary>
public class ProcedurePreload : ProcedureBase
{

    public static string[] DataTableNames =new string[]
    {


    };

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);                

        GameEntry.HotUpdate.InitHotAssembly();//加载热更dll
        GameEntry.BuiltinData.LodingFormTemplate.SetLodingState("加载资源中");
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        if (GameEntry.HotUpdate.LoadHotAssemblySuccess)
        {
            ChangeState<ProcedureMenu>(procedureOwner);
        }
    }

    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
        GameEntry.BuiltinData.UnLoadLodingForm();
    }

}


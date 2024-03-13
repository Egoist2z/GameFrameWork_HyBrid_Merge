//------ ------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;
using GameFramework.Event;
using AOT;

/// <summary>
/// 游戏预加载流程
/// </summary>
public class ProcedurePreload : ProcedureBase
{
    private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();

    public static string[] DataTableNames =new string[]
    {
        "UIFormBaseInfo",
    };

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);        
        m_LoadedFlag.Clear();
        
        GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);        
        ///加载游戏中表格数据
        foreach (var item in DataTableNames)
        {
            LoadDataTable(item);
        }
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        foreach (var item in m_LoadedFlag)
        {
            if (!item.Value)
            {
                return;
            }
        }
        ChangeState<ProcedureMenu>(procedureOwner);
    }

    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
        GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
        GameEntry.BuiltinData.UnLoadLodingForm();
        base.OnLeave(procedureOwner, isShutdown);
    }


    #region 加载游戏数据表    
    private void LoadDataTable(string dataTableName)
    {
        GameEntry.HotUpdate.RunMethod("HotUpdate.Start.StartGame", "LoadDataTable",new object[] { dataTableName });
        var dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, false);
        m_LoadedFlag.Add(dataTableAssetName, false);
    }

    private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
    {
        LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
        m_LoadedFlag[ne.DataTableAssetName] = true;
        Log.Debug("Load data table '{0}' OK.", ne.DataTableAssetName);
    }

    private void OnLoadDataTableFailure(object sender, GameEventArgs e)
    {
        LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs)e;
        Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName, ne.DataTableAssetName, ne.ErrorMessage);
    }
    #endregion

}


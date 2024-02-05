using AOT;
using HotUpdate.UI;
using HotUpdate.DataTables;
using UnityGameFramework.Runtime;

public static class StartGame
{
    /// <summary>
    /// 打开游戏登入 or 菜单界面
    /// </summary>
    public static void OpenMenuForm() 
    {
        GameEntry.UI.InitGroups();        
        GameEntry.UI.OpenUIForm(FormId.MenuForm);        
    }    

    /// <summary>
    /// 加载游戏配置数据表
    /// </summary>
    /// <param name="dataTableName"></param>
    public static void LoadDataTable(object dataTableName) 
    {
        var dataTable = (string)dataTableName;        
        var dataTableAssetName = AssetUtility.GetDataTableAsset(dataTable, false);        
        GameEntry.DataTable.LoadDataTable(dataTable, dataTableAssetName,null);
    }
    
}

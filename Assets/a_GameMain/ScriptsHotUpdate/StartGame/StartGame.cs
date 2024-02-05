using AOT;
using HotUpdate.UI;
using HotUpdate.DataTables;
using UnityGameFramework.Runtime;

public static class StartGame
{
    /// <summary>
    /// ����Ϸ���� or �˵�����
    /// </summary>
    public static void OpenMenuForm() 
    {
        GameEntry.UI.InitGroups();        
        GameEntry.UI.OpenUIForm(FormId.MenuForm);        
    }    

    /// <summary>
    /// ������Ϸ�������ݱ�
    /// </summary>
    /// <param name="dataTableName"></param>
    public static void LoadDataTable(object dataTableName) 
    {
        var dataTable = (string)dataTableName;        
        var dataTableAssetName = AssetUtility.GetDataTableAsset(dataTable, false);        
        GameEntry.DataTable.LoadDataTable(dataTable, dataTableAssetName,null);
    }
    
}

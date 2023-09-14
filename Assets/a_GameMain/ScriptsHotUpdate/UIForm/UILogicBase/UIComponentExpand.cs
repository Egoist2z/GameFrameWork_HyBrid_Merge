using UnityGameFramework.Runtime;
using HotUpdate.DataTables;
using AOT;

namespace HotUpdate.UI
{    

    public static class UIComponentExpand
    {
        public static void OpenUIForm(this UIComponent component,FormId formId,object userData=null)
        {            
            var uiFormData = GameEntry.DataTable.GetDataTable<DRUIFormBaseInfo>().GetDataRow((int)formId);
            var formName = AssetUtility.GetUIFormAsset(uiFormData.AssetName);            
            if (component.IsLoadingUIForm(formName))
            {
                return;
            }
            if (component.HasUIForm(formName))
            {
                return;
            }
            component.OpenUIForm(formName,uiFormData.UIGroupName);
        }


    }
}
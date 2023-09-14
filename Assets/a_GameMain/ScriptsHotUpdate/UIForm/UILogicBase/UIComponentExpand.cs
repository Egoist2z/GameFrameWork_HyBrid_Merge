using UnityGameFramework.Runtime;
using HotUpdate.DataTables;
using AOT;

namespace HotUpdate.UI
{    

    public static class UIComponentExpand
    {
        public static void OpenForm(this UIComponent component,FormId formId,object userData)
        {
            var uiFormData = GameEntry.DataTable.GetDataTable<DRUIFormBaseInfo>().GetDataRow((int)formId);
            var formName = AssetUtility.GetUIFormAsset(uiFormData.AssetName);
            if (component.HasUIForm(formName))
            {

            }
        }


    }
}
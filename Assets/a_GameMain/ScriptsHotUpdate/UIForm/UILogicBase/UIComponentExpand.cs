using UnityGameFramework.Runtime;
using HotUpdate.DataTables;
using AOT;
using GameFramework.UI;
using System;

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

        public static void InitGroups(this UIComponent component)
        {
            string name = null;
            int dp = 0;
            foreach (var item in Enum.GetValues(typeof(FormGroups)))
            {
                name = item.ToString();
                if (component.HasUIGroup(name))
                {
                    continue;
                }
                dp = (int)item;
                component.AddUIGroup(name,dp);
            }            
        }

    }
}
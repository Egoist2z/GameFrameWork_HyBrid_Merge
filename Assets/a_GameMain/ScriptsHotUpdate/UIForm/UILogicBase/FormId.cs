using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdate.UI
{
    public enum FormId
    {
        Null = 0,

        MenuForm = 1,

        SelectLevelForm = 2,
    }

    public enum FormGroups
    {        
        /// <summary>
        /// 菜单
        /// </summary>
        Meun=0,
        /// <summary>
        /// 主界面
        /// </summary>
        Main=1,
        /// <summary>
        /// 全屏界面
        /// </summary>
        Full=2,
        /// <summary>
        /// 弹出界面
        /// </summary>
        Popup=3,
        /// <summary>
        /// 提示
        /// </summary>
        Prompter=4,
        /// <summary>
        /// 引导
        /// </summary>
        Guidance =5,
    }

}
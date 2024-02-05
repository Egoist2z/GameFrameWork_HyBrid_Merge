﻿//------------------------------------------------------------
// 此文件由 ComponentCollection 自动生成，请勿直接修改。
// 生成时间：2023-09-15 14:22:46.99
//------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using GameExtension;

namespace HotUpdate.UI
{
    public partial class MenuForm 
    {
        private Text m_TitleText;
        private Button m_StartButton;
        private Button m_SettingsButton;
        private Button m_QuitButton;

        /// <summary>
        /// 初始化组件。
        /// </summary>
        public void InitComponents(GameObject target)
        {
            var collection = target.GetComponent<ComponentCollection>();
            m_TitleText = collection.GetComponent<Text>(0);
            m_StartButton = collection.GetComponent<Button>(1);
            m_SettingsButton = collection.GetComponent<Button>(2);
            m_QuitButton = collection.GetComponent<Button>(3);
        }
    }
}

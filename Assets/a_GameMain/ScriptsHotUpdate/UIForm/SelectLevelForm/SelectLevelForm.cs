using System;
using System.Collections.Generic;
using UnityEngine;
using GameExtension;
using UnityGameFramework;

namespace HotUpdate.UI
{
    public partial class SelectLevelForm : UGUIFormLogic
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            InitComponents(gameObject);
            AddComponentEvent();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        private void AddComponentEvent() 
        {
            m_CloseButton.onClick.AddListener(Close);
        }
    }
}

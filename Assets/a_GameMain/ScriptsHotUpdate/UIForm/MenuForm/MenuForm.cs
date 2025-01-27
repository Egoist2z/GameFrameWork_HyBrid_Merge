using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework;

namespace HotUpdate.UI
{
    public partial class MenuForm : UGUIFormLogic
    {       
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            InitComponents(gameObject);
            AddConponentEvent();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        public void AddConponentEvent() 
        {
            m_StartButton.onClick.AddListener(()=> {
                m_TitleText.text = "Start";
            });
        }
    }
}
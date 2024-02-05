using UnityGameFramework.Runtime;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

namespace HotUpdate.UI
{

    /// <summary>
    /// UGUI使用逻辑类
    /// </summary>
    public class UGUIFormLogic : UIFormLogic
    {
        public const int DepthFactor = 10;

        private CanvasGroup m_CanvasGroup = null;
        private Canvas m_Canvas = null;//当前面板自身的Canvas
        private List<Canvas> m_ChildrenCanvas = new List<Canvas>();//获取子物体上可能存在的Canvas

        public int OriginalDepth //面板初始层级
        {
            get;
            private set;
        }

        public int Depth //面板当前层级
        {
            get
            {
                return m_Canvas.sortingOrder;
            }
        }
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            gameObject.name = gameObject.name.Replace("(Clone)", "");
            m_Canvas = gameObject.GetOrAddComponent<Canvas>();
            m_CanvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();//Canvas Group可以用来控制一组不需要个别控制的UI元素的某些方面，CanvasGroup的属性会影响他所有children的GameObject
            gameObject.GetOrAddComponent<GraphicRaycaster>();//Graphic Raycaster组件一般是和Canvas挂载在同一个物体下面 管理他下面的所有子UI物体的点击响应方式 
            m_Canvas.overrideSorting = true;
            RectTransform transform = GetComponent<RectTransform>();
            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;
            transform.anchoredPosition = Vector2.zero;
            transform.sizeDelta = Vector2.zero;
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnCover()
        {
            base.OnCover();
        }

        protected override void OnRefocus(object userData)
        {
            base.OnRefocus(userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            int oldDepth = Depth;
            base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
            int deltaDepth = UGUIGroupHelper.DepthFactor * uiGroupDepth + DepthFactor * depthInUIGroup - oldDepth + OriginalDepth;
            GetComponentsInChildren(true, m_ChildrenCanvas);
            for (int i = 0; i < m_ChildrenCanvas.Count; i++)
            {
              m_ChildrenCanvas[i].sortingOrder += deltaDepth;
            }
            m_ChildrenCanvas.Clear();
        }

        protected void Close() 
        {
            GameEntry.UI.CloseUIForm(UIForm);
        }
    }
}
﻿using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// ui界面组层级辅助器
    /// </summary>
    public class UGUIGroupHelper : UIGroupHelperBase
    {
        public const int DepthFactor = 30;

        private int m_Depth = 0;
        private Canvas m_CachedCanvas = null;

        /// <summary>
        /// 设置界面组深度。
        /// </summary>
        /// <param name="depth">界面组深度。</param>
        public override void SetDepth(int depth)
        {
            m_Depth = depth;
            m_CachedCanvas.overrideSorting = true;//overrideSorting:覆盖画布的排序
            m_CachedCanvas.sortingOrder = DepthFactor * depth;//sortingOrder:画布在排序层中的顺序
        }

        private void Awake()
        {
            m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
            gameObject.GetOrAddComponent<GraphicRaycaster>();
        }

        private void Start()
        {
            m_CachedCanvas.overrideSorting = true;
            m_CachedCanvas.sortingOrder = DepthFactor * m_Depth;

            RectTransform transform = GetComponent<RectTransform>();
            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;
            transform.anchoredPosition = Vector2.zero;
            transform.sizeDelta = Vector2.zero;
        }
    }
}
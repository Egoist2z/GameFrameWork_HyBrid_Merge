//------------------------------------------------------------
// 此文件由 ComponentCollection 自动生成，请勿直接修改。
// 生成时间：2023-11-16 14:25:45.34
//------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using GameExtension;

namespace HotUpdate.UI
{
    public partial class SelectLevelForm 
    {
        private Button m_CloseButton;

        /// <summary>
        /// 初始化组件。
        /// </summary>
        public void InitComponents(GameObject target)
        {
            var collection = target.GetComponent<ComponentCollection>();
            m_CloseButton = collection.GetComponent<Button>(0);
        }
    }
}

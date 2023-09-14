//------------------------------------------------------------
// 此文件由 ComponentCollection 自动生成，请勿直接修改。
// 生成时间：2023-09-13 15:25:31.78
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

        /// <summary>
        /// 初始化组件。
        /// </summary>
        public void InitComponents(GameObject target)
        {
            var collection = target.GetComponent<ComponentCollection>();
            m_TitleText = collection.GetComponent<Text>(0);
            m_StartButton = collection.GetComponent<Button>(1);
        }
    }
}

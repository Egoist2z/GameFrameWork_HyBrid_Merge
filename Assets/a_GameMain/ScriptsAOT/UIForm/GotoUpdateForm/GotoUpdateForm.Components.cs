//------------------------------------------------------------
// 此文件由 ComponentCollection 自动生成，请勿直接修改。
// 生成时间：2023-09-12 14:51:59.64
//------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using GameExtension;

namespace AOT_UIForm
{
    public partial class GotoUpdateForm 
    {
        private Text m_TitleText;
        private Text m_ContentText;
        private Button m_QuitButton;
        private Button m_GotoButton;

        /// <summary>
        /// 初始化组件。
        /// </summary>
        public void InitComponents(GameObject target)
        {
            var collection = target.GetComponent<ComponentCollection>();            
            m_TitleText = collection.GetComponent<Text>(0);
            m_ContentText = collection.GetComponent<Text>(1);
            m_QuitButton = collection.GetComponent<Button>(2);
            m_GotoButton = collection.GetComponent<Button>(3);
        }
    }
}

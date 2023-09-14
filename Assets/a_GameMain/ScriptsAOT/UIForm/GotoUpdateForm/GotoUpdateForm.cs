using System;
using System.Collections.Generic;
using UnityEngine;
using GameExtension;
using UnityEngine.Events;

namespace AOT_UIForm
{
    public struct GotoUpdateFormData
    {
        public string title;
        public string content;
        public UnityAction quit;
        public UnityAction update;

    }
    public partial class GotoUpdateForm : MonoBehaviour
    {        
        public void InitForm(GotoUpdateFormData data) 
        {
            InitComponents(gameObject);
            m_TitleText.text = data.title;
            m_ContentText.text = data.content;
            m_QuitButton.onClick.AddListener(data.quit);
            m_GotoButton.onClick.AddListener(data.update);
        }
    }
}

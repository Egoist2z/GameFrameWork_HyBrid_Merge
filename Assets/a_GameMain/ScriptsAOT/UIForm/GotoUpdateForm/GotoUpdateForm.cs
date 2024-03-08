using System;
using System.Collections.Generic;
using UnityEngine;
using GameExtension;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AOT_UIForm
{
    public struct GotoUpdateFormData
    {
        public string title;
        public string content;
        public string affirm;
        public string cancel;
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
            m_QuitButton.transform.Find("icon").GetComponent<Text>().text = data.cancel;
            m_GotoButton.transform.Find("icon").GetComponent<Text>().text = data.affirm;
            m_QuitButton.onClick.AddListener(data.quit);
            m_GotoButton.onClick.AddListener(data.update);
        }
    }
}

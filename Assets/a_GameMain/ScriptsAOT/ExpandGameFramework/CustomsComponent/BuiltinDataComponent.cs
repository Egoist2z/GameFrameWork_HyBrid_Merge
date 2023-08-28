//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

    public class BuiltinDataComponent : GameFrameworkComponent
    {
        [SerializeField]
        private TextAsset m_BuildInfoTextAsset = null;

        [SerializeField]
        private RectTransform m_lodingRoot;
        
        private LodingForm m_lodingForm=null;

        //[SerializeField]
        //private TextAsset m_DefaultDictionaryTextAsset = null;

        //[SerializeField]
        //private Loding m_UpdateResourceFormTemplate = null;

        private BuildInfo m_BuildInfo = null;

        public BuildInfo BuildInfo
        {
            get
            {
                return m_BuildInfo;
            }
        }

        public LodingForm LodingFormTemplate
        {
            get
            {
                return m_lodingForm;
            }
        }

        public void InitBuildInfo()
        {
            if (m_BuildInfoTextAsset == null || string.IsNullOrEmpty(m_BuildInfoTextAsset.text))
            {
                Log.Info("Build info can not be found or empty.");
                return;
            }            
            m_BuildInfo = Utility.Json.ToObject<BuildInfo>(m_BuildInfoTextAsset.text);            
            if (m_BuildInfo == null)
            {
                Log.Warning("Parse build info failure.");
                return;
            }
        }

        public void InitLodingForm() 
        {
            if (m_lodingForm==null)
            {
                GameObject form = (GameObject)Resources.Load("LodingForm");
                if (form==null)
                {
                    Debug.LogError("LodingForm Lost");
                    return;
                }
                m_lodingForm= Instantiate(form).GetComponent<LodingForm>();
                m_lodingForm.transform.SetParent(m_lodingRoot);
                RectTransform rect = (RectTransform)m_lodingForm.transform;
                rect.localPosition = Vector3.zero;
                rect.sizeDelta = Vector2.zero;
                m_lodingForm.ResetForm();
            }            
        }
        
        public void UnLoadLodingForm() 
        {
            DestroyImmediate(m_lodingForm.gameObject);
            m_lodingForm = null;
        }


        //public void InitDefaultDictionary()
        //{
        //    if (m_DefaultDictionaryTextAsset == null || string.IsNullOrEmpty(m_DefaultDictionaryTextAsset.text))
        //    {
        //        Log.Info("Default dictionary can not be found or empty.");
        //        return;
        //    }

        //    if (!GameEntry.Localization.ParseData(m_DefaultDictionaryTextAsset.text))
        //    {
        //        Log.Warning("Parse default dictionary failure.");
        //        return;
        //    }
        //}
    }

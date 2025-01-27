﻿using UnityEngine;
using UnityEditor;

namespace GameExtension.Editor
{
    public class GUIStyleViewer : EditorWindow
    {
        private Vector2 m_ScrollPosition = Vector2.zero;
        private string m_SearchText = string.Empty;

        [MenuItem("Game Framework/Custom Tools/GUIStyle Viewer {内置预设GUI样式}")]
        public static void ShowWindow()
        {
            var window = GetWindow<GUIStyleViewer>("GUIStyle Viewer", true);
            window.minSize = new Vector2(600, 800);
            window.Show();
        }

        void OnGUI()
        {
            m_SearchText = EditorGUILayout.TextField("", m_SearchText, "SearchTextField");
            EditorGUILayout.HelpBox("Click item right or choose item left to copy to clipboard.", MessageType.Info);

            m_ScrollPosition = GUILayout.BeginScrollView(m_ScrollPosition, "HelpBox");
            {
                foreach (GUIStyle style in GUI.skin)
                {
                    if (style.name.ToLower().Contains(m_SearchText.ToLower()))
                    {
                        GUILayout.BeginHorizontal("Box");
                        {
                            EditorGUILayout.SelectableLabel("\"" + style.name + "\"");
                            GUILayout.FlexibleSpace();
                            if (GUILayout.Button(style.name, style))
                            {
                                EditorGUIUtility.systemCopyBuffer = "\"" + style.name + "\"";
                            }
                            GUILayout.Space(10);
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.EndScrollView();
        }
    }
}

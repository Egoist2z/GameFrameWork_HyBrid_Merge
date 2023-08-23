using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace GameExtension.Editor
{
    public class GameObjectClone : EditorWindow
    {
        private readonly List<FieldInfo> m_CacheFieldInfo = new List<FieldInfo>();

        private GameObject m_Template;
        private GameObject m_Target;

        [MenuItem("Game Framework/Custom Tools/Template To Target(复制预制体组件数据，到目标预制体)", false)]
        private static void ShowWindow()
        {
            var window = GetWindow<GameObjectClone>("Replace PrefabData", true);
            window.minSize = new Vector2(400, 200);
            window.Show();
        }

        private void OnGUI()
        {
            GUIClonePrefab();
        }

        private void GUIClonePrefab()
        {
            EditorGUILayout.BeginVertical();
            {
                m_Template = (GameObject) EditorGUILayout.ObjectField("Template(改动预制体)", m_Template, typeof(GameObject), true);
                m_Target = (GameObject) EditorGUILayout.ObjectField("Target(目标预制体)", m_Target, typeof(GameObject), true);

                GUILayout.Space(10);
                              
                if (GUILayout.Button("Replace(替换组件数据)"))
                {
                    string prefabPath = UnityEditor.PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(m_Target);
                    if (!string.IsNullOrEmpty(prefabPath))
                    {
                        var newPrefab = Clone();
                        if (newPrefab != null)
                        {
                            SaveNewPrefab(newPrefab, prefabPath);
                        }
                        DestroyImmediate(newPrefab);
                    }
                }             
            }
            EditorGUILayout.EndVertical();
        }

        private GameObject Clone()
        {
            var templateTransform = m_Target.transform;
            var targetTransform = Instantiate(m_Template.gameObject, m_Template.transform.parent).transform;
            CloneComponentAndStripChildren(templateTransform, targetTransform);
            return targetTransform.gameObject;
        }

        private void CloneComponentAndStripChildren(Transform templateTransform, Transform targetTransform)
        {
            // if (!targetTransform.name.StartsWith(templateTransform.name))
            // {
            //     Destroy(targetTransform.gameObject);
            //     return;
            // }

            // CloneComponents(templateTransform, targetTransform);

            // 递归处理子物体

            var templateChildren = templateTransform.childCount;
            var targetChildren = targetTransform.childCount;

            if (targetChildren < templateChildren)
            {
                Debug.LogErrorFormat("The number of  this {0}'s children is less than the template's.", targetTransform.name);
            }

            for (int i = targetChildren - 1; i >= templateChildren; i--) // 去除 相对于模板的 多余的子物体
            {
                DestroyImmediate(targetTransform.GetChild(i).gameObject);
            }

            int number = Mathf.Min(templateChildren, targetChildren);
            for (int i = number - 1; i >= 0; i--)
            {
                CloneComponentAndStripChildren(templateTransform.GetChild(i), targetTransform.GetChild(i));
            }
        }

        private void CloneComponents(Transform templateTransform, Transform targetTransform)
        {
            var templateComponents = templateTransform.GetComponents<Component>();
            for (int i = 0; i < templateComponents.Length; i++)
            {
                Component templateComponent = templateComponents[i];
                Type componentType = templateComponent.GetType();
                Component targetComponent = targetTransform.GetComponent(componentType);
                if (targetComponent == null)
                {
                    targetComponent = targetTransform.gameObject.AddComponent(componentType);
                }

                CloneComponent(templateComponent, targetComponent);
            }
        }

        private void CloneComponent(Component templateComponent, Component targetComponent)
        {
            if (templateComponent == null || targetComponent == null)
            {
                return;
            }

            CacheValueOfReference(targetComponent);
            UnityEditorInternal.ComponentUtility.CopyComponent(templateComponent);
            UnityEditorInternal.ComponentUtility.PasteComponentValues(targetComponent);
            ApplyValueOfReference(targetComponent);
        }

        private void CacheValueOfReference(Component targetComponent)
        {
            FieldInfo[] fieldInfos = targetComponent.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                if (fieldInfo.FieldType.IsValueType || fieldInfo.FieldType.Name.ToLower().Equals("string"))
                {
                    continue;
                }
                m_CacheFieldInfo.Add(fieldInfo);
            }
        }

        private void ApplyValueOfReference(Component targetComponent)
        {
            foreach (FieldInfo info in m_CacheFieldInfo)
            {
                FieldInfo fieldInfo = targetComponent.GetType().GetField(info.Name);
                if (fieldInfo == null)
                {
                    Debug.LogErrorFormat("Get Field info failed '{0}.{1}'.", targetComponent, info.Name);
                    continue;
                }
                fieldInfo.SetValue(targetComponent, info.GetValue(targetComponent));
            }

            m_CacheFieldInfo.Clear();
        }

        private void SaveNewPrefab(GameObject newPrefab, string prefabPath)
        {
            UnityEditor.PrefabUtility.SaveAsPrefabAssetAndConnect(newPrefab, prefabPath, InteractionMode.AutomatedAction, out bool saveResult);
            if (saveResult)
            {
                Debug.LogFormat("Prefab apply succeed: {0}.", prefabPath);
            }
            else
            {
                Debug.LogErrorFormat("Prefab apply failed: {0}.", prefabPath);
            }
        }
    }
}

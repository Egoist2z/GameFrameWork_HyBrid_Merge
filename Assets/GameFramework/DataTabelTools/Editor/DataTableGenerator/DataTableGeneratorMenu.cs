//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEditor;
using UnityEngine;

namespace DataTableTools
{
    public sealed class DataTableGeneratorMenu
    {
        /// <summary>
        /// 导出C#类模板配置文件
        /// </summary>
        private const string CSharpCodeTemplateFileName = "Assets/GameFramework/Configs/DataTableCodeTemplate.txt";
        /// <summary>
        /// 表格资源路径
        /// </summary>
        private const string GodGeneral_DataTablePath = "Assets/a_GameMain/GameAssets/DataTabels";
        /// <summary>
        /// 导出C#类路径
        /// </summary>
        private const string GodGeneral_CSharpCodePath = "Assets/a_GameMain/ScriptsAOT/DataTabels";      
        
        [MenuItem("Game Framework/DataTablesTools/导出本地配置表")]
        private static void GenerateDataTables()
        {            
            foreach (string dataTableName in ProcedurePreload.DataTableNames)
            {
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName, GodGeneral_DataTablePath);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName, GodGeneral_DataTablePath);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName,CSharpCodeTemplateFileName,GodGeneral_CSharpCodePath);
            }

            AssetDatabase.Refresh();
        }
    }
}

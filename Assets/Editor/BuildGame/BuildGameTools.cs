using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

public static class BuildGameTools
{
    [MenuItem("Game Framework/BuidleGame/BuidleGameFor_Android_Package(单机包)", false)]
    public static void BuidleGameFor_Android_Package() 
    {        
#if UNITY_ANDROID
        //CreateHotUpdateDlls();
        Create_Android_Ab();        
#else
        Debug.LogError("先切换到安卓环境");  
#endif
    }

    private static void CreateHotUpdateDlls()
    {        
        HybridCLR.Editor.Commands.PrebuildCommand.GenerateAll();        
        string hpDllPath = Application.dataPath + "/HybridCLRData/HotUpdateDlls/Android/HotUpdate.dll";
        string destPath = Application.dataPath + "/a_GameMain/GameAssets/HotUpdate_dll/HotUpdate.dll.bytes";
        if (File.Exists(destPath))
        {
            File.Delete(destPath);
        }        
        File.Move(hpDllPath.Replace("/Assets",""), destPath);
        AssetDatabase.Refresh();
        Debug.LogError("CreateDlls");
    }

    private static void Create_Android_Ab() 
    {
        var rec = GameObject.Find("Resource").GetComponent<UnityGameFramework.Runtime.ResourceComponent>();
        rec.ResourceMode = GameFramework.Resource.ResourceMode.Package;
        EditorSceneManager.SaveOpenScenes();
        var builderWindow = UnityGameFramework.Editor.ResourceTools.ResourceBuilder.Open();
        //builderWindow.PackageBuidel(PackageOverCallBack);
        //DelectDirFile(() => { builderWindow.OrderBuildResources = true; });
        //Debug.LogError("CreateAb");
    }

    private static void PackageOverCallBack(UnityGameFramework.Editor.ResourceTools.Platform obj)
    {        
        string path = Application.streamingAssetsPath + "/Package";        
        DirectoryInfo dir = new DirectoryInfo(path);
        FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();        
        
        dir = new DirectoryInfo(fileinfo[0].FullName+ "/Android");
        fileinfo = dir.GetFileSystemInfos();
        
        foreach (var item in fileinfo)
        {
            if (item.Name== "GameAssets")
            {
                Directory.Move(item.FullName, Application.streamingAssetsPath + "/" + item.Name);
            }
            if (item.Name == "GameFrameworkVersion.dat")
            {
                File.Move(item.FullName, Application.streamingAssetsPath + "/" + item.Name);
            }
        }
        AssetDatabase.Refresh();

        
        dir = new DirectoryInfo(Application.streamingAssetsPath);
        var dirs = dir.GetDirectories();

        foreach (var item in dirs)
        {            
            if (item.Name!= "GameAssets")
            {
                item.Delete(true);
                File.Delete(item.FullName+".meta");
            }
        }      
        AssetDatabase.Refresh();

        Debug.LogError("CallBack___DeleteFile");
        //BuildPlayerOptions opt = new BuildPlayerOptions();
        //opt.scenes = new string[] { "Assets/a_GameMain/GameAssets/Scenes/Start_Game.unity" };
        //opt.locationPathName = Application.dataPath + "/AoutApk/Game.apk".Replace("/Assets", "");
        //opt.target = BuildTarget.Android;
        //opt.options = BuildOptions.None;
        //BuildPipeline.BuildPlayer(opt);
    }
    

    private static void DelectDirFile(Action action)
    {
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
        foreach (FileSystemInfo i in fileinfo)
        {
            if (i is DirectoryInfo)
            {
                DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                subdir.Delete(true);         
            }
            else
            {
                File.Delete(i.FullName);
            }
        }
        AssetDatabase.Refresh();
        action.Invoke();
    }


    //[MenuItem("Game Framework/BuidleGame/BuidleGameFor_Android_Updatable(热更包)", false)]
    //public static void BuidleGameFor_Android_Updatable()
    //{
    //    var builderWindow = UnityGameFramework.Editor.ResourceTools.ResourceBuilder.Open();



    //}
}

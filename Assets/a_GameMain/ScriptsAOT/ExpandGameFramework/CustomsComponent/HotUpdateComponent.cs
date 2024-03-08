using UnityGameFramework.Runtime;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using GameFramework.Resource;

public class HotUpdateComponent : GameFrameworkComponent
{
    public Assembly hotAssembly;

    private bool loadHotAssemblySuccess;
    public bool LoadHotAssemblySuccess
    {
        get
        {
            return loadHotAssemblySuccess;
        }
    }

    private LoadAssetCallbacks loadAssetCallbacks;

    public const string HotPath = "Assets/a_GameMain/GameAssets/HotUpdate_dll/HotUpdate.dll.bytes";

    private void Start()
    {
        hotAssembly = null;
        loadHotAssemblySuccess = false;
        loadAssetCallbacks = null;
    }

    public void InitHotAssembly()
    {
        if (loadAssetCallbacks == null)
        {
            loadAssetCallbacks = new LoadAssetCallbacks(LoadHotAssemblySuccessCallback,LoadHotAssemblyFailureCallback);
        }
        if (GameEntry.Base.EditorResourceMode)
        {
            hotAssembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
            loadHotAssemblySuccess = true;
        }
        else
        {
            GameEntry.Resource.LoadAsset(HotPath, loadAssetCallbacks);
        }      
    }

    /// <summary>
    /// 调用hotupdateDLL
    /// </summary>
    /// <param name="type">类名</param>
    /// <param name="method">方法名</param>
    /// <param name="parameter">参数列表</param>
    public void RunMethod(string type, string method, object[] parameter = null)
    {
        Type tp = hotAssembly.GetType(type);        
        tp.GetMethod(method).Invoke(tp, parameter);
    }
    
    public void LoadHotAssemblySuccessCallback(string assetName, object asset, float duration, object userData)
    {        
        TextAsset textAsset = (TextAsset) asset;
        hotAssembly = Assembly.Load(textAsset.bytes);
        loadHotAssemblySuccess = true;
        Log.Debug("LoadHotAssembly {0} Success", assetName);
    }

    public void LoadHotAssemblyFailureCallback(string assetName, LoadResourceStatus status, string errorMessage, object userData)
    {
        loadHotAssemblySuccess = false;
        Log.Fatal("Load HotAssembly: {0} Failure error:{1}",assetName,errorMessage);
    }
}

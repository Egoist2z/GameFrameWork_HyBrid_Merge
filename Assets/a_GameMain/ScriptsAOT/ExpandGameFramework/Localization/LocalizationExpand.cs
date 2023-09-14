using UnityEngine;
using UnityGameFramework.Runtime;


public static class LocalizationExpand 
{
    public static string GetStringOrNull(this LocalizationComponent component,string key) 
    {
        if (!component.HasRawString(key))
        {
            Log.Error("LocalizationDicKey Is Null");
            return "";
        }
        return component.GetString(key);
    }

}

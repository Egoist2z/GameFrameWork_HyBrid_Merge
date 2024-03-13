using UnityEngine;
using UnityGameFramework.Runtime;


public static class LocalizationExpand 
{
    public static string GetStringOrNull(this LocalizationComponent component,string key) 
    {
        Log.Debug(key);
        if (!component.HasRawString(key))
        {
            Log.Error("LocalizationDicKey Is Null");
            return "";
        }
        return component.GetString(key);
    }

    //public static Font GetLocalizationFont(this LocalizationComponent component)
    //{        
    //    return new Font();
    //}

}

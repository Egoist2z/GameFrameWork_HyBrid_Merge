using GameFramework.Localization;
using System;
using UnityGameFramework.Runtime; 



public static class SettingExpand
{
    public static void SetGameLanguage(this SettingComponent component,Language language)
    {
        component.SetString(SettingTag.Language,language.ToString());
        component.Save();
    }

    public static Language GetGameLanguage(this SettingComponent component)
    {
        if (component.HasSetting(SettingTag.Language))
        {
            var value= component.GetString(SettingTag.Language);
            Language language=(Language)Enum.Parse(typeof(Language), value);
            return language;
        }
        return Language.Unspecified;
    }

    public static void SetAssetsLanguage(this SettingComponent component,string currentVariant) 
    {
        if (component.HasSetting(SettingTag.AssetsLanguage)&& component.GetString(SettingTag.AssetsLanguage)==currentVariant)
        {
            return;
        }
        component.SetString(SettingTag.AssetsLanguage,currentVariant);
        component.Save();
    }

    public static string GetAssetsLanguage(this SettingComponent component)
    {
        return component.GetString(SettingTag.AssetsLanguage);
    }

}
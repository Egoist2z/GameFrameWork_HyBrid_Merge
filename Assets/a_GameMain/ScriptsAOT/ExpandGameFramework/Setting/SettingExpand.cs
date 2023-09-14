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

}
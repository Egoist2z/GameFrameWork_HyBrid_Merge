//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

/// <summary>
/// 热更配置信息
/// </summary>
public class BuildInfo
{
    /// <summary>
    /// 游戏版本
    /// </summary>
    public string GameVersion;

    /// <summary>
    /// 内部版本
    /// </summary>
    public int InternalGameVersion;

    /// <summary>
    /// 检测资源文件地址
    /// </summary>
    public string CheckVersionUrl;

    /// <summary>
    /// 强制更新地址
    /// </summary>
    public string WindowsAppUrl;

    public string MacOSAppUrl;

    public string IOSAppUrl;

    public string AndroidAppUrl;
}

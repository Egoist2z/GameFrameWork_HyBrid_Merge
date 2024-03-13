//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Localization
{
    /// <summary>
    /// 本地化语言。
    /// </summary>
    public enum Language : byte
    {
        /// <summary>
        /// 未指定。
        /// </summary>
        Unspecified = 0,          
        /// <summary>
        /// 简体中文。
        /// </summary>
        ChineseSimplified,

        /// <summary>
        /// 繁体中文。
        /// </summary>
        ChineseTraditional,

        /// <summary>
        /// 英语。
        /// </summary>
        English,  
    }
}

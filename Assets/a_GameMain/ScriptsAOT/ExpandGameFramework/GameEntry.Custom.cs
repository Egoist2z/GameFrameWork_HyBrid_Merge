using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class GameEntry : MonoBehaviour
{
    public static BuiltinDataComponent BuiltinData
    {
        get;
        private set;
    }

    public static HotUpdateComponent HotUpdate
    {
        get;
        private set;
    }

    private static void InitCustomComponents()
    {
        BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
        HotUpdate = UnityGameFramework.Runtime.GameEntry.GetComponent<HotUpdateComponent>();
    }
}

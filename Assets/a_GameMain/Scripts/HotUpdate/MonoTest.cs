using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdate
{
    public class MonoTest : MonoBehaviour
    {
        private void Start()
        {
            Debug.LogError("StartMono");
        }


        public void CallMono() 
        {
            Debug.LogError("CallMono");
        }
    }
}
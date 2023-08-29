public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	// UnityEngine.CoreModule.dll
	// UnityGameFramework.Runtime.dll
	// mscorlib.dll
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// System.Collections.Generic.List<object>
	// }}

	public void RefMethods()
	{
		// object UnityEngine.Component.GetComponent<object>()
		// System.Void UnityEngine.Component.GetComponentsInChildren<object>(bool,System.Collections.Generic.List<object>)
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityExtension.GetOrAddComponent<object>(UnityEngine.GameObject)
	}
}
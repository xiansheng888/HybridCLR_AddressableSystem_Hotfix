using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// System.Action<float>
	// System.Collections.Generic.ArraySortHelper<float>
	// System.Collections.Generic.Comparer<float>
	// System.Collections.Generic.ICollection<float>
	// System.Collections.Generic.IComparer<float>
	// System.Collections.Generic.IEnumerable<float>
	// System.Collections.Generic.IEnumerator<float>
	// System.Collections.Generic.IList<float>
	// System.Collections.Generic.List.Enumerator<float>
	// System.Collections.Generic.List<float>
	// System.Collections.Generic.ObjectComparer<float>
	// System.Collections.ObjectModel.ReadOnlyCollection<float>
	// System.Comparison<float>
	// System.Predicate<float>
	// }}

	public void RefMethods()
	{
	}
}
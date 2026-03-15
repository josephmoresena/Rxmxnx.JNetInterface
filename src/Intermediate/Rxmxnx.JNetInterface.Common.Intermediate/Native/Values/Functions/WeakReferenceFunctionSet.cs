namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java object weak global references through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct WeakReferenceFunctionSet
{
	/// <summary>
	/// Pointer to <c>NewWeakGlobalRef</c> function.
	/// Creates a new weak global reference.
	/// </summary>
	public readonly NewRefFunction<JWeakRef> NewWeakGlobalRef;
	/// <summary>
	/// Pointer to <c>DeleteWeakGlobalRef</c> function.
	/// Delete the <c>VM</c> resources needed for the given weak global reference.
	/// </summary>
	public readonly DeleteRefFunction<JWeakRef> DeleteWeakGlobalRef;
}
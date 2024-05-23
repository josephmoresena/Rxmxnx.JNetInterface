namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java instance methods through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct InstanceMethodFunctionSet
{
	/// <summary>
	/// Pointers to <c>GetMethodID</c> and <c>Call&lt;type&gt;Method</c> functions.
	/// </summary>
	public readonly MethodFunctionSet<JObjectLocalRef> MethodFunctions;
	/// <summary>
	/// Pointers to <c>CallNonvirtual&lt;type&gt;Method</c> functions.
	/// </summary>
	public readonly NonVirtualMethodFunctionSet NonVirtualFunctions;
}
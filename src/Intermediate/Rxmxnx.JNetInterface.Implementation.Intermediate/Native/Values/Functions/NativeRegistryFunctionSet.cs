namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to register/unregister native methods in Java classes through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct NativeRegistryFunctionSet
{
	/// <summary>
	/// Pointer to <c>RegisterNatives</c> function.
	/// Registers native methods with the specified class.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JClassLocalRef, ReadOnlyValPtr<NativeMethodValue>, Int32,
		JResult> RegisterNatives;
	/// <summary>
	/// Pointer to <c>UnregisterNatives</c> function.
	/// Unregisters native methods of a class.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JResult> UnregisterNatives;
}
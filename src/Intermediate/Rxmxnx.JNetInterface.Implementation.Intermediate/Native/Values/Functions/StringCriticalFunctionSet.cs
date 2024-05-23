namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate critical memory in Java strings through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct StringCriticalFunctionSet
{
	/// <summary>
	/// Pointer to <c>GetStringCritical</c> function.
	/// Returns a pointer to the array of characters of the string.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JStringLocalRef, out JBoolean, ReadOnlyValPtr<Char>>
		GetStringCritical;
	/// <summary>
	/// Pointer to <c>ReleasePrimitiveArrayCritical</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to chars.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JStringLocalRef, ReadOnlyValPtr<Char>, void>
		ReleaseStringCritical;
}
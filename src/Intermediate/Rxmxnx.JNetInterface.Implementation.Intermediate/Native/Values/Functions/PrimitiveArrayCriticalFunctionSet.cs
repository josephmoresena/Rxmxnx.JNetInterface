namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate critical memory in Java arrays through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct PrimitiveArrayCriticalFunctionSet
{
	/// <summary>
	/// Pointer to <c>GetPrimitiveArrayCritical</c> function.
	/// Returns the body of the primitive array.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JArrayLocalRef, out JBoolean, ValPtr<Byte>>
		GetPrimitiveArrayCritical;
	/// <summary>
	/// Pointer to <c>ReleasePrimitiveArrayCritical</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JArrayLocalRef, ValPtr<Byte>, JReleaseMode, void>
		ReleasePrimitiveArrayCritical;
}
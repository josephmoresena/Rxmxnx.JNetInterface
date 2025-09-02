namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate critical memory in Java strings through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct StringCriticalFunctionSet
{
	/// <summary>
	/// Pointer to <c>GetStringCritical</c> function.
	/// Returns a pointer to the array of characters of the string.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Byte*, Char*> _getStringCritical;
	/// <summary>
	/// Pointer to <c>ReleasePrimitiveArrayCritical</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to chars.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Char*, void> _releaseStringCritical;

	/// <summary>
	/// <c>GetStringCritical</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ReadOnlyValPtr<Char> GetStringCritical(JEnvironmentRef envRef, JStringLocalRef stringRef,
		out JBoolean isCopy)
	{
		fixed (void* isCopyPtr = &isCopy)
			return this._getStringCritical(envRef.Pointer, stringRef.Pointer, (Byte*)isCopyPtr);
	}
	/// <summary>
	/// <c>ReleaseStringCritical</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ReleaseStringCritical(JEnvironmentRef envRef, JStringLocalRef stringRef, ReadOnlyValPtr<Char> charsPtr)
		=> this._releaseStringCritical(envRef.Pointer, stringRef.Pointer, charsPtr);
}
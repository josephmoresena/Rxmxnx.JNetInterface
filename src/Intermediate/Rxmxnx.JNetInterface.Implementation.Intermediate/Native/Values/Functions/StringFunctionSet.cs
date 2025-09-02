namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java string through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct StringFunctionSet
{
	/// <summary>
	/// Pointer to <c>NewString</c> function.
	/// Constructs a new <c>java.lang.String</c> object from an array of characters.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, Char*, Int32, IntPtr> _newString;
	/// <summary>
	/// Pointers to <c>GetStringLength</c>, <c>GetStringChars</c> and <c>ReleaseStringChars</c>
	/// functions.
	/// </summary>
	public readonly StringFunctionSet<Char> Utf16;
	/// <summary>
	/// Pointer to <c>NewStringUTF</c> function.
	/// Constructs a new <c>java.lang.String</c> object from an array of characters.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, Byte*, IntPtr> _newStringUtf;
	/// <summary>
	/// Pointers to <c>GetStringUTFLength</c>, <c>GetStringUTFChars</c> and <c>ReleaseStringUTFChars</c>
	/// functions.
	/// </summary>
	public readonly StringFunctionSet<Byte> Utf8;

	/// <summary>
	/// <c>NewStringUtf</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JStringLocalRef NewString(JEnvironmentRef envRef, Char* textPtr, Int32 textLength)
		=> new(this._newString(envRef.Pointer, textPtr, textLength));
	/// <summary>
	/// <c>NewStringUtf</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JStringLocalRef NewStringUtf(JEnvironmentRef envRef, Byte* textPtr)
		=> new(this._newStringUtf(envRef.Pointer, textPtr));
}

/// <summary>
/// Set of function pointers to manipulate Java string through JNI.
/// </summary>
/// <typeparam name="TChar">Type of character.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct StringFunctionSet<TChar>
	where TChar : unmanaged, IBinaryNumber<TChar>, IUnsignedNumber<TChar>
{
	/// <summary>
	/// Pointer to <c>GetStringLength</c> function.
	/// Returns the length (the count of characters) of a Java string.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Int32> _getLengthPtr;
	/// <summary>
	/// Pointer to <c>GetStringChars</c> function.
	/// Returns a pointer to the array of characters of the string.
	/// </summary>
	/// <remarks>This pointer is valid until <c>ReleaseStringChars()</c> is called.</remarks>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Byte*, void*> _getCharsPtr;
	/// <summary>
	/// Pointer to <c>ReleaseStringChars</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to chars.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, void*, void> _releaseChars;

	/// <summary>
	/// Pointer to <c>GetStringLength</c> function.
	/// Returns the length (the count of characters) of a Java string.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 GetStringLength(JEnvironmentRef envRef, JStringLocalRef stringRef)
		=> this._getLengthPtr(envRef.Pointer, stringRef.Pointer);
	/// <summary>
	/// Pointer to <c>GetStringChars</c> function.
	/// Returns a pointer to the array of characters of the string.
	/// </summary>
	/// <remarks>This pointer is valid until <c>ReleaseStringChars()</c> is called.</remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ReadOnlyValPtr<TChar> GetStringChars(JEnvironmentRef envRef, JStringLocalRef stringRef, out JBoolean isCopy)
	{
		fixed (void* isCopyPtr = &isCopy)
			return (ReadOnlyValPtr<TChar>)this._getCharsPtr(envRef.Pointer, stringRef.Pointer, (Byte*)isCopyPtr);
	}
	/// <summary>
	/// Pointer to <c>ReleaseStringChars</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to chars.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ReleaseStringChars(JEnvironmentRef envRef, JStringLocalRef stringRef, ReadOnlyValPtr<TChar> chars)
		=> this._releaseChars(envRef.Pointer, stringRef.Pointer, chars);
}
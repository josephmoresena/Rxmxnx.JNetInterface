namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java string through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe partial struct StringFunctionSet
{
	/// <summary>
	/// Pointer to <c>NewString</c> function.
	/// Constructs a new <c>java.lang.String</c> object from an array of characters.
	/// </summary>
	private readonly NewStringPtr _newString;
	/// <summary>
	/// Pointers to <c>GetStringLength</c>, <c>GetStringChars</c> and <c>ReleaseStringChars</c>
	/// functions.
	/// </summary>
	public readonly StringFunctionSet<Char> Utf16;
	/// <summary>
	/// Pointer to <c>NewStringUTF</c> function.
	/// Constructs a new <c>java.lang.String</c> object from an array of characters.
	/// </summary>
	private readonly NewStringUtfPtr _newStringUtf;
	/// <summary>
	/// Pointers to <c>GetStringUTFLength</c>, <c>GetStringUTFChars</c> and <c>ReleaseStringUTFChars</c>
	/// functions.
	/// </summary>
	public readonly StringFunctionSet<Byte> Utf8;

	/// <summary>
	/// <c>NewString</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JStringLocalRef NewString(JEnvironmentRef envRef, Char* textPtr, Int32 textLength)
		=> SystemInfo.IsWindows ?
			this._newString.Windows(envRef, textPtr, textLength) :
			this._newString.Unix(envRef, textPtr, textLength);
	/// <summary>
	/// <c>NewString</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JStringLocalRef NewStringUtf(JEnvironmentRef envRef, Byte* textPtr)
		=> SystemInfo.IsWindows ?
			this._newStringUtf.Windows(envRef, textPtr) :
			this._newStringUtf.Unix(envRef, textPtr);
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
internal readonly unsafe struct StringFunctionSet<TChar> : IStringFunctionSet
	where TChar : unmanaged, IBinaryNumber<TChar>, IUnsignedNumber<TChar>
{
	/// <summary>
	/// Pointer to <c>GetStringLength</c>, <c>GetStringChars</c> and <c>ReleaseStringChars</c> functions.
	/// </summary>
	private readonly IStringFunctionSet.StringFunctionSet _functions;

	/// <summary>
	/// <c>GetStringLength</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int32 GetStringLength(JEnvironmentRef envRef, JStringLocalRef stringRef)
		=> SystemInfo.IsWindows ?
			this._functions.Windows.GetLength(envRef, stringRef) :
			this._functions.Unix.GetLength(envRef, stringRef);
	/// <summary>
	/// <c>GetStringChars</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ReadOnlyValPtr<TChar> GetStringChars(JEnvironmentRef envRef, JStringLocalRef stringRef, out JBoolean isCopy)
	{
		fixed (JBoolean* isCopyPtr = &isCopy)
		{
			return (ReadOnlyValPtr<TChar>)(SystemInfo.IsWindows ?
				this._functions.Windows.GetChars(envRef, stringRef, isCopyPtr) :
				this._functions.Unix.GetChars(envRef, stringRef, isCopyPtr));
		}
	}
	/// <summary>
	/// <c>ReleaseStringChars</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ReleaseStringChars(JEnvironmentRef envRef, JStringLocalRef stringRef, ReadOnlyValPtr<TChar> chars)
	{
		if (SystemInfo.IsWindows)
			this._functions.Windows.ReleaseChars(envRef, stringRef, chars);
		else
			this._functions.Unix.ReleaseChars(envRef, stringRef, chars);
	}
}
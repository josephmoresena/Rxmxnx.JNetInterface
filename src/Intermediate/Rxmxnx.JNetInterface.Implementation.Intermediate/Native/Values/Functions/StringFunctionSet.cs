namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java string through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct StringFunctionSet
{
	/// <summary>
	/// Pointer to <c>NewString</c> function.
	/// Constructs a new <c>java.lang.String</c> object from an array of characters.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, ReadOnlyValPtr<Char>, Int32, JStringLocalRef> NewString;
	/// <summary>
	/// Pointers to <c>GetStringLength</c>, <c>GetStringChars</c> and <c>ReleaseStringChars</c>
	/// functions.
	/// </summary>
	public readonly StringFunctionSet<Char> Utf16;
	/// <summary>
	/// Pointer to <c>NewStringUTF</c> function.
	/// Constructs a new <c>java.lang.String</c> object from an array of characters.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, ReadOnlyValPtr<Byte>, JStringLocalRef> NewStringUtf;
	/// <summary>
	/// Pointers to <c>GetStringUTFLength</c>, <c>GetStringUTFChars</c> and <c>ReleaseStringUTFChars</c>
	/// functions.
	/// </summary>
	public readonly StringFunctionSet<Byte> Utf8;
}

/// <summary>
/// Set of function pointers to manipulate Java string through JNI.
/// </summary>
/// <typeparam name="TChar">Type of character.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct StringFunctionSet<TChar>
	where TChar : unmanaged, IBinaryNumber<TChar>, IUnsignedNumber<TChar>
{
	/// <summary>
	/// Pointer to <c>GetStringLength</c> function.
	/// Returns the length (the count of characters) of a Java string.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JStringLocalRef, Int32> GetStringLength;
	/// <summary>
	/// Pointer to <c>GetStringChars</c> function.
	/// Returns a pointer to the array of characters of the string.
	/// </summary>
	/// <remarks>This pointer is valid until <c>ReleaseStringChars()</c> is called.</remarks>
	public readonly delegate* unmanaged<JEnvironmentRef, JStringLocalRef, out JBoolean, ReadOnlyValPtr<TChar>>
		GetStringChars;
	/// <summary>
	/// Pointer to <c>ReleaseStringChars</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to chars.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JStringLocalRef, ReadOnlyValPtr<TChar>, void>
		ReleaseStringChars;
}
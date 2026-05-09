namespace Rxmxnx.JNetInterface.Native.Values.Interfaces;

/// <summary>
/// Set of function pointers to manipulate Java string through JNI.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal unsafe interface IStringFunctionSet
{
	/// <summary>
	/// Pointer to <c>GetStringLength</c>, <c>GetStringChars</c> and <c>ReleaseStringChars</c> functions.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	protected readonly struct StringFunctionSet
	{
#if !ANDROID
		/// <summary>
		/// Function pointer set for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly Windows Windows;
#endif
		/// <summary>
		/// Function pointer set for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly Unix Unix;
	}

#if !ANDROID
	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	protected readonly struct Windows
	{
		/// <summary>
		/// Pointer to <c>GetStringLength</c> function.
		/// Returns the length (the count of characters) of a Java string.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JStringLocalRef, Int32> GetLength;
		/// <summary>
		/// Pointer to <c>GetStringChars</c> function.
		/// Returns a pointer to the array of characters of the string.
		/// </summary>
		/// <remarks>This pointer is valid until <c>ReleaseStringChars()</c> is called.</remarks>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JStringLocalRef, JBoolean*, void*> GetChars;
		/// <summary>
		/// Pointer to <c>ReleaseStringChars</c> function.
		/// Informs the <c>VM</c> that the native code no longer needs access to chars.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JStringLocalRef, void*, void> ReleaseChars;
	}
#endif

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	protected readonly struct Unix
	{
		/// <summary>
		/// Pointer to <c>GetStringLength</c> function.
		/// Returns the length (the count of characters) of a Java string.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JStringLocalRef, Int32> GetLength;
		/// <summary>
		/// Pointer to <c>GetStringChars</c> function.
		/// Returns a pointer to the array of characters of the string.
		/// </summary>
		/// <remarks>This pointer is valid until <c>ReleaseStringChars()</c> is called.</remarks>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JStringLocalRef, JBoolean*, void*> GetChars;
		/// <summary>
		/// Pointer to <c>ReleaseStringChars</c> function.
		/// Informs the <c>VM</c> that the native code no longer needs access to chars.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JStringLocalRef, void*, void> ReleaseChars;
	}
}
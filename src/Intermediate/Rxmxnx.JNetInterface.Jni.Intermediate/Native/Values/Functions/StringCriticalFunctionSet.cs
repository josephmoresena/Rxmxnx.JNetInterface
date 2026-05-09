// ReSharper disable ConvertIfStatementToReturnStatement

namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate critical memory in Java strings through JNI.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct StringCriticalFunctionSet
{
#if !ANDROID
	/// <summary>
	/// Function set for Windows Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Windows _windows;
#endif
	/// <summary>
	/// Function set for Unix-like Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Unix _unix;

	/// <summary>
	/// <c>GetStringCritical</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ReadOnlyValPtr<Char> GetStringCritical(JEnvironmentRef envRef, JStringLocalRef stringRef,
		out JBoolean isCopy)
	{
		fixed (JBoolean* isCopyPtr = &isCopy)
		{
#if !ANDROID
			if (SystemInfo.IsWindows)
				return this._windows.GetStringCritical(envRef, stringRef, isCopyPtr);
#endif
			return this._unix.GetStringCritical(envRef, stringRef, isCopyPtr);
		}
	}
	/// <summary>
	/// <c>ReleasePrimitiveArrayCritical</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ReleaseStringCritical(JEnvironmentRef envRef, JStringLocalRef stringRef, ReadOnlyValPtr<Char> chars)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
		{
			this._windows.ReleaseStringCritical(envRef, stringRef, chars);
			return;
		}
#endif
		this._unix.ReleaseStringCritical(envRef, stringRef, chars);
	}

#if !ANDROID
	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Pointer to <c>GetStringCritical</c> function.
		/// Returns a pointer to the array of characters of the string.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JStringLocalRef, JBoolean*, Char*>
			GetStringCritical;
		/// <summary>
		/// Pointer to <c>ReleasePrimitiveArrayCritical</c> function.
		/// Informs the <c>VM</c> that the native code no longer needs access to chars.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JStringLocalRef, Char*, void>
			ReleaseStringCritical;
	}
#endif

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Pointer to <c>GetStringCritical</c> function.
		/// Returns a pointer to the array of characters of the string.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JStringLocalRef, JBoolean*, Char*>
			GetStringCritical;
		/// <summary>
		/// Pointer to <c>ReleasePrimitiveArrayCritical</c> function.
		/// Informs the <c>VM</c> that the native code no longer needs access to chars.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JStringLocalRef, Char*, void> ReleaseStringCritical;
	}
}
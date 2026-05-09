// ReSharper disable ConvertIfStatementToReturnStatement

namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to register/unregister native methods in Java classes through JNI.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NativeRegistryFunctionSet
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
	/// <c>RegisterNatives</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult RegisterNatives(JEnvironmentRef envRef, JClassLocalRef classRef, NativeMethodValue* methodsPtr,
		Int32 methodsCount)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._windows.RegisterNatives(envRef, classRef, methodsPtr, methodsCount);
#endif
		return this._unix.RegisterNatives(envRef, classRef, methodsPtr, methodsCount);
	}
	/// <summary>
	/// <c>UnregisterNatives</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult UnregisterNatives(JEnvironmentRef envRef, JClassLocalRef classRef)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._windows.UnregisterNatives(envRef, classRef);
#endif
		return this._unix.UnregisterNatives(envRef, classRef);
	}

#if !ANDROID
	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Pointer to <c>RegisterNatives</c> function.
		/// Registers native methods with the specified class.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JClassLocalRef, NativeMethodValue*, Int32, JResult
			> RegisterNatives;
		/// <summary>
		/// Pointer to <c>UnregisterNatives</c> function.
		/// Unregisters native methods of a class.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JClassLocalRef, JResult> UnregisterNatives;
	}
#endif

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Pointer to <c>RegisterNatives</c> function.
		/// Registers native methods with the specified class.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JClassLocalRef, NativeMethodValue*, Int32, JResult >
			RegisterNatives;
		/// <summary>
		/// Pointer to <c>UnregisterNatives</c> function.
		/// Unregisters native methods of a class.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JClassLocalRef, JResult> UnregisterNatives;
	}
}
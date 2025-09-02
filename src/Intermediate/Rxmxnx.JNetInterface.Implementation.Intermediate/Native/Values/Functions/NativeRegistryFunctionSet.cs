namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to register/unregister native methods in Java classes through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NativeRegistryFunctionSet
{
	/// <summary>
	/// Pointer to <c>RegisterNatives</c> function.
	/// Registers native methods with the specified class.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, void*, Int32, Int32> _registerNatives;
	/// <summary>
	/// Pointer to <c>UnregisterNatives</c> function.
	/// Unregisters native methods of a class.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Int32> _unregisterNatives;

	/// <summary>
	/// <c>RegisterNatives</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult RegisterNatives(JEnvironmentRef envRef, JClassLocalRef classRef, NativeMethodValue* method0Ptr,
		Int32 methodCount)
		=> (JResult)this._registerNatives(envRef.Pointer, classRef.Pointer, method0Ptr, methodCount);
	/// <summary>
	/// <c>UnregisterNatives</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult UnregisterNatives(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> (JResult)this._unregisterNatives(envRef.Pointer, classRef.Pointer);
}
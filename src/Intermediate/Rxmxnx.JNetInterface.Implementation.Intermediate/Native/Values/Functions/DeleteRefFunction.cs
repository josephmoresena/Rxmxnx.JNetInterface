namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to delete object reference through JNI.
/// </summary>
/// <typeparam name="TReference">Type of object reference.</typeparam>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct DeleteRefFunction<TReference>
	where TReference : unmanaged, INativeType, IWrapper<JObjectLocalRef>
{
	/// <summary>
	/// Pointer to <c>Delete<typeparamref name="TReference"/>Ref</c> function.
	/// </summary>
	[FieldOffset(0)]
	private readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, void> _windows;
	/// <summary>
	/// Pointer to <c>Delete<typeparamref name="TReference"/>Ref</c> function.
	/// </summary>
	[FieldOffset(0)]
	private readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, void> _unix;

	/// <summary>
	/// Pointer to <c>Delete<typeparamref name="TReference"/>Ref</c> function.
	/// Deletes the given <typeparamref name="TReference"/> reference.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void DeleteRef(JEnvironmentRef envRef, TReference objRef)
	{
		if (OperatingSystem.IsWindows())
			this._windows(envRef, objRef.Value);
		else
			this._unix(envRef, objRef.Value);
	}
}
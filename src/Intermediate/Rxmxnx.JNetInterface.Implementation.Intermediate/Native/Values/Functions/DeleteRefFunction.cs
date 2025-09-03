namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to delete object reference through JNI.
/// </summary>
/// <typeparam name="TReference">Type of object reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct DeleteRefFunction<TReference>
	where TReference : unmanaged, INativeType, IWrapper<JObjectLocalRef>
{
	/// <summary>
	/// Pointer to <c>Delete<typeparamref name="TReference"/>Ref</c> function.
	/// Deletes the given <typeparamref name="TReference"/> reference.
	/// </summary>
	private readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, void> _ptr;

	/// <summary>
	/// Pointer to <c>Delete<typeparamref name="TReference"/>Ref</c> function.
	/// Deletes the given <typeparamref name="TReference"/> reference.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void DeleteRef(JEnvironmentRef envRef, TReference objRef) => this._ptr(envRef, objRef.Value);
}
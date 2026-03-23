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
internal readonly unsafe struct DeleteRefFunction<TReference> : IDeleteRefFunction
	where TReference : unmanaged, INativeReferenceType, INativePointerType<TReference>
{
	/// <summary>
	/// Pointer to <c>Delete<typeparamref name="TReference"/>Ref</c> function.
	/// </summary>
	private readonly IDeleteRefFunction.DeleteRefFunction _function;

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
		if (SystemInfo.IsWindows)
			this._function.Windows(envRef, objRef.Pointer);
		else
			this._function.Unix(envRef, objRef.Pointer);
	}
}
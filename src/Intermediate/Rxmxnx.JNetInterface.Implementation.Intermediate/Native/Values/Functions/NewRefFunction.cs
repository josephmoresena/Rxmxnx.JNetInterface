namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to create new object reference through JNI.
/// </summary>
/// <typeparam name="TReference">Type of object reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct NewRefFunction<TReference>
	where TReference : unmanaged, INativeType, IWrapper<JObjectLocalRef>
{
	/// <summary>
	/// Pointer to <c>New<typeparamref name="TReference"/>Ref</c> function.
	/// Creates a new global reference to the object referred.
	/// </summary>
	/// <remarks>
	/// Created references must be explicitly disposed of by calling
	/// <c>Delete<typeparamref name="TReference"/>Ref()</c>.
	/// </remarks>
	private readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, IntPtr> _ptr;

	/// <summary>
	/// Pointer to <c>New<typeparamref name="TReference"/>Ref</c> function.
	/// Creates a new global reference to the object referred.
	/// </summary>
	/// <remarks>
	/// Created references must be explicitly disposed of by calling
	/// <c>Delete<typeparamref name="TReference"/>Ref()</c>.
	/// </remarks>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TReference NewRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		TReference result = default;
		Unsafe.As<TReference, IntPtr>(ref result) = this._ptr(envRef, localRef);
		return result;
	}
}
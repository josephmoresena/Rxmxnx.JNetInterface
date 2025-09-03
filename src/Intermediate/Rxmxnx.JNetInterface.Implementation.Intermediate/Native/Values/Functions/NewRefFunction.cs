namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to create new object reference through JNI.
/// </summary>
/// <typeparam name="TReference">Type of object reference.</typeparam>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NewRefFunction<TReference>
	where TReference : unmanaged, INativeType, IWrapper<JObjectLocalRef>
{
	/// <summary>
	/// Pointer to <c>New<typeparamref name="TReference"/>Ref</c> function.
	/// </summary>
	[FieldOffset(0)]
	private readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, IntPtr> _windows;
	/// <summary>
	/// Pointer to <c>New<typeparamref name="TReference"/>Ref</c> function.
	/// </summary>
	[FieldOffset(0)]
	private readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, IntPtr> _unix;

	/// <summary>
	/// Creates a new <typeparamref name="TReference"/> reference to the object referred.
	/// </summary>
	/// <remarks>
	/// Created references must be explicitly disposed of by calling
	/// <c>Delete<typeparamref name="TReference"/>Ref()</c>.
	/// </remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TReference NewRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		TReference result = default;
		Unsafe.As<TReference, IntPtr>(ref result) = OperatingSystem.IsWindows() ?
			this._windows(envRef, localRef) :
			this._unix(envRef, localRef);
		return result;
	}
}
namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to create new object reference through JNI.
/// </summary>
/// <typeparam name="TReference">Type of object reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct NewRefFunction<TReference>
	where TReference : unmanaged, INativeType<TReference>, IWrapper<JObjectLocalRef>
{
	/// <summary>
	/// Pointer to <c>New<typeparamref name="TReference"/>Ref</c> function.
	/// Creates a new global reference to the object referred.
	/// </summary>
	/// <remarks>
	/// Created references must be explicitly disposed of by calling
	/// <c>Delete<typeparamref name="TReference"/>Ref()</c>.
	/// </remarks>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, TReference> NewRef;
}
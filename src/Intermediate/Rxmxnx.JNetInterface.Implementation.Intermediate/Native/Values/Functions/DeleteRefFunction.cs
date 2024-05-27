namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to delete object reference through JNI.
/// </summary>
/// <typeparam name="TReference">Type of object reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct DeleteRefFunction<TReference>
	where TReference : unmanaged, INativeType<TReference>, IWrapper<JObjectLocalRef>
{
	/// <summary>
	/// Pointer to <c>Delete<typeparamref name="TReference"/>Ref</c> function.
	/// Deletes the given <typeparamref name="TReference"/> reference.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, TReference, void> DeleteRef;
}
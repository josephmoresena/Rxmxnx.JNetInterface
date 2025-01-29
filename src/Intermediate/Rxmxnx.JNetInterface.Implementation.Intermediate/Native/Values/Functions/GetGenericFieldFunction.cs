namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to Get the value of Java fields through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver field.</typeparam>
/// <typeparam name="TField">Type of field.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct GetGenericFieldFunction<TReceiver, TField> where TReceiver : unmanaged, IWrapper
	where TField : unmanaged, INativeType
{
	/// <summary>
	/// Pointer to <c>Get&lt;type&gt;Field</c> function.
	/// </summary>
	public readonly delegate* managed<JEnvironmentRef, TReceiver, JFieldId, TField> _managedGet;
	/// <summary>
	/// Pointer to <c>Get&lt;type&gt;Field</c> function.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, TReceiver, JFieldId, TField> _unmanagedGet;

	/// <summary>
	/// Calls to <c>Get&lt;type&gt;Field</c> function.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TField Get(JEnvironmentRef envRef, TReceiver receiver, JFieldId fieldId)
		=> MethodOffset.UseManagedGenericPointers ?
			this._managedGet(envRef, receiver, fieldId) :
			this._unmanagedGet(envRef, receiver, fieldId);
}
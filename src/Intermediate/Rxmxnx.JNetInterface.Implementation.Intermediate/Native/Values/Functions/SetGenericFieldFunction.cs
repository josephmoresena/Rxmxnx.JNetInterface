namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to Set the value of Java fields through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver field.</typeparam>
/// <typeparam name="TField">Type of field.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct SetGenericFieldFunction<TReceiver, TField>
	where TReceiver : unmanaged, IWrapper<JObjectLocalRef> where TField : unmanaged, INativeType
{
	/// <summary>
	/// Pointer to <c>Set&lt;type&gt;Field</c> function.
	/// </summary>
	private readonly void* _ptr;

	/// <summary>
	/// Calls <c>Set&lt;type&gt;Field</c> function.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Set(JEnvironmentRef envRef, TReceiver receiver, JFieldId fieldId, TField value)
	{
		if (MethodOffset.UseManagedGenericPointers)
			((delegate* managed<JEnvironmentRef, TReceiver, JFieldId, TField, void>)this._ptr)(
				envRef, receiver, fieldId, value);
		else
			((delegate* unmanaged<JEnvironmentRef, TReceiver, JFieldId, TField, void>)this._ptr)(
				envRef, receiver, fieldId, value);
	}
}
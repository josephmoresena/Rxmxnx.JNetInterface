namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to Get the value of Java fields through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver field.</typeparam>
/// <typeparam name="TField">Type of field.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct GetGenericFieldFunction<TReceiver, TField>
	where TReceiver : unmanaged, IWrapper<JObjectLocalRef> where TField : unmanaged, INativeType
{
	/// <summary>
	/// Pointer to <c>Get&lt;type&gt;Field</c> function.
	/// </summary>
	public readonly void* _ptr;

	/// <summary>
	/// Calls to <c>Get&lt;type&gt;Field</c> function.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TField Get(JEnvironmentRef envRef, TReceiver receiver, JFieldId fieldId)
	{
		if (GenericFunctionCallHelper.UseManagedGenericPointers)
			return ((delegate* managed<JEnvironmentRef, TReceiver, JFieldId, TField>)this._ptr)(
				envRef, receiver, fieldId);
#if !NET8_0
			return ((delegate* unmanaged<JEnvironmentRef, TReceiver, JFieldId, TField>)this._ptr)(
				envRef, receiver, fieldId);
#else
		TField result = default;
		GenericFunctionCallHelper.GetField(this._ptr, TField.Type, envRef, receiver.Value.Pointer, fieldId,
		                                   ref Unsafe.As<TField, Byte>(ref result));
		return result;
#endif
	}
}
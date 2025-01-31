namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java functions through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver function.</typeparam>
/// <typeparam name="TResult">Type of return function.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct CallGenericFunction<TReceiver, TResult>
	where TReceiver : unmanaged, IWrapper<JObjectLocalRef> where TResult : unmanaged, INativeType
{
	/// <summary>
	/// Internal reserved entries.
	/// </summary>
	private readonly MethodOffset _offset;
	/// <summary>
	/// Managed caller <c>A</c> function.
	/// </summary>
	private readonly void* _ptr;

	/// <summary>
	/// Calls <c>A</c> function.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Call(JEnvironmentRef envRef, TReceiver receiver, JMethodId methodId, JValue* args)
	{
		if (MethodOffset.UseManagedGenericPointers)
			return ((delegate* managed<JEnvironmentRef, TReceiver, JMethodId, JValue*, TResult>)this._ptr)(
				envRef, receiver, methodId, args);
#if !NET8_0
			return ((delegate* unmanaged<JEnvironmentRef, TReceiver, JMethodId, JValue*, TResult>)this._ptr)(
				envRef, receiver, methodId, args);
#else
		TResult result = default;
		NonGenericFunctionHelper.CallMethod(this._ptr, envRef, receiver.Value.Pointer, methodId, args, sizeof(TResult),
		                                    Unsafe.AsPointer(ref result));
		return result;
#endif
	}
}
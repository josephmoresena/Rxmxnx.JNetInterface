namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java functions through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver function.</typeparam>
/// <typeparam name="TResult">Type of return function.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
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
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Call(JEnvironmentRef envRef, TReceiver receiver, JMethodId methodId, JValue* args)
	{
		TResult result = default;
		GenericFunctionCallHelper.CallMethod(this._ptr, TResult.Type, envRef, receiver.Value.Pointer, methodId, args,
		                                     ref Unsafe.As<TResult, Byte>(ref result));
		return result;
	}
}
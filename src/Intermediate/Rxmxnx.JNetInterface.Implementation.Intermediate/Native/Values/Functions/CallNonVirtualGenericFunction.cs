namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java non-virtual functions through JNI.
/// </summary>
/// <typeparam name="TResult">Type of return function.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct CallNonVirtualGenericFunction<TResult> where TResult : unmanaged, INativeType
{
	/// <summary>
	/// Internal reserved entries.
	/// </summary>
	private readonly MethodOffset _offset;
	/// <summary>
	/// Caller function pointers.
	/// </summary>
	private readonly void* _ptr;

	/// <summary>
	/// Calls <c>A</c> function.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Call(JEnvironmentRef envRef, JObjectLocalRef localRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
		if (MethodOffset.UseManagedGenericPointers)
			return ((delegate* managed<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, TResult>)
				this._ptr)(envRef, localRef, classRef, methodId, args);
		try
		{
			return ((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, TResult>)
				this._ptr)(envRef, localRef, classRef, methodId, args);
		}
		catch (Exception)
		{
#if !NET8_0
			throw;
#else
			TResult result = default;
			NonGenericFunctionHelper.CallNonVirtualMethod(this._ptr, envRef, localRef, classRef, methodId, args,
			                                              sizeof(TResult), Unsafe.AsPointer(ref result));
			return result;
#endif
		}
	}
}
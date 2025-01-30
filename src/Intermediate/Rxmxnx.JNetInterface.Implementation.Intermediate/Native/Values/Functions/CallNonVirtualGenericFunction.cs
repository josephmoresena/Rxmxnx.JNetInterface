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
	/// <remarks>Should it really be declared as managed?</remarks>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Call(JEnvironmentRef envRef, JObjectLocalRef localRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
		=> MethodOffset.UseManagedGenericPointers ?
			((delegate* managed<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, TResult>)this
				._ptr)(envRef, localRef, classRef, methodId, args) :
			((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, TResult>)
				this._ptr)(envRef, localRef, classRef, methodId, args);
}
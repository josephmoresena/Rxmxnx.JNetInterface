namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java methods through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver method.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct CallMethodFunction<TReceiver>
	where TReceiver : unmanaged, INativeType, IWrapper<JObjectLocalRef>
{
	/// <summary>
	/// Internal reserved entries.
	/// </summary>
#pragma warning disable CS0169
	private readonly MethodOffset _offset;
#pragma warning restore CS0169
	/// <summary>
	/// Caller <c>A</c> function.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, IntPtr, void*, void> _call;

	/// <summary>
	/// <c>CallVoidMethodA</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Call(JEnvironmentRef envRef, TReceiver receiverRef, JMethodId methodId, JValue* args)
		=> this._call(envRef.Pointer, receiverRef.Value.Pointer, methodId.Pointer, args);
}
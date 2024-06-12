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
#pragma warning disable CS0169
	private readonly MethodOffset _offset;
#pragma warning restore CS0169
	/// <summary>
	/// Caller <c>A</c> function.
	/// </summary>
	/// <remarks>Should it really be declared as managed?</remarks>
	public readonly delegate* managed<JEnvironmentRef, TReceiver, JMethodId, JValue*, TResult> Call;
}
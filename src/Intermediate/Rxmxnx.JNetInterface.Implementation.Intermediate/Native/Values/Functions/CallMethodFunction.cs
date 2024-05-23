namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java methods through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver method.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct CallMethodFunction<TReceiver>
	where TReceiver : unmanaged, INativeType<TReceiver>, IWrapper<JObjectLocalRef>
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
	public readonly delegate* unmanaged<JEnvironmentRef, TReceiver, JMethodId, ReadOnlyValPtr<JValue>, void> Call;
}
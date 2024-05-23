namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to Set the value of Java fields through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver field.</typeparam>
/// <typeparam name="TResult">Type of return field.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct SetGenericFieldFunction<TReceiver, TResult>
	where TReceiver : unmanaged, IWrapper<JObjectLocalRef> where TResult : unmanaged, INativeType<TResult>
{
	/// <summary>
	/// Pointer to <c>Set&lt;type&gt;Field</c> function.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, TReceiver, JFieldId, TResult, void> Set;
}
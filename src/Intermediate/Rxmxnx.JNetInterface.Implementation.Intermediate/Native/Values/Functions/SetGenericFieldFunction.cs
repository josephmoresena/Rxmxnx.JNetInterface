namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to Set the value of Java fields through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver field.</typeparam>
/// <typeparam name="TResult">Type of return field.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct SetGenericFieldFunction<TReceiver, TResult>
	where TReceiver : unmanaged, IWrapper<JObjectLocalRef> where TResult : unmanaged, INativeType
{
	/// <summary>
	/// Pointer to <c>Set&lt;type&gt;Field</c> function.
	/// </summary>
	/// <remarks>Should it really be declared as managed?</remarks>
	public readonly delegate* managed<JEnvironmentRef, TReceiver, JFieldId, TResult, void> Set;
}
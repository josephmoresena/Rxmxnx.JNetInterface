namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to enter/exit monitor of Java objects through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct MonitorFunctionSet
{
	/// <summary>
	/// Pointer to <c>MonitorEnter</c> function.
	/// Enters the monitor associated with the underlying Java object.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JResult> MonitorEnter;
	/// <summary>
	/// Pointer to <c>MonitorExit</c> function.
	/// The thread decrements the counter indicating the number of times it has entered this monitor.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JResult> MonitorExit;
}
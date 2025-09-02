namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to enter/exit monitor of Java objects through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct MonitorFunctionSet
{
	/// <summary>
	/// Pointer to <c>MonitorEnter</c> function.
	/// Enters the monitor associated with the underlying Java object.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Int32> _monitorEnter;
	/// <summary>
	/// Pointer to <c>MonitorExit</c> function.
	/// The thread decrements the counter indicating the number of times it has entered this monitor.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Int32> _monitorExit;

	/// <summary>
	/// <c>MonitorEnter</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult MonitorEnter(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> (JResult)this._monitorEnter(envRef.Pointer, localRef.Pointer);
	/// <summary>
	/// <c>MonitorExit</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult MonitorExit(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> (JResult)this._monitorExit(envRef.Pointer, localRef.Pointer);
}
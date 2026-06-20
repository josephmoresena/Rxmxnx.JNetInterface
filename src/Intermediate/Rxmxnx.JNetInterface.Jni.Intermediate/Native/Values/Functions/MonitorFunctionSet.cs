// ReSharper disable ConvertIfStatementToReturnStatement

namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to enter/exit monitor of Java objects through JNI.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct MonitorFunctionSet
{
#if !ANDROID
	/// <summary>
	/// Function set for Windows Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Windows _windows;
#endif
	/// <summary>
	/// Function set for Unix-like Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Unix _unix;

	/// <summary>
	/// <c>MonitorEnter</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult MonitorEnter(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._windows.MonitorEnter(envRef, localRef);
#endif
		return this._unix.MonitorEnter(envRef, localRef);
	}
	/// <summary>
	/// <c>MonitorExit</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult MonitorExit(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._windows.MonitorExit(envRef, localRef);
#endif
		return this._unix.MonitorExit(envRef, localRef);
	}

#if !ANDROID
	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Pointer to <c>MonitorEnter</c> function.
		/// Enters the monitor associated with the underlying Java object.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JResult> MonitorEnter;
		/// <summary>
		/// Pointer to <c>MonitorExit</c> function.
		/// The thread decrements the counter indicating the number of times it has entered this monitor.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JResult> MonitorExit;
	}
#endif

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Pointer to <c>MonitorEnter</c> function.
		/// Enters the monitor associated with the underlying Java object.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JResult> MonitorEnter;
		/// <summary>
		/// Pointer to <c>MonitorExit</c> function.
		/// The thread decrements the counter indicating the number of times it has entered this monitor.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JResult> MonitorExit;
	}
}
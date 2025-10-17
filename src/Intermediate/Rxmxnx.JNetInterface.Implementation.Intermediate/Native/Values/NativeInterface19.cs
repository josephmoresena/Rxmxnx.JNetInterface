namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNINativeInterface_</c> struct. Contains all pointers to the functions of JNI.
/// </summary>
/// <remarks>JNI 19</remarks>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NativeInterface19 : INativeInterface<NativeInterface19>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => (Int32)JRuntimeVersion.J19;

	/// <summary>
	/// Function set for Windows Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Windows _windows;
	/// <summary>
	/// Function set for Unix-like Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Unix _unix;

	/// <summary>
	/// <c>IsVirtualThread</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JBoolean IsVirtualThread(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> OperatingSystem.IsWindows() ?
			this._windows.IsVirtualThread(envRef, localRef) :
			this._unix.IsVirtualThread(envRef, localRef);

	/// <summary>
	/// Information of <see cref="NativeInterface19.IsVirtualThread"/>
	/// </summary>
	public static readonly JniMethodInfo IsVirtualThreadInfo = new()
	{
		Name = nameof(NativeInterface19.IsVirtualThread), Level = JniSafetyLevels.CriticalSafe,
	};

	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Native interface for <c>JNI_VERSION_9</c>
		/// </summary>
#pragma warning disable CS0169
		private readonly NativeInterface9 _nativeInterface;
#pragma warning restore CS0169

		/// <summary>
		/// Pointer to <c>IsVirtualThread</c> function.
		/// Tests whether an object is a virtual Thread.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JBoolean> IsVirtualThread;
	}

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Native interface for <c>JNI_VERSION_9</c>
		/// </summary>
#pragma warning disable CS0169
		private readonly NativeInterface9 _nativeInterface;
#pragma warning restore CS0169

		/// <summary>
		/// Pointer to <c>IsVirtualThread</c> function.
		/// Tests whether an object is a virtual Thread.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JBoolean> IsVirtualThread;
	}
}
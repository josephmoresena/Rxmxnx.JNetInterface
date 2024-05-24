namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JNativeInterface"/> type.
/// </summary>
/// <remarks></remarks>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct NativeInterface19 : INativeInterface<NativeInterface>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => 0x00130000;

	/// <summary>
	/// Native interface for <c>JNI_VERSION_9</c>
	/// </summary>
	public readonly NativeInterface NativeInterface9;
	/// <summary>
	/// Pointer to <c>IsVirtualThread</c> function.
	/// Tests whether an object is a virtual Thread.
	/// </summary>
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JBoolean> IsVirtualThread;

	/// <summary>
	/// Information of <see cref="NativeInterface19.IsVirtualThread"/>
	/// </summary>
	public static readonly JniMethodInfo IsVirtualThreadInfo = new()
	{
		Name = nameof(NativeInterface19.IsVirtualThread), Level = JniSafetyLevels.CriticalSafe,
	};
}
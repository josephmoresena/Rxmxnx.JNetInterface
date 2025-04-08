namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JNativeInterface"/> type.
/// </summary>
/// <remarks>Thread Operations</remarks>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NativeInterface19 : INativeInterface<NativeInterface19>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => 0x00130000;

	/// <summary>
	/// Native interface for <c>JNI_VERSION_9</c>
	/// </summary>
#pragma warning disable CS0169
	private readonly NativeInterface _nativeInterface9;
#pragma warning restore CS0169

	/// <summary>
	/// Pointer to <c>IsVirtualThread</c> function.
	/// Tests whether an object is a virtual Thread.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JBoolean> IsVirtualThread;

	/// <summary>
	/// Information of <see cref="NativeInterface19.IsVirtualThread"/>
	/// </summary>
	public static readonly JniMethodInfo IsVirtualThreadInfo = new()
	{
		Name = nameof(NativeInterface19.IsVirtualThread), Level = JniSafetyLevels.CriticalSafe,
	};
}
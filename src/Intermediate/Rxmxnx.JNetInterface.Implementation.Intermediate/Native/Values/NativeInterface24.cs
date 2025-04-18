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
internal readonly unsafe struct NativeInterface24 : INativeInterface<NativeInterface24>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => 0x00180000;

	/// <summary>
	/// Native interface for <c>JNI_VERSION_19</c>
	/// </summary>
#pragma warning disable CS0169
	private readonly NativeInterface19 _nativeInterface;
#pragma warning restore CS0169

	/// <summary>
	/// Pointer to <c>GetStringUTFLengthAsLong</c> function.
	/// Retrieves the UTF string length as long value.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JStringLocalRef, Int64> GetStringUtfLongLength;

	/// <summary>
	/// Information of <see cref="GetStringUtfLongLength"/>
	/// </summary>
	public static readonly JniMethodInfo GetStringUtfLongLengthInfo = new()
	{
		Name = nameof(NativeInterface24.GetStringUtfLongLengthInfo), Level = JniSafetyLevels.CriticalSafe,
	};
}
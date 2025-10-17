namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNINativeInterface_</c> struct. Contains all pointers to the functions of JNI.
/// </summary>
/// <remarks>JNI 1.4</remarks>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
#endif
internal readonly struct NativeInterface4 : INativeInterface<NativeInterface4>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => (Int32)JRuntimeVersion.SEd4;

	/// <summary>
	/// Native interface for <c>JNI_VERSION_1_2</c>
	/// </summary>
#pragma warning disable CS0169
	private readonly NativeInterface _nativeInterface;
#pragma warning restore CS0169

	/// <summary>
	/// Pointers to <c>GetObjectRefType</c>, <c>GetDirectBufferAddress</c> and <c>GetDirectBufferCapacity</c>
	/// functions.
	/// </summary>
	public readonly NioFunctionSet NioFunctions;

	/// <summary>
	/// Information of <see cref="NioFunctionSet.NewDirectByteBuffer"/>
	/// </summary>
	public static readonly JniMethodInfo NewDirectByteBufferInfo = new()
	{
		Name = nameof(NioFunctionSet.NewDirectByteBuffer), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NioFunctionSet.GetDirectBufferAddress"/>
	/// </summary>
	public static readonly JniMethodInfo GetDirectBufferAddressInfo = new()
	{
		Name = nameof(NioFunctionSet.GetDirectBufferAddress), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NioFunctionSet.GetDirectBufferCapacity"/>
	/// </summary>
	public static readonly JniMethodInfo GetDirectBufferCapacityInfo = new()
	{
		Name = nameof(NioFunctionSet.GetDirectBufferCapacity), Level = JniSafetyLevels.CriticalSafe,
	};
}
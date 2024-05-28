namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JNativeInterface"/> type.
/// </summary>
/// <remarks>NIO Support</remarks>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct NativeInterface4 : INativeInterface<NativeInterface4>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => 0x00010004;

	/// <summary>
	/// Native interface for <c>JNI_VERSION_1_2</c>
	/// </summary>
	public readonly NativeInterface NativeInterface2;
	/// <summary>
	/// Pointer to <c>NewDirectByteBuffer</c> function.
	/// Allocates and returns a direct <c>java.nio.ByteBuffer</c> referring to the given block of memory.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, IntPtr, Int64, JObjectLocalRef> NewDirectByteBuffer;
	/// <summary>
	/// Pointer to <c>GetDirectBufferAddress</c> function.
	/// Fetches and returns the starting address of the memory region referenced by the given direct <c>java.nio.Buffer</c>.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, IntPtr> GetDirectBufferAddress;
	/// <summary>
	/// Pointer to <c>GetDirectBufferCapacity</c> function.
	/// Fetches and returns the capacity of the memory region referenced by the given direct <c>java.nio.Buffer</c>.
	/// </summary>
	/// <remarks>The capacity is the number of elements that the memory region contains.</remarks>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, Int64> GetDirectBufferCapacity;

	/// <summary>
	/// Information of <see cref="NativeInterface4.NewDirectByteBuffer"/>
	/// </summary>
	public static readonly JniMethodInfo NewDirectByteBufferInfo = new()
	{
		Name = nameof(NativeInterface4.NewDirectByteBuffer), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NativeInterface4.GetDirectBufferAddress"/>
	/// </summary>
	public static readonly JniMethodInfo GetDirectBufferAddressInfo = new()
	{
		Name = nameof(NativeInterface4.GetDirectBufferAddress), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NativeInterface4.GetDirectBufferCapacity"/>
	/// </summary>
	public static readonly JniMethodInfo GetDirectBufferCapacityInfo = new()
	{
		Name = nameof(NativeInterface4.GetDirectBufferCapacity), Level = JniSafetyLevels.CriticalSafe,
	};
}
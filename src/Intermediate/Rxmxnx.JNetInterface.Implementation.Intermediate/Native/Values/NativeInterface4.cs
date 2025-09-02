namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JNativeInterface"/> type.
/// </summary>
/// <remarks>NIO Support</remarks>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NativeInterface4 : INativeInterface<NativeInterface4>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => 0x00010004;

	/// <summary>
	/// Native interface for <c>JNI_VERSION_1_2</c>
	/// </summary>
#pragma warning disable CS0169
	private readonly NativeInterface _nativeInterface;
#pragma warning restore CS0169

	/// <summary>
	/// Pointer to <c>NewDirectByteBuffer</c> function.
	/// Allocates and returns a direct <c>java.nio.ByteBuffer</c> referring to the given block of memory.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Int64, IntPtr> _newDirectByteBuffer;
	/// <summary>
	/// Pointer to <c>GetDirectBufferAddress</c> function.
	/// Fetches and returns the starting address of the memory region referenced by the given direct <c>java.nio.Buffer</c>.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, IntPtr> _getDirectBufferAddress;
	/// <summary>
	/// Pointer to <c>GetDirectBufferCapacity</c> function.
	/// Fetches and returns the capacity of the memory region referenced by the given direct <c>java.nio.Buffer</c>.
	/// </summary>
	/// <remarks>The capacity is the number of elements that the memory region contains.</remarks>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Int64> _getDirectBufferCapacity;

	/// <summary>
	/// <c>NewDirectByteBuffer</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectLocalRef NewDirectByteBuffer(JEnvironmentRef envRef, IntPtr buffPtr, Int64 buffSize)
		=> new(this._newDirectByteBuffer(envRef.Pointer, buffPtr, buffSize));
	/// <summary>
	/// <c>GetDirectBufferAddress</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IntPtr GetDirectBufferAddress(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> this._getDirectBufferAddress(envRef.Pointer, localRef.Pointer);
	/// <summary>
	/// <c>GetDirectBufferCapacity</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int64 GetDirectBufferCapacity(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> this._getDirectBufferCapacity(envRef.Pointer, localRef.Pointer);

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
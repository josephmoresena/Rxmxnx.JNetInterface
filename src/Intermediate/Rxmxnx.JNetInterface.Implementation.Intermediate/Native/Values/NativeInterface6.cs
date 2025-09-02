namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JNativeInterface"/> type.
/// </summary>
/// <remarks>JNI 1.6</remarks>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NativeInterface6 : INativeInterface<NativeInterface6>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => 0x00010006;

	/// <summary>
	/// Native interface for <c>JNI_VERSION_1_4</c>
	/// </summary>
#pragma warning disable CS0169
	private readonly NativeInterface4 _nativeInterface;
#pragma warning restore CS0169

	/// <summary>
	/// Pointer to <c>GetObjectRefType</c> function.
	/// Retrieves the type of given object reference.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Int32> _getObjectRefType;

	/// <summary>
	/// <c>GetObjectRefType</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JReferenceType GetObjectRefType(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> (JReferenceType)this._getObjectRefType(envRef.Pointer, localRef.Pointer);

	/// <summary>
	/// Information of <see cref="NativeInterface6.GetObjectRefType"/>
	/// </summary>
	public static readonly JniMethodInfo GetObjectRefTypeInfo = new()
	{
		Name = nameof(NativeInterface6.GetObjectRefType), Level = JniSafetyLevels.CriticalSafe,
	};
}
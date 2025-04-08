namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JNativeInterface"/> type.
/// </summary>
/// <remarks>Module operations</remarks>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NativeInterface9 : INativeInterface<NativeInterface9>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => 0x00090000;

	/// <summary>
	/// Native interface for <c>JNI_VERSION_1_8</c>
	/// </summary>
#pragma warning disable CS0169
	private readonly NativeInterface _nativeInterface9;
#pragma warning restore CS0169

	/// <summary>
	/// Pointer to <c>GetModule</c> function.
	/// Returns the <c>java.lang.Module</c> object for the module that the class is a member of.
	/// </summary>
	/// <remarks>
	/// If the class is not in a named module then the unnamed module of the class loader for the class is returned.
	/// If the class represents an array type then this function returns the Module object for the element type.
	/// If the class represents a primitive type or <c>void</c>, then the Module object for the <c>java.base</c> module is
	/// returned.
	/// </remarks>
	public readonly delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JObjectLocalRef> GetModule;

	/// <summary>
	/// Information of <see cref="NativeInterface9.GetModule"/>
	/// </summary>
	public static readonly JniMethodInfo GetModuleInfo = new() { Name = nameof(NativeInterface9.GetModule), };
}
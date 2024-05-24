namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JNativeInterface"/> type.
/// </summary>
/// <remarks></remarks>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct NativeInterface9 : INativeInterface<NativeInterface>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => 0x00010006;

	/// <summary>
	/// Native interface for <c>JNI_VERSION_1_8</c>
	/// </summary>
	public readonly NativeInterface NativeInterface6;

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
	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JObjectLocalRef> GetModule;

	/// <summary>
	/// Information of <see cref="NativeInterface9.GetModule"/>
	/// </summary>
	public static readonly JniMethodInfo IsVirtualThreadInfo = new() { Name = nameof(NativeInterface9.GetModule), };
}
namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for class objects (<c>jclass</c>). Represents a native-signed integer
/// which serves as opaque identifier for an class object (<c>java.lang.Class&lt;?&gt;</c>).
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JClassLocalRef : IObjectReferenceType<JClassLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JClass;

	/// <summary>
	/// Internal <see cref="JObjectLocalRef"/> reference.
	/// </summary>
	private readonly JObjectLocalRef _value;

	/// <summary>
	/// JNI local reference.
	/// </summary>
	public JObjectLocalRef Value => this._value;
	/// <inheritdoc/>
	public IntPtr Pointer => this._value.Pointer;

	/// <summary>
	/// Converts a given <see cref="JGlobalRef"/> to <see cref="JClassLocalRef"/> instance.
	/// </summary>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> to convert.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static JClassLocalRef FromReference(in JGlobalRef globalRef)
		=> NativeUtilities.Transform<JGlobalRef, JClassLocalRef>(in globalRef);
	/// <summary>
	/// Converts a given <see cref="JWeakRef"/> to <see cref="JClassLocalRef"/> instance.
	/// </summary>
	/// <param name="weakRef">A <see cref="JWeakRef"/> to convert.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static JClassLocalRef FromReference(in JWeakRef weakRef)
		=> NativeUtilities.Transform<JWeakRef, JClassLocalRef>(in weakRef);
}
namespace Rxmxnx.JNetInterface.Native.Identifiers;

/// <summary>
/// JNI handle for methods (<c>methodID</c>). Represents a native-signed integer which serves
/// as opaque identifier for a declared method in a <c>class</c>.
/// </summary>
/// <remarks>This handle will be valid until the associated <c>class</c> is unloaded.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly unsafe partial struct JMethodId : IAccessibleIdentifierType, INativePointerType<JMethodId>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JMethod;

	/// <summary>
	/// Internal value.
	/// </summary>
	[FieldOffset(0)]
	private readonly void* _pointer;

	/// <inheritdoc/>
	public IntPtr Pointer => (IntPtr)this._pointer;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JMethodId() : this(IntPtr.Zero) { }

	/// <summary>
	/// Private constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JMethodId(IntPtr value) => this._pointer = (void*)value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this.Pointer.GetHashCode();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JMethodId methodId && this.Pointer.Equals(methodId.Pointer);

	static JMethodId INativePointerType<JMethodId>.New(IntPtr value) => new(value);
}
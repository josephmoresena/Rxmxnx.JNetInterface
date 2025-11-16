namespace Rxmxnx.JNetInterface.Native.Identifiers;

/// <summary>
/// JNI handle for fields (<c>fieldID</c>). Represents a native-signed integer which serves
/// as opaque identifier for a declared field in a <c>class</c>.
/// </summary>
/// <remarks>This handle will be valid until the associated <c>class</c> is unloaded.</remarks>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
[StructLayout(LayoutKind.Explicit)]
public readonly unsafe partial struct JFieldId : IAccessibleIdentifierType, INativePointerType<JFieldId>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JField;

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
	public JFieldId() : this(IntPtr.Zero) { }

	/// <summary>
	/// Private constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JFieldId(IntPtr value) => this._pointer = (void*)value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this.Pointer.GetHashCode();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj)
		=> obj is JFieldId fieldId && this.Pointer.Equals(fieldId.Pointer);

	static JFieldId INativePointerType<JFieldId>.New(IntPtr value) => new(value);
}
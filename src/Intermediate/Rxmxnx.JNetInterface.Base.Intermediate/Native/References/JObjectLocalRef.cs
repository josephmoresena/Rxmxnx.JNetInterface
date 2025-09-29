namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for fully-qualified-class objects (<c>jobject</c>).
/// Represents a native-signed integer which serves as opaque identifier for any
/// object.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly unsafe partial struct JObjectLocalRef : INativeReferenceType, INativePointerType<JObjectLocalRef>,
	INativeDataType<JObjectLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JObject;

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
	public JObjectLocalRef() : this(IntPtr.Zero) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal JObjectLocalRef(IntPtr value) => this._pointer = (void*)value;

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this.Pointer.GetHashCode();
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => JObjectLocalRef.Equals(this, obj);

	static JObjectLocalRef INativePointerType<JObjectLocalRef>.New(IntPtr value) => new(value);

	/// <summary>
	/// Indicates whether <paramref name="objRef"/> and a <paramref name="obj"/> are equal.
	/// </summary>
	/// <typeparam name="TObject">The type of object reference.</typeparam>
	/// <param name="objRef">The object reference to compare with <paramref name="obj"/>.</param>
	/// <param name="obj">The object to compare with <paramref name="objRef"/>.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="obj"/> and <paramref name="objRef"/> represent the same value;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static Boolean ObjectEquals<TObject>(in TObject objRef, Object? obj)
		where TObject : unmanaged, IWrapper<JObjectLocalRef>
		=> obj switch
		{
			TObject obj2 => objRef.Value.Equals(obj2.Value),
			IEquatable<TObject> other => other.Equals(objRef),
			IWrapper<JObjectLocalRef> otherRef => objRef.Equals(otherRef.Value),
			JObjectLocalRef jObjRef => objRef.Equals(jObjRef),
			IEquatable<JObjectLocalRef> other => other.Equals(objRef.Value),
			_ => false,
		};

	/// <summary>
	/// Indicates whether <paramref name="objRef"/> and a <paramref name="obj"/> are equal.
	/// </summary>
	/// <param name="objRef">The object reference to compare with <paramref name="obj"/>.</param>
	/// <param name="obj">The object to compare with <paramref name="objRef"/>.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="obj"/> and <paramref name="objRef"/> represent the same value;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean Equals(in JObjectLocalRef objRef, Object? obj)
		=> obj switch
		{
			JObjectLocalRef jObjRef => objRef.Equals(jObjRef),
			IWrapper<JObjectLocalRef> otherRef => objRef.Equals(otherRef.Value),
			IEquatable<JObjectLocalRef> other => other.Equals(objRef),
			_ => false,
		};

	static implicit INativeDataType<JObjectLocalRef>.operator JObjectLocalRef(JObjectLocalRef value) => value;
	static explicit INativeDataType<JObjectLocalRef>.operator JObjectLocalRef(JObjectLocalRef value) => value;
}
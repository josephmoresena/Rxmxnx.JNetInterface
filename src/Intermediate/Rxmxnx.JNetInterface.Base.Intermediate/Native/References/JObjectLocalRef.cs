namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for fully-qualified-class objects (<c>jobject</c>).
/// Represents a native-signed integer which serves as opaque identifier for any
/// object.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JObjectLocalRef : IFixedPointer, INativeType, IWrapper<JObjectLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JObject;

	/// <inheritdoc/>
	public IntPtr Pointer { get; }

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	public JObjectLocalRef() => this.Pointer = IntPtr.Zero;

	[ExcludeFromCodeCoverage]
	JObjectLocalRef IWrapper<JObjectLocalRef>.Value => this;

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this.Pointer.GetHashCode();
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => JObjectLocalRef.Equals(this, obj);

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
			IObjectReferenceType otherRef => objRef.Equals(otherRef.Value),
			IObjectGlobalReferenceType globalRef => objRef.Equals(globalRef.Value),
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
			IObjectReferenceType otherRef => objRef.Equals(otherRef.Value),
			IObjectGlobalReferenceType globalRef => objRef.Equals(globalRef.Value),
			IEquatable<JObjectLocalRef> other => other.Equals(objRef),
			_ => false,
		};
}
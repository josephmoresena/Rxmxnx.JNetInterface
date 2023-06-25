namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for fully-qualified-class objects (<c>jobject</c>).
/// Represents a native signed integer which serves as opaque identifier for any
/// object.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JObjectLocalRef : IFixedPointer, INative<JObjectLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type
	{
		get => JNativeType.JObject;
	}

	/// <summary>
	/// Internal native signed integer
	/// </summary>
	private readonly IntPtr _value;

	/// <inheritdoc/>
	public IntPtr Pointer
	{
		get => this._value;
	}

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	public JObjectLocalRef() => this._value = IntPtr.Zero;

	/// <summary>
	/// Internal constructor.
	/// </summary>
	internal JObjectLocalRef(JObjectLocalRef objRef) => this._value = objRef._value;

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => JObjectLocalRef.Equals(this, obj);

	/// <summary>
	/// Indicates wheter <paramref name="objRef"/> and a <paramref name="obj"/> are equal.
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
		=> JObjectLocalRef.Equals(objRef, obj);

	/// <summary>
	/// Indicates wheter <paramref name="objRef"/> and a <paramref name="obj"/> are equal.
	/// </summary>
	/// <typeparam name="TObject">The type of object reference.</typeparam>
	/// <param name="objRef">The object reference to compare with <paramref name="obj"/>.</param>
	/// <param name="obj">The object to compare with <paramref name="objRef"/>.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="obj"/> and <paramref name="objRef"/> represent the same value;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean Equals<TObject>(in TObject objRef, Object? obj) where TObject : unmanaged
	{
		if (obj is IWrapper<JObjectLocalRef> other)
			return other.Equals(objRef);
		return false;
	}
}
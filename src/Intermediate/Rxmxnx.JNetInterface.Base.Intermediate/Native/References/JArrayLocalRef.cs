namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jarray</c>). Represents a native signed integer
/// which serves as opaque identifier for an array object (<c>[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JArrayLocalRef : IObjectReference<JArrayLocalRef>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JArray;

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

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => JArrayLocalRef.Equals(this, obj);

	/// <summary>
	/// Indicates whether <paramref name="arr"/> and a <paramref name="obj"/> are equal.
	/// </summary>
	/// <typeparam name="TArray">The type of array reference.</typeparam>
	/// <param name="arr">The array reference to compare with <paramref name="obj"/>.</param>
	/// <param name="obj">The object to compare with <paramref name="arr"/>.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="obj"/> and <paramref name="arr"/> represent the same value;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static Boolean ArrayEquals<TArray>(in TArray arr, Object? obj)
		where TArray : unmanaged, IWrapper<JObjectLocalRef>, IEquatable<JArrayLocalRef>
	{
		if (obj is IEquatable<TArray> other)
			return other.Equals(arr);
		if (obj is JArrayLocalRef otherArr)
			return arr.Equals(otherArr);
		if (obj is JObjectLocalRef jObjRef)
			return arr.Equals(jObjRef);

		return JObjectLocalRef.ObjectEquals(arr, obj);
	}

	/// <summary>
	/// Indicates whether <paramref name="arr"/> and a <paramref name="obj"/> are equal.
	/// </summary>
	/// <typeparam name="TArray">The type of object reference.</typeparam>
	/// <param name="arr">The object reference to compare with <paramref name="obj"/>.</param>
	/// <param name="obj">The object to compare with <paramref name="arr"/>.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="obj"/> and <paramref name="arr"/> represent the same value;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean Equals<TArray>(in TArray arr, Object? obj)
		where TArray : unmanaged, IWrapper<JObjectLocalRef>
	{
		if (obj is IEquatable<TArray> other)
			return other.Equals(arr);
		if (obj is IEquatable<JArrayLocalRef> otherArr)
			return otherArr.Equals(arr);
		if (obj is JObjectLocalRef jObjRef)
			return arr.Equals(jObjRef);

		return JObjectLocalRef.ObjectEquals(arr, obj);
	}
}
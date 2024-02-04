namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// JNI local handle for array objects (<c>jarray</c>). Represents a native signed integer
/// which serves as opaque identifier for an array object (<c>[]</c>).
/// This handle is valid only for the thread who owns the reference.
/// </summary>
/// <remarks>This handle is valid only for the thread who owns the reference.</remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct JArrayLocalRef : IObjectReferenceType<JArrayLocalRef>
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
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this._value.GetHashCode();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
		=> obj switch
		{
			TArray arr2 => arr.Value.Equals(arr2.Value),
			IEquatable<TArray> other => other.Equals(arr),
			JArrayLocalRef otherArr => arr.Equals(otherArr),
			IArrayReferenceType arrRef => arr.Equals(arrRef.ArrayValue),
			_ => JObjectLocalRef.ObjectEquals(arr, obj),
		};

	/// <summary>
	/// Indicates whether <paramref name="arr"/> and a <paramref name="obj"/> are equal.
	/// </summary>
	/// <param name="arr">The object reference to compare with <paramref name="obj"/>.</param>
	/// <param name="obj">The object to compare with <paramref name="arr"/>.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="obj"/> and <paramref name="arr"/> represent the same value;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean Equals(in JArrayLocalRef arr, Object? obj)
		=> obj switch
		{
			JArrayLocalRef otherArr => arr.Equals(otherArr),
			IArrayReferenceType arrRef => arr.Equals(arrRef.ArrayValue),
			IEquatable<JArrayLocalRef> other => other.Equals(arr),
			_ => JObjectLocalRef.ObjectEquals(arr, obj),
		};
}
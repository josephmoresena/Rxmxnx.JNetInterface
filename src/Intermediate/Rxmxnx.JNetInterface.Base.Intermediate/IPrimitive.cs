namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
public interface IPrimitive : IObject, IDataType, IComparable, IConvertible
{
	/// <summary>
	/// JNI signature for an array of current primitive type.
	/// </summary>
	static abstract CString ArraySignature { get; }
	/// <inheritdoc cref="IDataType.PrimitiveMetadata"/>
	new static abstract JPrimitiveMetadata PrimitiveMetadata { get; }
}

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
public interface IPrimitive<TPrimitive, TValue> : IPrimitive<TValue>, IDataType<TPrimitive>
	where TPrimitive : unmanaged, IPrimitive<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	Int32 IComparable<TValue>.CompareTo(TValue other) => this.Value.CompareTo(other);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	Boolean IEquatable<TValue>.Equals(TValue other) => this.Value.Equals(other);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<Byte> span, ref Int32 offset)
	{
		ref TValue refValue = ref Unsafe.AsRef(this.Value);
		refValue.AsBytes().CopyTo(span[offset..]);
		offset += TPrimitive.PrimitiveMetadata.SizeOf;
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<JValue> span, Int32 index) { JValue.Create(this.Value); }

	/// <summary>
	/// Defines an implicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JObject"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TPrimitive"/> to implicitly convert.</param>
	static abstract implicit operator JObject(TPrimitive value);
	/// <summary>
	/// Defines an implicit conversion of a given <typeparamref name="TValue"/> to <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TValue"/> to implicitly convert.</param>
	static abstract implicit operator TPrimitive(TValue value);
}
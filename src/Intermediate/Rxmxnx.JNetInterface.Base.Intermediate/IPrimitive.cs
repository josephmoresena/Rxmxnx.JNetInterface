namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
public interface IPrimitive : IObject, IDataType, IComparable, IConvertible
{
	/// <inheritdoc cref="IDataType.Metadata"/>
	protected new static abstract JPrimitiveMetadata Metadata { get; }
	
	/// <summary>
	/// Retrieves the metadata for given primitive type.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of current java primitive datatype.</typeparam>
	/// <returns>The <see cref="JPrimitiveMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JPrimitiveMetadata GetMetadata<TPrimitive>() where TPrimitive : IPrimitive
		=> (JPrimitiveMetadata)IDataType.GetMetadata<TPrimitive>();
}

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
public interface IPrimitive<TPrimitive> : IPrimitive, IDataType<TPrimitive> where TPrimitive : IPrimitive<TPrimitive>
{
	static JDataTypeMetadata IDataType.Metadata => TPrimitive.Metadata;
	
	/// <summary>
	/// Defines an implicit conversion of a given <typeparamref name="TPrimitive"/> to <see cref="JObject"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TPrimitive"/> to implicitly convert.</param>
	static abstract implicit operator JObject(TPrimitive value);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JObject"/> to <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="jObj">A <see cref="JObject"/> to implicitly convert.</param>
	static abstract explicit operator TPrimitive(JObject jObj);
}

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
internal interface IPrimitive<TPrimitive, TValue> : IPrimitive<TPrimitive>, IPrimitiveWrapper<TValue>
	where TPrimitive : unmanaged, IPrimitive<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<Byte> span, ref Int32 offset)
	{
		ref TValue refValue = ref Unsafe.AsRef(this.Value);
		refValue.AsBytes().CopyTo(span[offset..]);
		offset += IDataType.GetMetadata<TPrimitive>().SizeOf;
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<JValue> span, Int32 index) => span[index] = JValue.Create(this.Value);

	/// <summary>
	/// Defines an implicit conversion of a given <typeparamref name="TValue"/> to <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TValue"/> to implicitly convert.</param>
	static abstract implicit operator TPrimitive(TValue value);
}
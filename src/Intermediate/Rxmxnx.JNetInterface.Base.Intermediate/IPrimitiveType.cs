namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
public interface IPrimitiveType : IObject, IDataType, IComparable, IConvertible
{
	/// <summary>
	/// Retrieves the metadata for given primitive type.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of current java primitive datatype.</typeparam>
	/// <returns>The <see cref="JPrimitiveMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JPrimitiveMetadata GetMetadata<TPrimitive>() where TPrimitive : IPrimitiveType
		=> (JPrimitiveMetadata)IDataType.GetMetadata<TPrimitive>();
}

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
public interface IPrimitiveType<TPrimitive> : IPrimitiveType, IDataType<TPrimitive>
	where TPrimitive : IPrimitiveType<TPrimitive>
{
	
	/// <inheritdoc cref="IDataType.Metadata"/>
	protected new static abstract JPrimitiveMetadata Metadata { get; }
	
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
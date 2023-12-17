namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IPrimitiveType : IObject, IDataType, IComparable, IConvertible
{
	/// <summary>
	/// Native primitive type.
	/// </summary>
	static virtual JNativeType JniType
		=> ValidationUtilities.ThrowInvalidInterface<JNativeType>(nameof(IPrimitiveType));
	
	/// <summary>
	/// Copy the sequence of bytes of current instance to <paramref name="span"/>.
	/// </summary>
	/// <param name="span">Binary span.</param>
	/// <returns>Number of bytes copied.</returns>
	internal void CopyTo(Span<Byte> span);

	/// <summary>
	/// Retrieves the metadata for given primitive type.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of current java primitive datatype.</typeparam>
	/// <returns>The <see cref="JPrimitiveTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JPrimitiveTypeMetadata GetMetadata<TPrimitive>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> (JPrimitiveTypeMetadata)IDataType.GetMetadata<TPrimitive>();
}

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
public interface IPrimitiveType<TPrimitive> : IPrimitiveType, IDataType<TPrimitive>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	static Type IDataType<TPrimitive>.SelfType => typeof(IPrimitiveType<TPrimitive>);
	static JTypeKind IDataType.Kind => JTypeKind.Primitive;

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
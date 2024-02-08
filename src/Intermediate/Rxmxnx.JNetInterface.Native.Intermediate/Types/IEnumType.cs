namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java enum type instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IEnumType : IReferenceType
{
	static JTypeKind IDataType.Kind => JTypeKind.Enum;

	/// <summary>
	/// Retrieves the metadata for given enum type.
	/// </summary>
	/// <typeparam name="TEnum">Type of current java enum datatype.</typeparam>
	/// <returns>The <see cref="JEnumTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JEnumTypeMetadata GetMetadata<TEnum>() where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
		=> (JEnumTypeMetadata)IDataType.GetMetadata<TEnum>();
}

/// <summary>
/// This interface exposes an object that represents a java enum type instance.
/// </summary>
/// <typeparam name="TEnum">Type of java enum type.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IEnumType<TEnum> : IEnumType, IReferenceType<TEnum> where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
{
	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	protected new static abstract JEnumTypeMetadata<TEnum> Metadata { get; }

	static JDataTypeMetadata IDataType<TEnum>.Metadata => TEnum.Metadata;
}
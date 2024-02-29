namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an enum <see cref="IDataType"/> type.
/// </summary>
public abstract record JEnumTypeMetadata : JClassTypeMetadata
{
	/// <summary>
	/// List of fields representing each enum value.
	/// </summary>
	public abstract IEnumFieldList Fields { get; }

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Enum;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Enum name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	private protected JEnumTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }

	/// <inheritdoc/>
	public override String ToString() => $"{base.ToString()}{nameof(JEnumTypeMetadata.Fields)} = {this.Fields}, ";
}

/// <summary>
/// This record stores the metadata for an enum <see cref="IDataType"/> type.
/// </summary>
/// <typeparam name="TEnum">Type of java enum type.</typeparam>
public abstract record JEnumTypeMetadata<TEnum> : JEnumTypeMetadata where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
{
	/// <inheritdoc/>
	private protected JEnumTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }

	/// <inheritdoc/>
	public override String ToString() => base.ToString();
}
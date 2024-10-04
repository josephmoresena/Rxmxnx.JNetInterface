namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an enum <see cref="IDataType"/> type.
/// </summary>
public abstract class JEnumTypeMetadata : JClassTypeMetadata
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
	/// <param name="className">Enum name of the current type.</param>
	private protected JEnumTypeMetadata(ReadOnlySpan<Byte> className) : base(className) { }
	/// <inheritdoc/>
	private protected JEnumTypeMetadata(TypeInfoSequence information) : base(information) { }

	/// <summary>
	/// Fields property.
	/// </summary>
	private protected override IAppendableProperty? GetPrimaryProperty() => this.Fields as IAppendableProperty;
}

/// <summary>
/// This record stores the metadata for an enum <see cref="IDataType"/> type.
/// </summary>
/// <typeparam name="TEnum">Type of java enum type.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class JEnumTypeMetadata<TEnum> : JEnumTypeMetadata where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
{
	/// <inheritdoc/>
	private protected JEnumTypeMetadata(ReadOnlySpan<Byte> className) : base(className) { }
	/// <inheritdoc/>
	private protected JEnumTypeMetadata(TypeInfoSequence information) : base(information) { }

	/// <inheritdoc/>
	internal override Boolean IsInstance(JReferenceObject jObject) => jObject is TEnum;
}
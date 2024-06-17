namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an array <see cref="IDataType"/> type.
/// </summary>
public abstract partial class JArrayTypeMetadata : JClassTypeMetadata
{
	/// <summary>
	/// Element type of the current array metadata.
	/// </summary>
	public abstract JDataTypeMetadata ElementMetadata { get; }

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Array;
	/// <inheritdoc/>
	public override JTypeModifier Modifier { get; }
	/// <inheritdoc/>
	public override IInterfaceSet Interfaces => InterfaceSet.ArraySet;

	/// <summary>
	/// Array dimension.
	/// </summary>
	public Int32 Dimension { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="signature">JNI signature for the current array type.</param>
	/// <param name="final">Indicates whether element type is final.</param>
	/// <param name="dimension">Array dimension.</param>
	private protected JArrayTypeMetadata(ReadOnlySpan<Byte> signature, Boolean final, Int32 dimension) : base(
		signature, signature)
	{
		this.Dimension = dimension;
		this.Modifier = final ? JTypeModifier.Final : JTypeModifier.Extensible;
		JArrayTypeMetadata.metadataCache.TryAdd(this.Signature.ToHexString(), this);
	}

	/// <summary>
	/// Indicates whether an instance of the current array type is instance of the current type of
	/// <paramref name="otherMetadata"/>.
	/// </summary>
	/// <param name="otherMetadata">A <see cref="JArrayTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if an instance of the current type is instance of
	/// the current type of <paramref name="otherMetadata"/>; otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean TypeOf(JArrayTypeMetadata otherMetadata)
	{
		if (this.Equals(otherMetadata)) return true;
		if (this.ElementMetadata is JReferenceTypeMetadata elementMetadata &&
		    otherMetadata.ElementMetadata is JReferenceTypeMetadata otherElementMetadata)
			return elementMetadata.TypeOf(otherElementMetadata);
		return false;
	}

	/// <inheritdoc/>
	public override Boolean TypeOf(JReferenceTypeMetadata otherMetadata)
		=> otherMetadata is JArrayTypeMetadata arrayMetadata ? this.TypeOf(arrayMetadata) : base.TypeOf(otherMetadata);

	/// <summary>
	/// Element class name property.
	/// </summary>
	private protected override ClassProperty GetPrimaryProperty()
		=> new()
		{
			PropertyName = nameof(JArrayTypeMetadata.ElementMetadata),
			Value = ClassNameHelper.GetClassName(this.ElementMetadata.Signature),
		};
	/// <summary>
	/// Array dimension property.
	/// </summary>
	private protected override ClassProperty GetSecondaryProperty()
		=> new() { PropertyName = nameof(JArrayTypeMetadata.Dimension), Value = $"{this.Dimension}", };
}
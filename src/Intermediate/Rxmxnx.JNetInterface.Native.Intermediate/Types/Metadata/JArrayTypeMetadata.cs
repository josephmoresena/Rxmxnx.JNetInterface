namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an array <see cref="IDataType"/> type.
/// </summary>
public abstract partial record JArrayTypeMetadata : JClassTypeMetadata
{
	/// <summary>
	/// Element type of current array metadata.
	/// </summary>
	public abstract JDataTypeMetadata ElementMetadata { get; }

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Array;
	/// <inheritdoc/>
	public override JTypeModifier Modifier { get; }
	/// <summary>
	/// Array dimension.
	/// </summary>
	public Int32 Dimension { get; }

	/// <summary>
	/// Element class name.
	/// </summary>
	public CString ElementClassName => this.ElementMetadata.ClassName;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="signature">JNI signature for current array type.</param>
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
	/// Indicates whether an instance of current array type is instance of the current type of
	/// <paramref name="otherMetadata"/>.
	/// </summary>
	/// <param name="otherMetadata">A <see cref="JArrayTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if an instance of current current type is instance of
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

	/// <inheritdoc/>
	public override String ToString()
		=> $"{base.ToString()}{nameof(JArrayTypeMetadata.ElementClassName)} = {this.ElementClassName}, " +
			$"{nameof(JArrayTypeMetadata.Dimension)} = {this.Dimension}, ";

	/// <summary>
	/// Sets the object element with <paramref name="index"/> on <paramref name="jArray"/>.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="index">Element index.</param>
	/// <param name="value">Object instance.</param>
	internal abstract void SetObjectElement(JArrayObject jArray, Int32 index, JReferenceObject? value);
}
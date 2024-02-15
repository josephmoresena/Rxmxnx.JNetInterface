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
	public override JTypeModifier Modifier => JTypeModifier.Final;
	/// <summary>
	/// Array deep.
	/// </summary>
	public Int32 Deep { get; }

	/// <summary>
	/// Element class name.
	/// </summary>
	public CString ElementClassName => this.ElementMetadata.ClassName;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="signature">JNI signature for current array type.</param>
	/// <param name="deep">Array deep.</param>
	private protected JArrayTypeMetadata(ReadOnlySpan<Byte> signature, Int32 deep) : base(signature, signature)
	{
		this.Deep = deep;
		JArrayTypeMetadata.metadataCache.TryAdd(this.Signature.ToHexString(), this);
	}

	/// <inheritdoc/>
	public override String ToString()
		=> $"{base.ToString()}{nameof(JArrayTypeMetadata.ElementClassName)} = {this.ElementClassName}, " +
			$"{nameof(JArrayTypeMetadata.Deep)} = {this.Deep}, ";

	/// <summary>
	/// Sets the object element with <paramref name="index"/> on <paramref name="jArray"/>.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="index">Element index.</param>
	/// <param name="value">Object instance.</param>
	internal abstract void SetObjectElement(JArrayObject jArray, Int32 index, JReferenceObject? value);
}
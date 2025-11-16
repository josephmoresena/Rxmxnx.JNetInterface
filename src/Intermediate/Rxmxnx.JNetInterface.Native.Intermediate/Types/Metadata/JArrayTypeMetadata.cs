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
	public sealed override JTypeKind Kind => JTypeKind.Array;
	/// <inheritdoc/>
	public sealed override JTypeModifier Modifier { get; }
	/// <inheritdoc/>
	public sealed override IInterfaceSet Interfaces => InterfaceSet.SerializableCloneableSet;

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
	private protected JArrayTypeMetadata(ReadOnlySpan<Byte> signature, Boolean final, Int32 dimension) : base(signature)
	{
		this.Dimension = dimension;
		this.Modifier = final ? JTypeModifier.Final : JTypeModifier.Extensible;
		JArrayTypeMetadata.metadataCache.TryAdd(this.Signature.ToHexString(), this);
	}

	/// <summary>
	/// Creates a <see cref="JArrayObject"/> instance for current type.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="length">Array length.</param>
	/// <returns>A <see cref="JArrayObject"/> instance.</returns>
	public abstract JArrayObject CreateInstance(IEnvironment env, Int32 length);

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
	public sealed override Boolean TypeOf(JReferenceTypeMetadata otherMetadata)
		=> otherMetadata is JArrayTypeMetadata arrayMetadata ? this.TypeOf(arrayMetadata) : base.TypeOf(otherMetadata);

	/// <summary>
	/// Retrieves the <see cref="JArrayTypeMetadata"/> instance for <paramref name="level"/> dimension.
	/// </summary>
	/// <param name="level">The number of dimensions of the array of the current element type.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	internal JArrayTypeMetadata WithAdditionalNesting(Int32 level)
	{
		JArrayTypeMetadata result = this;
		while (result.Dimension - this.Dimension < level)
			result = NativeValidationUtilities.ThrowIfMissingArrayMetadata(result);
		return result;
	}

	/// <summary>
	/// Element class name property.
	/// </summary>
	private protected override ClassProperty GetPrimaryProperty()
		=> new()
		{
			PropertyName = nameof(JArrayTypeMetadata.ElementMetadata),
			Value = ITypeInformation.GetJavaClassName(this.ElementMetadata),
		};
	/// <summary>
	/// Array dimension property.
	/// </summary>
	private protected override ClassProperty GetSecondaryProperty()
		=> new() { PropertyName = nameof(JArrayTypeMetadata.Dimension), Value = $"{this.Dimension}", };
}
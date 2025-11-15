namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
public abstract partial class JDataTypeMetadata : ITypeInformation
{
	/// <summary>
	/// Internal information.
	/// </summary>
	private readonly TypeInfoSequence _info;

	/// <summary>
	/// Array signature for the current type.
	/// </summary>
	public CString ArraySignature => this._info.ArraySignature;

	/// <summary>
	/// Metadata argument for the current type.
	/// </summary>
	public abstract JArgumentMetadata ArgumentMetadata { get; }
	/// <summary>
	/// CLR type of <see cref="IDataType"/>.
	/// </summary>
	public abstract Type Type { get; }
	/// <summary>
	/// Modifier of the current type.
	/// </summary>
	public abstract JTypeModifier Modifier { get; }
	/// <summary>
	/// Size of the current type in bytes.
	/// </summary>
	public abstract Int32 SizeOf { get; }
	/// <summary>
	/// Kind of the current type.
	/// </summary>
	public abstract JTypeKind Kind { get; }
	/// <summary>
	/// Specifies the minimun Java runtime version required for the current type.
	/// </summary>
	public abstract JRuntimeVersion Since { get; }

	/// <inheritdoc/>
	public CString ClassName => this._info.Name;
	/// <inheritdoc/>
	public CString Signature => this._info.Signature;
	/// <inheritdoc/>
	public String Hash => this._info.ToString();

	Boolean? ITypeInformation.IsFinal => this.Modifier is JTypeModifier.Final;

#if PACKAGE
	/// <summary>
	/// Creates a <see cref="JArrayTypeMetadata"/> from current instance.
	/// </summary>
	/// <param name="depth">Depth of the resulting metadata type.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	public JArrayTypeMetadata GetArrayMetadata(Byte depth)
	{
		JArrayTypeMetadata? arrayTypeMetadata = this as JArrayTypeMetadata;
		Int32 offset = arrayTypeMetadata is not null ? 0 : 1;

		arrayTypeMetadata ??= this.GetArrayMetadata();
		CommonValidationUtilities.ThrowIfInvalidDimension(this, arrayTypeMetadata?.Dimension - offset, depth);
		return arrayTypeMetadata!.WithAdditionalNesting((Int32)depth - offset);
	}

	/// <summary>
	/// Creates a <see cref="JArrayTypeMetadata"/> from current instance.
	/// </summary>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	public abstract JArrayTypeMetadata? GetArrayMetadata();
#endif
}
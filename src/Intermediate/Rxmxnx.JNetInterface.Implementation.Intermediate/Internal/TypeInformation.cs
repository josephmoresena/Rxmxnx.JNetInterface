namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Stores class information without <see cref="JDataTypeMetadata"/>.
/// </summary>
/// <param name="sequence">A <see cref="CStringSequence"/> containg class information.</param>
/// <param name="kind">A <see cref="JTypeKind"/> value.</param>
/// <param name="isFinal">Indicates whether current type is final.</param>
internal sealed class TypeInformation(TypeInfoSequence sequence, JTypeKind kind = default, Boolean? isFinal = default)
	: ITypeInformation
{
	/// <inheritdoc/>
	public CString ClassName => sequence.ClassName;
	/// <inheritdoc/>
	public CString Signature => sequence.Signature;
	/// <inheritdoc/>
	public String Hash => sequence.ToString();
	/// <inheritdoc/>
	public JTypeKind Kind => kind;
	/// <inheritdoc/>
	public Boolean? IsFinal => isFinal ?? this.IsFinalType();

	/// <summary>
	/// Indicates whether current type is final.
	/// </summary>
	/// <returns>Computed is final flag.</returns>
	private Boolean? IsFinalType()
	{
		if (this.Kind is JTypeKind.Primitive or JTypeKind.Enum) return true;
		return this.Kind is JTypeKind.Interface or JTypeKind.Annotation ? false : null;
	}
}
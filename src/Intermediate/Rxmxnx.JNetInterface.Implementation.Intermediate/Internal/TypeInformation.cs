namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Stores class information without <see cref="JDataTypeMetadata"/>.
/// </summary>
/// <param name="sequence">A <see cref="CStringSequence"/> containg class information.</param>
/// <param name="kind">A <see cref="JTypeKind"/> value.</param>
/// <param name="modifier">A <see cref="JTypeModifier"/> value.</param>
internal sealed class TypeInformation(
	CStringSequence sequence,
	JTypeKind kind = default,
	JTypeModifier? modifier = default) : ITypeInformation
{
	/// <inheritdoc/>
	public CString ClassName => sequence[0];
	/// <inheritdoc/>
	public CString Signature => sequence[1];
	/// <inheritdoc/>
	public String Hash => sequence.ToString();
	/// <inheritdoc/>
	public JTypeKind Kind => kind;
	/// <inheritdoc/>
	public JTypeModifier? Modifier => modifier;
	/// <inheritdoc/>
	public IFixedPointer.IDisposable GetClassNameFixedPointer() => sequence.GetFixedPointer();
}
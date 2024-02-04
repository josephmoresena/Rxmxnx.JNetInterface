namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Stores class information without <see cref="JDataTypeMetadata"/>.
/// </summary>
/// <param name="sequence">A <see cref="CStringSequence"/> containg class information.</param>
internal sealed class TypeInformation(CStringSequence sequence) : ITypeInformation
{
	/// <inheritdoc/>
	public CString ClassName => sequence[0];
	/// <inheritdoc/>
	public CString Signature => sequence[1];
	/// <inheritdoc/>
	public String Hash => sequence.ToString();
}
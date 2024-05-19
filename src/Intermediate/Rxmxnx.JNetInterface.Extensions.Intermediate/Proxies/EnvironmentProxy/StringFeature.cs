namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract Int32 GetLength(JReferenceObject jObject);
	/// <inheritdoc/>
	public abstract Int32 GetUtf8Length(JReferenceObject jObject);
	/// <inheritdoc/>
	public abstract INativeMemoryAdapter GetSequence(JStringObject jString, JMemoryReferenceKind referenceKind);
	/// <inheritdoc/>
	public abstract INativeMemoryAdapter GetUtf8Sequence(JStringObject jString, JMemoryReferenceKind referenceKind);
	/// <inheritdoc/>
	public abstract INativeMemoryAdapter GetCriticalSequence(JStringObject jString, JMemoryReferenceKind referenceKind);
}
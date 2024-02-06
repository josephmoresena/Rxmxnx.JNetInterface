namespace Rxmxnx.JNetInterface.Native.Dummies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract Int32 GetLength(JReferenceObject jObject);
	/// <inheritdoc/>
	public abstract Int32 GetUtf8Length(JReferenceObject jObject);
	/// <inheritdoc/>
	public abstract void GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex = 0);
	/// <inheritdoc/>
	public abstract void GetCopyUtf8(JStringObject jString, Memory<Byte> utf8Units, Int32 startIndex = 0);
	/// <inheritdoc/>
	public abstract INativeMemoryAdapter GetSequence(JStringObject jString, JMemoryReferenceKind referenceKind);
	/// <inheritdoc/>
	public abstract INativeMemoryAdapter GetUtf8Sequence(JStringObject jString, JMemoryReferenceKind referenceKind);
	/// <inheritdoc/>
	public abstract INativeMemoryAdapter GetCriticalSequence(JStringObject jString, JMemoryReferenceKind referenceKind);
	/// <inheritdoc/>
	public abstract JStringObject Create(ReadOnlySpan<Char> data);
	/// <inheritdoc/>
	public abstract JStringObject Create(ReadOnlySpan<Byte> utf8Data);
}
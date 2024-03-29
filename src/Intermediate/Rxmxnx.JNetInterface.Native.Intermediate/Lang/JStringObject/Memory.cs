namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// Retrieves the native memory of UTF-16 characters.
	/// </summary>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>A <see cref="JNativeMemory{Char}"/> instance.</returns>
	public JNativeMemory<Char> GetNativeChars(JMemoryReferenceKind referenceKind = JMemoryReferenceKind.Local)
	{
		INativeMemoryAdapter adapter = this.Environment.StringFeature.GetSequence(this, referenceKind);
		return new(adapter);
	}
	/// <summary>
	/// Retrieves the critical native memory of UTF-16 characters.
	/// </summary>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>A <see cref="JNativeMemory{Char}"/> instance.</returns>
	public JNativeMemory<Char> GetCriticalChars(
		JMemoryReferenceKind referenceKind = JMemoryReferenceKind.ThreadUnrestricted)
	{
		INativeMemoryAdapter adapter = this.Environment.StringFeature.GetCriticalSequence(this, referenceKind);
		return new(adapter);
	}
	/// <summary>
	/// Retrieves the native memory of UTF-8 characters.
	/// </summary>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>A <see cref="JNativeMemory{Char}"/> instance.</returns>
	public JNativeMemory<Byte> GetNativeUtf8Chars(JMemoryReferenceKind referenceKind = JMemoryReferenceKind.Local)
	{
		INativeMemoryAdapter adapter = this.Environment.StringFeature.GetUtf8Sequence(this, referenceKind);
		return new(adapter);
	}
}
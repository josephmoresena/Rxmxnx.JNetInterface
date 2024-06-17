namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a JNI string provider instance.
/// </summary>
internal partial interface IStringFeature
{
	/// <summary>
	/// Retrieves the length of given <see cref="JReferenceObject"/> instance.
	/// </summary>
	/// <param name="jObject"><see cref="JReferenceObject"/> instance.</param>
	/// <returns>String length.</returns>
	Int32 GetLength(JReferenceObject jObject);
	/// <summary>
	/// Retrieves the UTF-8 length of given <see cref="JReferenceObject"/> instance.
	/// </summary>
	/// <param name="jObject"><see cref="JReferenceObject"/> instance.</param>
	/// <returns>UTF-8 string length.</returns>
	Int32 GetUtf8Length(JReferenceObject jObject);
	/// <summary>
	/// Copies UTF-16 <paramref name="jString"/> chars into <paramref name="chars"/>.
	/// </summary>
	/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
	/// <param name="chars">Destination buffer.</param>
	/// <param name="startIndex">Offset position.</param>
	void GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex = 0);
	/// <summary>
	/// Copies UTF-8 <paramref name="jString"/> units into <paramref name="utf8Units"/>.
	/// </summary>
	/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
	/// <param name="utf8Units">Destination buffer.</param>
	/// <param name="startIndex">Offset position.</param>
	void GetUtf8Copy(JStringObject jString, Span<Byte> utf8Units, Int32 startIndex = 0);

	/// <summary>
	/// Retrieves a <see cref="INativeMemoryAdapter"/> to <see cref="JStringObject"/> characters.
	/// </summary>
	/// <param name="jString"><see cref="JStringObject"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>Adapter of <paramref name="jString"/> UTF-16 data.</returns>
	INativeMemoryAdapter GetSequence(JStringObject jString, JMemoryReferenceKind referenceKind);
	/// <summary>
	/// Retrieves a <see cref="INativeMemoryAdapter"/> to <see cref="JStringObject"/> UTF-8 characters.
	/// </summary>
	/// <param name="jString"><see cref="JStringObject"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>Adapter of <paramref name="jString"/> UTF-8 data.</returns>
	INativeMemoryAdapter GetUtf8Sequence(JStringObject jString, JMemoryReferenceKind referenceKind);
	/// <summary>
	/// Retrieves a direct <see cref="INativeMemoryAdapter"/> to <see cref="JStringObject"/> characters.
	/// </summary>
	/// <param name="jString"><see cref="JStringObject"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>Adapter of <paramref name="jString"/> UTF-16 data.</returns>
	INativeMemoryAdapter GetCriticalSequence(JStringObject jString, JMemoryReferenceKind referenceKind);
}
namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a JNI string provider instance.
/// </summary>
public interface IStringProvider
{
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="data"/>.
	/// </summary>
	/// <param name="data">UTF-16 string data.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	JStringObject Create(ReadOnlySpan<Char> data);
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="utf8Data"/>.
	/// </summary>
	/// <param name="utf8Data">UTF-8 string data.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	JStringObject Create(ReadOnlySpan<Byte> utf8Data);
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
	/// Retrieves a pointer to <see cref="JStringObject"/> characters.
	/// </summary>
	/// <param name="jString"><see cref="JStringObject"/> instance.</param>
	/// <param name="isCopy">Output. Indicates whether the resulting pointer references a data copy.</param>
	/// <returns>Pointer to <paramref name="jString"/> UTF-16 data.</returns>
	IntPtr GetSequence(JStringObject jString, out Boolean isCopy);
	/// <summary>
	/// Retrieves a pointer to <see cref="JStringObject"/> UTF-8 characters.
	/// </summary>
	/// <param name="jString"><see cref="JStringObject"/> instance.</param>
	/// <param name="isCopy">Output. Indicates whether the resulting pointer references a data copy.</param>
	/// <returns>Pointer to <paramref name="jString"/> UTF-8 data.</returns>
	IntPtr GetUtf8Sequence(JStringObject jString, out Boolean isCopy);
	/// <summary>
	/// Retrieves a direct pointer to <see cref="JStringObject"/> characters.
	/// </summary>
	/// <param name="jString"><see cref="JStringObject"/> instance.</param>
	/// <returns>Pointer to <paramref name="jString"/> UTF-16 data.</returns>
	IntPtr GetCriticalSequence(JStringObject jString);
	/// <summary>
	/// Releases the UTF-16 pointer associated to <paramref name="jString"/>.
	/// </summary>
	/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
	/// <param name="pointer">Pointer to release to.</param>
	void ReleaseSequence(JStringObject jString, IntPtr pointer);
	/// <summary>
	/// Releases the UTF-8 pointer associated to <paramref name="jString"/>.
	/// </summary>
	/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
	/// <param name="pointer">Pointer to release to.</param>
	void ReleaseUtf8Sequence(JStringObject jString, IntPtr pointer);
	/// <summary>
	/// Releases the critical UTF-16 pointer associated to <paramref name="jString"/>.
	/// </summary>
	/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
	/// <param name="pointer">Pointer to release to.</param>
	void ReleaseCriticalSequence(JStringObject jString, IntPtr pointer);
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
	void GetCopyUtf8(JStringObject jString, Memory<Byte> utf8Units, Int32 startIndex = 0);
}
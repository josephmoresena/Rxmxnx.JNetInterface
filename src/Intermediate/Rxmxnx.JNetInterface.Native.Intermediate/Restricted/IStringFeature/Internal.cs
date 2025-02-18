namespace Rxmxnx.JNetInterface.Restricted;

internal partial interface IStringFeature
{
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="data"/>.
	/// </summary>
	/// <param name="data">UTF-16 string data.</param>
	/// <param name="value">String value.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	internal JStringObject Create(ReadOnlySpan<Char> data, String? value = default);
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="utf8Data"/>.
	/// </summary>
	/// <param name="utf8Data">UTF-8 string data.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	internal JStringObject Create(ReadOnlySpan<Byte> utf8Data);
	/// <summary>
	/// Retrieves a pointer to <paramref name="stringRef"/> characters.
	/// </summary>
	/// <param name="stringRef"><see cref="JStringLocalRef"/> reference.</param>
	/// <param name="isCopy">Output. Indicates whether the resulting pointer references a data copy.</param>
	/// <returns>Pointer to <paramref name="stringRef"/> UTF-16 data.</returns>
	internal ReadOnlyValPtr<Char> GetSequence(JStringLocalRef stringRef, out Boolean isCopy);
	/// <summary>
	/// Retrieves a pointer to <paramref name="stringRef"/> UTF-8 characters.
	/// </summary>
	/// <param name="stringRef"><see cref="JStringLocalRef"/> reference.</param>
	/// <param name="isCopy">Output. Indicates whether the resulting pointer references a data copy.</param>
	/// <returns>Pointer to <paramref name="stringRef"/> UTF-8 data.</returns>
	internal ReadOnlyValPtr<Byte> GetUtf8Sequence(JStringLocalRef stringRef, out Boolean isCopy);
	/// <summary>
	/// Retrieves a direct pointer to <paramref name="stringRef"/> characters.
	/// </summary>
	/// <param name="stringRef"><see cref="JStringLocalRef"/> reference.</param>
	/// <returns>Pointer to <paramref name="stringRef"/> UTF-16 data.</returns>
	internal ReadOnlyValPtr<Char> GetCriticalSequence(JStringLocalRef stringRef);
	/// <summary>
	/// Releases the UTF-16 pointer associated to <paramref name="stringRef"/>.
	/// </summary>
	/// <param name="stringRef"><see cref="JStringLocalRef"/> reference.</param>
	/// <param name="pointer">Pointer to release to.</param>
	internal void ReleaseSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer);
	/// <summary>
	/// Releases the UTF-8 pointer associated to <paramref name="stringRef"/>.
	/// </summary>
	/// <param name="stringRef"><see cref="JStringLocalRef"/> reference.</param>
	/// <param name="pointer">Pointer to release to.</param>
	internal void ReleaseUtf8Sequence(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> pointer);
	/// <summary>
	/// Releases the critical UTF-16 pointer associated to <paramref name="stringRef"/>.
	/// </summary>
	/// <param name="stringRef"><see cref="JStringLocalRef"/> reference.</param>
	/// <param name="pointer">Pointer to release to.</param>
	internal void ReleaseCriticalSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer);
}
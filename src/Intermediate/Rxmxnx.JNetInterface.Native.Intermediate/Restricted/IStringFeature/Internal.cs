namespace Rxmxnx.JNetInterface.Restricted;

public partial interface IStringFeature
{
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
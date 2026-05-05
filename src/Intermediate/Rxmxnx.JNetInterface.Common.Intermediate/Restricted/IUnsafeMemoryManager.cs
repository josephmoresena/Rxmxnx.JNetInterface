namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents an unsafe memory manager object.
/// </summary>
internal interface IUnsafeMemoryManager
{
	/// <summary>
	/// Deletes <paramref name="globalRef"/>.
	/// </summary>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
	void DeleteGlobalRef(JGlobalRef globalRef);
	/// <summary>
	/// Deletes <paramref name="weakRef"/>.
	/// </summary>
	/// <param name="weakRef">A <see cref="JWeakRef"/> reference.</param>
	void DeleteWeakGlobalRef(JWeakRef weakRef);
	/// <summary>
	/// Deletes <paramref name="localRef"/>.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference to remove.</param>
	void DeleteLocalRef(JObjectLocalRef localRef);
	/// <summary>
	/// Retrieves type of given reference.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>A <see cref="JReferenceType"/> value.</returns>
	JReferenceType GetReferenceType(JObjectLocalRef localRef);
	/// <summary>
	/// Tests whether two references point to the same object.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="otherRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if both references refer to the same object; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	Boolean IsSame(JObjectLocalRef localRef, JObjectLocalRef otherRef);
}
namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents a native memory manager object.
/// </summary>
internal interface INativeMemoryManager
{
	/// <summary>
	/// Creates a new <see cref="INativeTransaction"/> transaction.
	/// </summary>
	/// <param name="capacity">Transaction capacity.</param>
	/// <returns>A new <see cref="INativeTransaction"/> instance.</returns>
	INativeTransaction CreateTransaction(Int32 capacity);
	/// <summary>
	/// Creates a new synchronizer for <paramref name="jObject"/> instance.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>A new synchronizer for <paramref name="jObject"/> instance.</returns>
	IDisposable CreateSynchronized(IEnvironment env, JReferenceObject jObject);
	/// <summary>
	/// Creates a native memory adapter instance for <paramref name="jString"/>.
	/// </summary>
	/// <param name="jString"><see cref="JStringObject"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <param name="critical">Indicates this adapter is for a critical sequence.</param>
	/// <returns>A new native memory adapter instance for <paramref name="jString"/>.</returns>
	INativeMemoryAdapter CreateMemoryAdapter(JStringObject jString, JMemoryReferenceKind referenceKind,
		Boolean? critical);
	/// <summary>
	/// Creates a native memory adapter instance for <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray"><see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <param name="critical">Indicates this adapter is for a critical sequence.</param>
	/// <returns>A new native memory adapter instance for <paramref name="jArray"/>.</returns>
	INativeMemoryAdapter CreateMemoryAdapter<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind, Boolean critical) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Indicates whether <paramref name="weakRef"/> can be removed safely.
	/// </summary>
	/// <param name="weakRef">A <see cref="JWeakRef"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="weakRef"/> can be removed safely;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	Boolean SecureRemove(JWeakRef weakRef);
	/// <summary>
	/// Indicates whether <paramref name="globalRef"/> can be removed safely.
	/// </summary>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="globalRef"/> can be removed safely;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	Boolean SecureRemove(JGlobalRef globalRef);
	/// <summary>
	/// Indicates whether <paramref name="localRef"/> can be removed safely.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="localRef"/> can be removed safely;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	Boolean SecureRemove(JObjectLocalRef localRef);
}
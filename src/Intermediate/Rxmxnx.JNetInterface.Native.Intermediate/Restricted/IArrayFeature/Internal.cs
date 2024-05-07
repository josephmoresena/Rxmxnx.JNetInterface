namespace Rxmxnx.JNetInterface.Restricted;

public partial interface IArrayFeature
{
	/// <summary>
	/// Retrieves a pointer to <paramref name="arrayRef"/> elements.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="arrayRef">A <see cref="JArrayLocalRef"/> reference.</param>
	/// <param name="isCopy">Output. Indicates whether the resulting pointer references a data copy.</param>
	/// <returns>Pointer to <paramref name="arrayRef"/> data.</returns>
	internal IntPtr GetPrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, out Boolean isCopy)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Retrieves a direct pointer to <paramref name="arrayRef"/> elements.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JArrayLocalRef"/> reference.</param>
	/// <returns>Pointer to <paramref name="arrayRef"/> data.</returns>
	internal ValPtr<Byte> GetPrimitiveCriticalSequence(JArrayLocalRef arrayRef);
	/// <summary>
	/// Releases the pointer associated to <paramref name="arrayRef"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="arrayRef">A <see cref="JArrayLocalRef"/> reference.</param>
	/// <param name="pointer">Pointer to release to.</param>
	/// <param name="mode">Release mode.</param>
	internal void ReleasePrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, IntPtr pointer, JReleaseMode mode)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Releases the critical pointer associated to <paramref name="arrayRef"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JArrayLocalRef"/> reference.</param>
	/// <param name="criticalPtr">Pointer to release to.</param>
	internal void ReleasePrimitiveCriticalSequence(JArrayLocalRef arrayRef, ValPtr<Byte> criticalPtr);
}
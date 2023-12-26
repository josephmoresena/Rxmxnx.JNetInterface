namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a java array provider instance.
/// </summary>
public interface IArrayProvider
{
	/// <summary>
	/// Creates an empty <see cref="JArrayObject{TElement}"/> instance.
	/// </summary>
	/// <typeparam name="TElement">Type of array element.</typeparam>
	/// <param name="length">New array length.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance.</returns>
	JArrayObject<TElement> CreateArray<TElement>(Int32 length) where TElement : IObject, IDataType<TElement>;
	/// <summary>
	/// Creates a <paramref name="initialElement"/> filled <see cref="JArrayObject{TElement}"/> instance.
	/// </summary>
	/// <typeparam name="TElement">Type of array element.</typeparam>
	/// <param name="length">New array length.</param>
	/// <param name="initialElement">Instance to set each array element.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance.</returns>
	JArrayObject<TElement> CreateArray<TElement>(Int32 length, TElement initialElement)
		where TElement : IObject, IDataType<TElement>;
	/// <summary>
	/// Retrieves the array length from <paramref name="jObject"/>
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>The length of <paramref name="jObject"/> array.</returns>
	Int32 GetArrayLength(JReferenceObject jObject);
	/// <summary>
	/// Retrieves the element with <paramref name="index"/> on <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TElement">Type of <paramref name="jArray"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="index">Element index.</param>
	/// <returns>The element with <paramref name="index"/> on <paramref name="jArray"/>.</returns>
	TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
		where TElement : IObject, IDataType<TElement>;
	/// <summary>
	/// Sets the element with <paramref name="index"/> on <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TElement">Type of <paramref name="jArray"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="index">Element index.</param>
	/// <param name="value">Element value.</param>
	void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
		where TElement : IObject, IDataType<TElement>;
	/// <summary>
	/// Retrieves a pointer to <see cref="JArrayObject{TPrimitive}"/> elements.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="isCopy">Output. Indicates whether the resulting pointer references a data copy.</param>
	/// <returns>Pointer to <paramref name="jArray"/> UTF-16 data.</returns>
	IntPtr GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, out Boolean isCopy)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Retrieves a direct pointer to <see cref="JArrayObject{TPrimitive}"/> elements.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <returns>Pointer to <paramref name="jArray"/> UTF-16 data.</returns>
	IntPtr GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Releases the pointer associated to <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="pointer">Pointer to release to.</param>
	/// <param name="mode">Release mode.</param>
	void ReleaseSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, IntPtr pointer, JReleaseMode mode)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Releases the critical pointer associated to <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="pointer">Pointer to release to.</param>
	void ReleaseCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, IntPtr pointer)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Copies <paramref name="jArray"/> elements into <paramref name="elements"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="startIndex">Offset position.</param>
	/// <param name="elements">Destination buffer.</param>
	void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Int32 startIndex, Memory<TPrimitive> elements)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Copies <paramref name="elements"/> elements into <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="elements">Source buffer.</param>
	/// <param name="startIndex">Offset position.</param>
	void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlyMemory<TPrimitive> elements, Int32 startIndex = 0)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
}
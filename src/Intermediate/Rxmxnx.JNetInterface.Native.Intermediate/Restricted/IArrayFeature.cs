namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes JNI array feature.
/// </summary>
internal partial interface IArrayFeature
{
	/// <summary>
	/// Creates an empty <see cref="JArrayObject{TElement}"/> instance.
	/// </summary>
	/// <typeparam name="TElement">Type of array element.</typeparam>
	/// <param name="length">New array length.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance.</returns>
	JArrayObject<TElement> CreateArray<TElement>(Int32 length) where TElement : IDataType<TElement>;
	/// <summary>
	/// Creates a <paramref name="initialElement"/> filled <see cref="JArrayObject{TElement}"/> instance.
	/// </summary>
	/// <typeparam name="TElement">Type of array element.</typeparam>
	/// <param name="length">New array length.</param>
	/// <param name="initialElement">Instance to set each array element.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance.</returns>
	JArrayObject<TElement> CreateArray<TElement>(Int32 length, TElement initialElement)
		where TElement : IDataType<TElement>;
	/// <summary>
	/// Creates an empty <see cref="JArrayObject{TElement}"/> instance.
	/// </summary>
	/// <typeparam name="TElement">Type of array element.</typeparam>
	/// <param name="jClass">Element <see cref="JClassObject"/> instance.</param>
	/// <param name="length">New array length.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance.</returns>
	JArrayObject<TElement> CreateArray<TElement>(JClassObject jClass, Int32 length)
		where TElement : IDataType<TElement>;
	/// <summary>
	/// Creates a <paramref name="initialElement"/> filled <see cref="JArrayObject{TElement}"/> instance.
	/// </summary>
	/// <typeparam name="TElement">Type of array element.</typeparam>
	/// <param name="jClass">Element <see cref="JClassObject"/> instance.</param>
	/// <param name="length">New array length.</param>
	/// <param name="initialElement">Instance to set each array element.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance.</returns>
	JArrayObject<TElement> CreateArray<TElement>(JClassObject jClass, Int32 length, TElement initialElement)
		where TElement : IDataType<TElement>;
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
	TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index) where TElement : IDataType<TElement>;
	/// <summary>
	/// Sets the element with <paramref name="index"/> on <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TElement">Type of <paramref name="jArray"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="index">Element index.</param>
	/// <param name="value">Element value.</param>
	void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
		where TElement : IDataType<TElement>;
	/// <summary>
	/// Determines the index of a specific item in <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TElement">Type of <paramref name="jArray"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="item">The object to locate in <paramref name="jArray"/>.</param>
	/// <returns>The index of <paramref name="item"/> if found in <paramref name="jArray"/>; otherwise, -1.</returns>
	Int32 IndexOf<TElement>(JArrayObject<TElement> jArray, TElement? item) where TElement : IDataType<TElement>;
	/// <summary>
	/// Copies the elements of the <paramref name="jArray"/> to a <paramref name="array"/>,
	/// starting at a particular index.
	/// </summary>
	/// <typeparam name="TElement">Type of <paramref name="jArray"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="array">
	/// The one-dimensional <typeparamref name="TElement"/> array that is the destination of the elements copied
	/// from <paramref name="jArray"/>.
	/// </param>
	/// <param name="arrayIndex">
	/// The zero-based index in <paramref name="array"/> at which copying begins.
	/// </param>
	/// <exception cref="ArgumentNullException">
	/// <paramref name="array"/> is <see langword="null"/>.
	/// </exception>
	/// <exception cref="ArgumentOutOfRangeException">
	/// <paramref name="arrayIndex"/> is less than 0.
	/// </exception>
	/// <exception cref="ArgumentException">
	/// The number of elements in the source <paramref name="jArray"/> is greater
	/// than the available space from <paramref name="arrayIndex"/> to the end of the
	/// destination <paramref name="array"/>.
	/// </exception>
	void CopyTo<TElement>(JArrayObject<TElement> jArray, TElement?[] array, Int32 arrayIndex)
		where TElement : IDataType<TElement>;
	/// <summary>
	/// Copies <paramref name="jArray"/> elements into <paramref name="elements"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="elements">Destination buffer.</param>
	/// <param name="startIndex">Offset position.</param>
	void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Span<TPrimitive> elements, Int32 startIndex = 0)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Copies <paramref name="elements"/> elements into <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="elements">Source buffer.</param>
	/// <param name="startIndex">Offset position.</param>
	void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlySpan<TPrimitive> elements, Int32 startIndex = 0)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Retrieves a <see cref="INativeMemoryAdapter"/> to <see cref="JArrayObject{TPrimitive}"/> elements.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>Adapter of <paramref name="jArray"/> data.</returns>
	INativeMemoryAdapter GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, JMemoryReferenceKind referenceKind)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Retrieves a direct <see cref="INativeMemoryAdapter"/> to <see cref="JArrayObject{TPrimitive}"/> elements.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>Adapter of <paramref name="jArray"/> data.</returns>
	INativeMemoryAdapter GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
}
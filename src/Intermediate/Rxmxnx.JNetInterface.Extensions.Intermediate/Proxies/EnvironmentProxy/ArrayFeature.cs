namespace Rxmxnx.JNetInterface.Native.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract JArrayObject<TElement> CreateArray<TElement>(Int32 length)
		where TElement : IObject, IDataType<TElement>;
	/// <inheritdoc/>
	public abstract JArrayObject<TElement> CreateArray<TElement>(Int32 length, TElement initialElement)
		where TElement : IObject, IDataType<TElement>;
	/// <inheritdoc/>
	public abstract Int32 GetArrayLength(JReferenceObject jObject);
	/// <inheritdoc/>
	public abstract TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
		where TElement : IObject, IDataType<TElement>;
	/// <inheritdoc/>
	public abstract void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
		where TElement : IObject, IDataType<TElement>;
	/// <inheritdoc/>
	public abstract Int32 IndexOf<TElement>(JArrayObject<TElement> jArray, TElement? item)
		where TElement : IObject, IDataType<TElement>;
	/// <inheritdoc/>
	public abstract void CopyTo<TElement>(JArrayObject<TElement> jArray, TElement?[] array, Int32 arrayIndex)
		where TElement : IObject, IDataType<TElement>;
	/// <inheritdoc/>
	public abstract void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Int32 startIndex,
		Memory<TPrimitive> elements) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <inheritdoc/>
	public abstract void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlyMemory<TPrimitive> elements,
		Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <inheritdoc/>
	public abstract INativeMemoryAdapter GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <inheritdoc/>
	public abstract INativeMemoryAdapter GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
}
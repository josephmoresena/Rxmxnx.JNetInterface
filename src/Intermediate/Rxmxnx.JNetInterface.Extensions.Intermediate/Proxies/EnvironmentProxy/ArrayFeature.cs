namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract JArrayObject<TElement> CreateArray<TElement>(Int32 length) where TElement : IDataType<TElement>;
	/// <inheritdoc/>
	public abstract JArrayObject<TElement> CreateArray<TElement>(Int32 length, TElement initialElement)
		where TElement : IDataType<TElement>;
	/// <inheritdoc/>
	public abstract Int32 GetArrayLength(JReferenceObject jObject);
	/// <inheritdoc/>
	public abstract TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
		where TElement : IDataType<TElement>;
	/// <inheritdoc/>
	public abstract void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
		where TElement : IDataType<TElement>;
	/// <inheritdoc/>
	public abstract Int32 IndexOf<TElement>(JArrayObject<TElement> jArray, TElement? item)
		where TElement : IDataType<TElement>;
	/// <inheritdoc/>
	public abstract void CopyTo<TElement>(JArrayObject<TElement> jArray, TElement?[] array, Int32 arrayIndex)
		where TElement : IDataType<TElement>;
	/// <inheritdoc/>
	public abstract INativeMemoryAdapter GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <inheritdoc/>
	public abstract INativeMemoryAdapter GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
}
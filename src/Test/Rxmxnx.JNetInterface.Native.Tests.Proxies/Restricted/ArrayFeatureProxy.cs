namespace Rxmxnx.JNetInterface.Tests.Restricted;

[ExcludeFromCodeCoverage]
public abstract class ArrayFeatureProxy : IArrayFeature
{
	public abstract JArrayObject<TElement> CreateArray<TElement>(Int32 length)
		where TElement : IObject, IDataType<TElement>;
	public abstract JArrayObject<TElement> CreateArray<TElement>(Int32 length, TElement initialElement)
		where TElement : IObject, IDataType<TElement>;
	public abstract Int32 GetArrayLength(JReferenceObject jObject);
	public abstract TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
		where TElement : IObject, IDataType<TElement>;
	public abstract void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
		where TElement : IObject, IDataType<TElement>;
	public abstract Int32 IndexOf<TElement>(JArrayObject<TElement> jArray, TElement? item)
		where TElement : IObject, IDataType<TElement>;
	public abstract void CopyTo<TElement>(JArrayObject<TElement> jArray, TElement?[] array, Int32 arrayIndex)
		where TElement : IObject, IDataType<TElement>;
	public abstract void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Int32 startIndex,
		Memory<TPrimitive> elements) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	public abstract void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlyMemory<TPrimitive> elements,
		Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	public abstract INativeMemoryAdapter GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	public abstract INativeMemoryAdapter GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	public abstract IntPtr GetPrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, out Boolean isCopy)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	public abstract ValPtr<Byte> GetPrimitiveCriticalSequence(JArrayLocalRef arrayRef);
	public abstract void ReleasePrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, IntPtr pointer,
		JReleaseMode mode) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	public abstract void ReleasePrimitiveCriticalSequence(JArrayLocalRef arrayRef, ValPtr<Byte> criticalPtr);
}
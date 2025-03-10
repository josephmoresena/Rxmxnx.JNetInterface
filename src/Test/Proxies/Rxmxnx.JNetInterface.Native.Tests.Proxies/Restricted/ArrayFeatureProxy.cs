namespace Rxmxnx.JNetInterface.Tests.Restricted;

[ExcludeFromCodeCoverage]
public abstract partial class ArrayFeatureProxy : IArrayFeature
{
	public abstract JArrayObject<TElement> CreateArray<TElement>(Int32 length) where TElement : IDataType<TElement>;
	public abstract JArrayObject<TElement> CreateArray<TElement>(Int32 length, TElement initialElement)
		where TElement : IDataType<TElement>;
	public abstract JArrayObject<TElement> CreateArray<TElement>(JClassObject jClass, Int32 length)
		where TElement : IDataType<TElement>;
	public abstract JArrayObject<TElement> CreateArray<TElement>(JClassObject jClass, Int32 length,
		TElement initialElement) where TElement : IDataType<TElement>;
	public abstract Int32 GetArrayLength(JReferenceObject jObject);
	public abstract TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
		where TElement : IDataType<TElement>;
	public abstract void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
		where TElement : IDataType<TElement>;
	public abstract Int32 IndexOf<TElement>(JArrayObject<TElement> jArray, TElement? item)
		where TElement : IDataType<TElement>;
	public abstract void CopyTo<TElement>(JArrayObject<TElement> jArray, TElement?[] array, Int32 arrayIndex)
		where TElement : IDataType<TElement>;
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
	public abstract void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, IFixedMemory<TPrimitive> elements,
		Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	public abstract void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, IReadOnlyFixedMemory<TPrimitive> elements,
		Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
}
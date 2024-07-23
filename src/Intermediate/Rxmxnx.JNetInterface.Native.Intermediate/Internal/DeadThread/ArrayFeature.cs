namespace Rxmxnx.JNetInterface.Internal;

internal partial class DeadThread : IArrayFeature
{
	JArrayObject<TElement> IArrayFeature.CreateArray<TElement>(Int32 length)
		=> this.ThrowInvalidResult<JArrayObject<TElement>>();
	JArrayObject<TElement> IArrayFeature.CreateArray<TElement>(Int32 length, TElement initialElement)
		=> this.ThrowInvalidResult<JArrayObject<TElement>>();
	Int32 IArrayFeature.GetArrayLength(JReferenceObject jObject)
	{
		this.GetArrayLengthTrace(jObject);
		return 0;
	}
	TElement? IArrayFeature.GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index) where TElement : default
		=> this.ThrowInvalidResult<TElement?>();
	void IArrayFeature.SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
		where TElement : default
		=> this.ThrowInvalidResult<Byte>();
	Int32 IArrayFeature.IndexOf<TElement>(JArrayObject<TElement> jArray, TElement? item) where TElement : default
		=> this.ThrowInvalidResult<Int32>();
	void IArrayFeature.CopyTo<TElement>(JArrayObject<TElement> jArray, TElement?[] array, Int32 arrayIndex)
		where TElement : default
		=> this.ThrowInvalidResult<Byte>();
	void IArrayFeature.GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Span<TPrimitive> elements, Int32 startIndex)
		=> this.ThrowInvalidResult<Byte>();
	void IArrayFeature.SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlySpan<TPrimitive> elements,
		Int32 startIndex)
		=> this.ThrowInvalidResult<Byte>();
	INativeMemoryAdapter IArrayFeature.GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind)
		=> this.ThrowInvalidResult<INativeMemoryAdapter>();
	INativeMemoryAdapter IArrayFeature.GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind)
		=> this.ThrowInvalidResult<INativeMemoryAdapter>();
	IntPtr IArrayFeature.GetPrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, out Boolean isCopy)
	{
		Unsafe.SkipInit(out isCopy);
		return this.ThrowInvalidResult<IntPtr>();
	}
	ValPtr<Byte> IArrayFeature.GetPrimitiveCriticalSequence(JArrayLocalRef arrayRef)
		=> this.ThrowInvalidResult<ValPtr<Byte>>();
	void IArrayFeature.ReleasePrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, IntPtr pointer, JReleaseMode mode)
		=> this.ReleasePrimitiveSequenceTrace(arrayRef, pointer);
	void IArrayFeature.ReleasePrimitiveCriticalSequence(JArrayLocalRef arrayRef, ValPtr<Byte> criticalPtr)
		=> this.ReleasePrimitiveCriticalSequenceTrace(arrayRef, criticalPtr);
}
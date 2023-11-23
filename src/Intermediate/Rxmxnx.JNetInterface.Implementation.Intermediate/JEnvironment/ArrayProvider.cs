namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IArrayProvider
	{
		public Int32 GetArrayLength(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			GetArrayLengthDelegate getArrayLength = this.GetDelegate<GetArrayLengthDelegate>();
			return getArrayLength(this.Reference, jObject.As<JArrayLocalRef>());
		}
		public TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
			where TElement : IDataType<TElement>
		{
			JDataTypeMetadata metadata = IDataType.GetMetadata<TElement>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata) throw new NotImplementedException();
			throw new NotImplementedException();
		}
		public void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
			where TElement : IDataType<TElement>
		{
			throw new NotImplementedException();
		}
		public IntPtr GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, out Boolean isCopy)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
			=> throw new NotImplementedException();
		public IntPtr GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
			=> throw new NotImplementedException();
		public void ReleaseSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, IntPtr pointer, JReleaseMode mode)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			throw new NotImplementedException();
		}
		public void ReleaseCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, IntPtr pointer)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			throw new NotImplementedException();
		}
		public void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Int32 startIndex, Span<Char> elements)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			throw new NotImplementedException();
		}
		public void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Span<Char> elements, Int32 startIndex = 0)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			throw new NotImplementedException();
		}
	}
}
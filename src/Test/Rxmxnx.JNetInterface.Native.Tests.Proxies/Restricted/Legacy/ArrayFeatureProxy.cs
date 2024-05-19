namespace Rxmxnx.JNetInterface.Tests.Restricted;

public partial class ArrayFeatureProxy
{
	void IArrayFeature.GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Span<TPrimitive> elements, Int32 startIndex)
		=> elements.WithSafeFixed((this, jArray, startIndex), ArrayFeatureProxy.GetCopy);
	void IArrayFeature.SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlySpan<TPrimitive> elements,
		Int32 startIndex)
		=> elements.WithSafeFixed((this, jArray, startIndex), ArrayFeatureProxy.SetCopy);

	private static void GetCopy<TPrimitive>(in IFixedContext<TPrimitive> mem,
		(ArrayFeatureProxy feature, JArrayObject<TPrimitive> jArray, Int32 startIndex) args)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> args.feature.GetCopy(args.jArray, mem, args.startIndex);
	private static void SetCopy<TPrimitive>(in IReadOnlyFixedContext<TPrimitive> mem,
		(ArrayFeatureProxy feature, JArrayObject<TPrimitive> jArray, Int32 startIndex) args)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> args.feature.SetCopy(args.jArray, mem, args.startIndex);
}
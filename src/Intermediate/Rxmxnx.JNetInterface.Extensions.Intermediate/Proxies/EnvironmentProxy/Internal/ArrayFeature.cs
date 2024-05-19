namespace Rxmxnx.JNetInterface.Proxies;

public partial class EnvironmentProxy
{
	void IArrayFeature.GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Span<TPrimitive> elements, Int32 startIndex)
		=> elements.WithSafeFixed((this, jArray, startIndex), EnvironmentProxy.GetCopy);

	void IArrayFeature.SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlySpan<TPrimitive> elements,
		Int32 startIndex)
		=> elements.WithSafeFixed((this, jArray, startIndex), EnvironmentProxy.SetCopy);

	private static void GetCopy<TPrimitive>(in IFixedContext<TPrimitive> ctx,
		(EnvironmentProxy proxy, JArrayObject<TPrimitive> jArray, Int32 startIndex) arg)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> arg.proxy.GetCopy(arg.jArray, ctx, arg.startIndex);
	private static void SetCopy<TPrimitive>(in IReadOnlyFixedContext<TPrimitive> ctx,
		(EnvironmentProxy proxy, JArrayObject<TPrimitive> jArray, Int32 startIndex) arg)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> arg.proxy.SetCopy(arg.jArray, ctx, arg.startIndex);
}
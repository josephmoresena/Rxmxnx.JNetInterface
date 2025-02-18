namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	JStringObject IStringFeature.Create(ReadOnlySpan<Char> data, String? value)
		=> this.Create(value ?? data.ToString());
	JStringObject IStringFeature.Create(ReadOnlySpan<Byte> utf8Data) => this.Create(new CString(utf8Data));
	void IStringFeature.GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex)
		=> chars.WithSafeFixed((this, jString, startIndex), EnvironmentProxy.GetCopy);
	void IStringFeature.GetUtf8Copy(JStringObject jString, Span<Byte> utf8Units, Int32 startIndex)
		=> utf8Units.WithSafeFixed((this, jString, startIndex), EnvironmentProxy.GetUtf8Copy);

	private static void GetCopy(in IFixedContext<Char> ctx,
		(EnvironmentProxy proxy, JStringObject jString, Int32 startIndex) arg)
		=> arg.proxy.GetCopy(arg.jString, ctx, arg.startIndex);
	private static void GetUtf8Copy(in IFixedContext<Byte> ctx,
		(EnvironmentProxy proxy, JStringObject jString, Int32 startIndex) arg)
		=> arg.proxy.GetUtf8Copy(arg.jString, ctx, arg.startIndex);
}
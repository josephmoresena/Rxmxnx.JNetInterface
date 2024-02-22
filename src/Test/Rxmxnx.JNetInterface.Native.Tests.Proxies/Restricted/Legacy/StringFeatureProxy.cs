namespace Rxmxnx.JNetInterface.Tests.Restricted;

public partial class StringFeatureProxy
{
	void IStringFeature.GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex)
		=> chars.WithSafeFixed((this, jString, startIndex), StringFeatureProxy.GetCopy);
	JStringObject IStringFeature.Create(ReadOnlySpan<Char> data) => this.Create(data.ToString());
	JStringObject IStringFeature.Create(ReadOnlySpan<Byte> utf8Data) => this.Create(new CString(utf8Data));

	private static void GetCopy(in IFixedContext<Char> mem,
		(StringFeatureProxy feature, JStringObject jString, Int32 startIndex) args)
		=> args.feature.GetCopy(args.jString, mem, args.startIndex);
}
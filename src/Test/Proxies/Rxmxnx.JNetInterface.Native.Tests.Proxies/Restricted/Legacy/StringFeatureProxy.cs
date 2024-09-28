namespace Rxmxnx.JNetInterface.Tests.Restricted;

public partial class StringFeatureProxy
{
	void IStringFeature.GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex)
		=> chars.WithSafeFixed((this, jString, startIndex), StringFeatureProxy.GetCopy);
	void IStringFeature.GetUtf8Copy(JStringObject jString, Span<Byte> utf8Units, Int32 startIndex)
		=> utf8Units.WithSafeFixed((this, jString, startIndex), StringFeatureProxy.GetUtf8Copy);
	JStringObject IStringFeature.Create(ReadOnlySpan<Char> data) => this.Create(data.ToString());
	JStringObject IStringFeature.Create(ReadOnlySpan<Byte> utf8Data) => this.Create(new CString(utf8Data));

	private static void GetCopy(in IFixedContext<Char> mem,
		(StringFeatureProxy feature, JStringObject jString, Int32 startIndex) args)
		=> args.feature.GetCopy(args.jString, mem, args.startIndex);
	private static void GetUtf8Copy(in IFixedContext<Byte> mem,
		(StringFeatureProxy feature, JStringObject jString, Int32 startIndex) args)
		=> args.feature.GetUtf8Copy(args.jString, mem, args.startIndex);
}
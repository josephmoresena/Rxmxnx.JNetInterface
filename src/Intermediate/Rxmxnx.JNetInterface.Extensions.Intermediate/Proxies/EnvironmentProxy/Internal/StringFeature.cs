namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	JStringObject IStringFeature.Create(ReadOnlySpan<Char> data) => this.Create(data.ToString());
	JStringObject IStringFeature.Create(ReadOnlySpan<Byte> utf8Data) => this.Create(new CString(utf8Data));
}
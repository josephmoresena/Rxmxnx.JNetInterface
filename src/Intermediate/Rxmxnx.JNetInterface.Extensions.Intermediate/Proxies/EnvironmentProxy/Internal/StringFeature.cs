namespace Rxmxnx.JNetInterface.Native.Dummies;

public abstract partial class EnvironmentProxy
{
	JStringObject IStringFeature.Create(ReadOnlySpan<Char> data) => this.Create(data.ToString());
	JStringObject IStringFeature.Create(ReadOnlySpan<Byte> data) => this.Create(new CString(data));
}
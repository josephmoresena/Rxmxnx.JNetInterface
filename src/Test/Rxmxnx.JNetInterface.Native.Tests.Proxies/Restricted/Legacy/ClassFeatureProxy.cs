namespace Rxmxnx.JNetInterface.Tests.Restricted;

public partial class ClassFeatureProxy : IClassFeature
{
	JClassObject IClassFeature.GetClass(ReadOnlySpan<Byte> className) => this.GetClass(new CString(className));
	JClassObject IClassFeature.LoadClass(CString className, ReadOnlySpan<Byte> rawClassBytes,
		JClassLoaderObject? jClassLoader)
		=> this.LoadClass(new(className), rawClassBytes.ToArray(), jClassLoader);
	JClassObject IClassFeature.LoadClass<TDataType>(ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader)
		=> this.LoadClass<TDataType>(rawClassBytes.ToArray(), jClassLoader);
}
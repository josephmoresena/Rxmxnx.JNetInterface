namespace Rxmxnx.JNetInterface.Tests.Restricted;

public partial class ClassFeatureProxy
{
	JClassObject IClassFeature.GetClass(ReadOnlySpan<Byte> className) => this.GetClass(new CString(className));
	JClassObject IClassFeature.LoadClass(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> rawClassBytes,
		JClassLoaderObject? jClassLoader)
		=> this.LoadClass(new(className), rawClassBytes.ToArray(), jClassLoader);
	JClassObject IClassFeature.LoadClass<TDataType>(ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader)
		=> this.LoadClass<TDataType>(rawClassBytes.ToArray(), jClassLoader);
	void IClassFeature.GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
	{
		ITypeInformation information = this.GetClassInfo(jClass);
		name = information.ClassName;
		signature = information.Signature;
		hash = information.Hash;
	}
}
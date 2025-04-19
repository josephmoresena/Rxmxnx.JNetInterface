namespace Rxmxnx.JNetInterface.Tests.Restricted;

public partial class ClassFeatureProxy
{
	JClassObject IClassFeature.ClassObject => this.GetClass<JClassObject>();
	JClassObject IClassFeature.ThrowableObject => this.GetClass<JThrowableObject>();
	JClassObject IClassFeature.StackTraceElementObject => this.GetClass<JStackTraceElementObject>();

	JClassObject IClassFeature.GetClass(ReadOnlySpan<Byte> className) => this.GetClass(new CString(className));
	JClassObject IClassFeature.LoadClass(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> rawClassBytes,
		JClassLoaderObject? jClassLoader)
		=> this.LoadClass(new(className), rawClassBytes.ToArray(), jClassLoader);
	JClassObject IClassFeature.LoadClass<TDataType>(ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader)
		=> this.LoadClass<TDataType>(rawClassBytes.ToArray(), jClassLoader);
	void IClassFeature.GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
	{
		TypeInformationProxy? information = this.GetClassInfo(jClass);
		name = information?.ClassName!;
		signature = information?.Signature!;
		hash = information?.Hash!;
	}
	JClassObject IClassFeature.GetClass<TDataType>()
		=> !this.UseNonGeneric ? this.GetClass<TDataType>() : this.GetNonGenericClass(typeof(TDataType));
}
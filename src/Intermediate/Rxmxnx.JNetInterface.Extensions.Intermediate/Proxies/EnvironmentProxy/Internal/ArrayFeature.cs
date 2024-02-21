namespace Rxmxnx.JNetInterface.Native.Proxies;

public abstract partial class EnvironmentProxy
{
	void IArrayFeature.SetObjectElement(JArrayObject jArray, Int32 index, JReferenceObject? value)
	{
		JArrayTypeMetadata arrayMetadata = jArray.TypeMetadata;
		arrayMetadata.SetObjectElement(jArray, index, value);
	}
}
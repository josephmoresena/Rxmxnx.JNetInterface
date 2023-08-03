namespace Rxmxnx.JNetInterface.Native;

public abstract partial class JInterfaceObject
{
	/// <summary>
	/// <see cref="JReferenceTypeMetadata"/> class builder.
	/// </summary>
	/// <typeparam name="TInterface">Type of interface.</typeparam>
	protected new sealed class JTypeMetadataBuilder<TInterface>
		where TInterface : JInterfaceObject, IInterfaceType<TInterface> { }
}
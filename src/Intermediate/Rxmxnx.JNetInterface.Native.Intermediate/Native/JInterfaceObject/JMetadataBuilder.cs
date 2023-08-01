namespace Rxmxnx.JNetInterface.Native;

public abstract partial class JInterfaceObject
{
	/// <summary>
	/// <see cref="JReferenceMetadata"/> class builder.
	/// </summary>
	/// <typeparam name="TInterface">Type of interface.</typeparam>
	protected new sealed class JMetadataBuilder<TInterface>
		where TInterface : JInterfaceObject, IInterfaceType<TInterface> { }
}
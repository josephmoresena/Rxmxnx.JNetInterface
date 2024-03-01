namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents an interface instance.
/// </summary>
/// <typeparam name="TInterface">Type of <see cref="IInterfaceType"/>.</typeparam>
public abstract class JInterfaceObject<TInterface> : JLocalObject.InterfaceView
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	/// <inheritdoc/>
	protected JInterfaceObject(IReferenceType.ObjectInitializer initializer) : base(initializer.Instance) { }
}
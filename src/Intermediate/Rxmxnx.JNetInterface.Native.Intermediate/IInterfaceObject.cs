namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an interface instance.
/// </summary>
[UnconditionalSuppressMessage("Trimming", "IL2091")]
public interface IInterfaceObject<TInterface> : ILocalObject
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>;
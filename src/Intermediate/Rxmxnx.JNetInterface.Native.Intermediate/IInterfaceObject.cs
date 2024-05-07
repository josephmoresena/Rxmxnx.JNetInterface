namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an interface instance.
/// </summary>
public interface IInterfaceObject<TInterface> : IObject
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>;
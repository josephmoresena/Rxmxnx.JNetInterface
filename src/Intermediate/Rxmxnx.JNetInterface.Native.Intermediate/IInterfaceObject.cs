namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an interface instance.
/// </summary>
#if !NET8_0_OR_GREATER
[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
public interface IInterfaceObject<TInterface> : ILocalObject
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>;
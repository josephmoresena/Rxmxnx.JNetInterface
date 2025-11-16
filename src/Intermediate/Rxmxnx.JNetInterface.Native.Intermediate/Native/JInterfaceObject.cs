namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents an interface instance.
/// </summary>
/// <typeparam name="TInterface">Type of <see cref="IInterfaceType"/>.</typeparam>
#if !NET8_0_OR_GREATER
[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public abstract class JInterfaceObject<TInterface> : JLocalObject.InterfaceView
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	/// <inheritdoc/>
	protected JInterfaceObject(IReferenceType.ObjectInitializer initializer) : base(initializer.Instance) { }
}
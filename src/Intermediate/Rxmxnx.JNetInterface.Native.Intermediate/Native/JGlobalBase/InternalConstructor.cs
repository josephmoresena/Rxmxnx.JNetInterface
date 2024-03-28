namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="globalRef">Global object reference.</param>
	private protected JGlobalBase(ILocalObject jLocal, JGlobalRef globalRef) : this(
		jLocal.VirtualMachine, ILocalObject.CreateMetadata(jLocal), jLocal.IsProxy, globalRef.Pointer) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="weakRef">Weak global object reference.</param>
	private protected JGlobalBase(ILocalObject jLocal, JWeakRef weakRef) : this(
		jLocal.VirtualMachine, ILocalObject.CreateMetadata(jLocal), jLocal.IsProxy, weakRef.Pointer) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="Native.ObjectMetadata"/> instance.</param>
	/// <param name="isProxy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="globalRef">Global reference.</param>
	private protected JGlobalBase(IVirtualMachine vm, ObjectMetadata metadata, Boolean isProxy, JGlobalRef globalRef) :
		this(vm, metadata, isProxy, globalRef.Pointer) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="Native.ObjectMetadata"/> instance.</param>
	/// <param name="isProxy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="weakRef">Weak global reference.</param>
	private protected JGlobalBase(IVirtualMachine vm, ObjectMetadata metadata, Boolean isProxy, JWeakRef weakRef) :
		this(vm, metadata, isProxy, weakRef.Pointer) { }
}
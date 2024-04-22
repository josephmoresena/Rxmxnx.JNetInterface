namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="globalRef">Global object reference.</param>
	private protected JGlobalBase(ILocalObject jLocal, JGlobalRef globalRef) : this(
		jLocal.VirtualMachine, ILocalObject.CreateMetadata(jLocal), globalRef.Pointer) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="weakRef">Weak global object reference.</param>
	private protected JGlobalBase(ILocalObject jLocal, JWeakRef weakRef) : this(
		jLocal.VirtualMachine, ILocalObject.CreateMetadata(jLocal), weakRef.Pointer) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="Native.ObjectMetadata"/> instance.</param>
	/// <param name="globalRef">Global reference.</param>
	private protected JGlobalBase(IVirtualMachine vm, ObjectMetadata metadata, JGlobalRef globalRef) : this(
		vm, metadata, globalRef.Pointer) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="Native.ObjectMetadata"/> instance.</param>
	/// <param name="weakRef">Weak global reference.</param>
	private protected JGlobalBase(IVirtualMachine vm, ObjectMetadata metadata, JWeakRef weakRef) : this(
		vm, metadata, weakRef.Pointer) { }
}
namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="weakRef">Weak global object reference.</param>
	internal JGlobalBase(ILocalObject jLocal, JWeakRef weakRef) : base(jLocal.IsProxy)
	{
		this.VirtualMachine = jLocal.VirtualMachine;
		this._value = IMutableReference.Create(weakRef.Pointer);
		this.ObjectMetadata = ILocalObject.CreateMetadata(jLocal);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="Native.ObjectMetadata"/> instance.</param>
	/// <param name="isProxy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="globalRef">Global reference.</param>
	internal JGlobalBase(IVirtualMachine vm, ObjectMetadata metadata, Boolean isProxy, JGlobalRef globalRef) :
		base(isProxy)
	{
		this.VirtualMachine = vm;
		this._value = IMutableReference.Create(globalRef.Pointer);
		this.ObjectMetadata = metadata;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="Native.ObjectMetadata"/> instance.</param>
	/// <param name="isProxy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="weakRef">Weak global reference.</param>
	internal JGlobalBase(IVirtualMachine vm, ObjectMetadata metadata, Boolean isProxy, JWeakRef weakRef) : base(isProxy)
	{
		this.VirtualMachine = vm;
		this._value = IMutableReference.Create(weakRef.Pointer);
		this.ObjectMetadata = metadata;
	}
}
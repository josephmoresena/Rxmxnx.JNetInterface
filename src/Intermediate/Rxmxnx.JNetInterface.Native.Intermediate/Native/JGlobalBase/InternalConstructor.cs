namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="globalRef">Global object reference.</param>
	internal JGlobalBase(ILocalObject jLocal, JGlobalRef globalRef) : base(globalRef, jLocal.IsDummy)
	{
		this._vm = jLocal.VirtualMachine;
		this._objectMetadata = ILocalObject.CreateMetadata(jLocal);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="weakRef">Weak global object reference.</param>
	internal JGlobalBase(ILocalObject jLocal, JWeakRef weakRef) : base(weakRef, jLocal.IsDummy)
	{
		this._vm = jLocal.VirtualMachine;
		this._objectMetadata = ILocalObject.CreateMetadata(jLocal);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="globalRef">Global reference.</param>
	internal JGlobalBase(IVirtualMachine vm, JObjectMetadata metadata, Boolean isDummy, JGlobalRef globalRef) : base(
		globalRef, isDummy)
	{
		this._vm = vm;
		this._objectMetadata = metadata;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="weakRef">Weak global reference.</param>
	internal JGlobalBase(IVirtualMachine vm, JObjectMetadata metadata, Boolean isDummy, JWeakRef weakRef) : base(
		weakRef, isDummy)
	{
		this._vm = vm;
		this._objectMetadata = metadata;
	}
}
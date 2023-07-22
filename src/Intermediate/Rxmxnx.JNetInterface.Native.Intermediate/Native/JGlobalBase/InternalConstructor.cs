namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JGlobalBase(IVirtualMachine vm, Boolean isDummy) : base(isDummy) => this._vm = vm;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="globalRef">Global object reference.</param>
	internal JGlobalBase(JLocalObject jLocal, JGlobalRef globalRef) : base(globalRef, jLocal.IsDummy)
		=> this._vm = jLocal.Environment.VirtualMachine;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="weakRef">Weak global object reference.</param>
	internal JGlobalBase(JLocalObject jLocal, JWeakRef weakRef) : base(weakRef, jLocal.IsDummy)
		=> this._vm = jLocal.Environment.VirtualMachine;
}
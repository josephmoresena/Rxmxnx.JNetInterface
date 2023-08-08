namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="globalRef">Global object reference.</param>
	internal JGlobalBase(JLocalObject jLocal, JGlobalRef globalRef) : base(globalRef, jLocal.IsDummy)
	{
		this._vm = jLocal.Environment.VirtualMachine;
		this._objectMetadata = ILocalObject.CreateMetadata(jLocal);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="weakRef">Weak global object reference.</param>
	internal JGlobalBase(JLocalObject jLocal, JWeakRef weakRef) : base(weakRef, jLocal.IsDummy)
	{
		this._vm = jLocal.Environment.VirtualMachine;
		this._objectMetadata = ILocalObject.CreateMetadata(jLocal);
	}
}
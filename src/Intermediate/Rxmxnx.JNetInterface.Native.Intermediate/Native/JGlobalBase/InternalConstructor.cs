namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="weakRef">Weak global object reference.</param>
	internal JGlobalBase(ILocalObject jLocal, JWeakRef weakRef) : base(jLocal.IsDummy)
	{
		this._vm = jLocal.VirtualMachine;
		this._value = IMutableReference.Create(NativeUtilities.Transform<JWeakRef, IntPtr>(in weakRef));
		this._objectMetadata = ILocalObject.CreateMetadata(jLocal);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="globalRef">Global reference.</param>
	internal JGlobalBase(IVirtualMachine vm, JObjectMetadata metadata, Boolean isDummy, JGlobalRef globalRef) :
		base(isDummy)
	{
		this._vm = vm;
		this._value = IMutableReference.Create(NativeUtilities.Transform<JGlobalRef, IntPtr>(in globalRef));
		this._objectMetadata = metadata;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="weakRef">Weak global reference.</param>
	internal JGlobalBase(IVirtualMachine vm, JObjectMetadata metadata, Boolean isDummy, JWeakRef weakRef) :
		base(isDummy)
	{
		this._vm = vm;
		this._value = IMutableReference.Create(NativeUtilities.Transform<JWeakRef, IntPtr>(in weakRef));
		this._objectMetadata = metadata;
	}
}
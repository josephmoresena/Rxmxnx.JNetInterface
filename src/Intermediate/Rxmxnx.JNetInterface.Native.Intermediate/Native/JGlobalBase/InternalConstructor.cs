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
		this.VirtualMachine = jLocal.VirtualMachine;
		this._value = IMutableReference.Create(NativeUtilities.Transform<JWeakRef, IntPtr>(in weakRef));
		this.ObjectMetadata = ILocalObject.CreateMetadata(jLocal);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="Native.ObjectMetadata"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="globalRef">Global reference.</param>
	internal JGlobalBase(IVirtualMachine vm, ObjectMetadata metadata, Boolean isDummy, JGlobalRef globalRef) :
		base(isDummy)
	{
		this.VirtualMachine = vm;
		this._value = IMutableReference.Create(NativeUtilities.Transform<JGlobalRef, IntPtr>(in globalRef));
		this.ObjectMetadata = metadata;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="Native.ObjectMetadata"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="weakRef">Weak global reference.</param>
	internal JGlobalBase(IVirtualMachine vm, ObjectMetadata metadata, Boolean isDummy, JWeakRef weakRef) : base(isDummy)
	{
		this.VirtualMachine = vm;
		this._value = IMutableReference.Create(NativeUtilities.Transform<JWeakRef, IntPtr>(in weakRef));
		this.ObjectMetadata = metadata;
	}
}
namespace Rxmxnx.JNetInterface.Native;

internal class JGlobalObject : JGlobal
{
	public JGlobalObject(ILocalObject jLocal, JGlobalRef globalRef) : base(jLocal, globalRef) { }
	public JGlobalObject(IVirtualMachine vm, JObjectMetadata metadata, Boolean isDummy, JGlobalRef globalRef) : base(
		vm, metadata, isDummy, globalRef) { }

	internal override Boolean JniSecure()
		=> (this.VirtualMachine.GetEnvironment() as JEnvironment)?.JniSecure() ?? false;
}
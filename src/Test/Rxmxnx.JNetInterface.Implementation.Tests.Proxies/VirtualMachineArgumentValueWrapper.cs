namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public readonly unsafe struct VirtualMachineArgumentValueWrapper
{
	private readonly VirtualMachineArgumentValue _value;

	public JGlobalRef Group => this._value.Group;
	public Int32 Version => this._value.Version;
	public IntPtr NamePtr => (IntPtr)this._value.Name;
}
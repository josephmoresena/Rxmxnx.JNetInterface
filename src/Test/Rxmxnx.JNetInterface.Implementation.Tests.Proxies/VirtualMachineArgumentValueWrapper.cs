namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public readonly unsafe struct VirtualMachineArgumentValueWrapper
{
#pragma warning disable CS0649
	private readonly VirtualMachineArgumentValue _value;
#pragma warning restore CS0649

	public JGlobalRef Group => this._value.Group;
	public Int32 Version => this._value.Version;
	public IntPtr NamePtr => (IntPtr)this._value.Name;
}
namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public readonly struct VirtualMachineArgumentValueWrapper : IWrapper<VirtualMachineArgumentValue>
{
	internal VirtualMachineArgumentValue Value { get; init; }

	VirtualMachineArgumentValue IWrapper<VirtualMachineArgumentValue>.Value => this.Value;
}
namespace Rxmxnx.JNetInterface.Tests;

public readonly struct NativeMethodValueWrapper : IWrapper<NativeMethodValue>
{
	internal NativeMethodValue Value { get; init; }

	NativeMethodValue IWrapper<NativeMethodValue>.Value => this.Value;
}
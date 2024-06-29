namespace Rxmxnx.JNetInterface.Tests;

public readonly struct JValueWrapper : IWrapper<JValue>
{
	internal JValue Value { get; init; }

	JValue IWrapper<JValue>.Value => this.Value;
}
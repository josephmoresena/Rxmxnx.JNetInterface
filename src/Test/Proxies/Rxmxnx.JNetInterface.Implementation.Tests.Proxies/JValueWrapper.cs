namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public readonly struct JValueWrapper : IWrapper<JValue>
{
	internal JValue Value { get; init; }

	JValue IWrapper<JValue>.Value => this.Value;
}
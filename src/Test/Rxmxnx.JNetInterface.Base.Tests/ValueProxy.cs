namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public readonly record struct ValueProxy
{
	internal JValue Value { get; init; }
}
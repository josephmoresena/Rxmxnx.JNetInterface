namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public readonly record struct ValueProxy
{
	// ReSharper disable once DefaultStructEqualityIsUsed.Global
	internal JValue Value { get; init; }
}
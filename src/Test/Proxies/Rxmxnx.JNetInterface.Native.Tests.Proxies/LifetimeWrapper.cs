namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed record LifetimeWrapper : IWrapper<ObjectLifetime>
{
	internal ObjectLifetime Value { get; init; } = default!;

	ObjectLifetime IWrapper<ObjectLifetime>.Value => this.Value;
}
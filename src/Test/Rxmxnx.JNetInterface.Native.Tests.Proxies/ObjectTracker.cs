namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ObjectTracker
{
	public WeakReference WeakReference { get; init; } = new(null);
	public IWrapper<Boolean>? FinalizerFlag { get; init; }
}
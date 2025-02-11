namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

[ExcludeFromCodeCoverage]
public readonly struct ExecuteState
{
	public String ExecutablePath { get; init; }
	public String? WorkingDirectory { get; init; }
	public Action<Collection<String>> AppendArgs { get; init; }
	public IExecutionNotifier? Notifier { get; init; }
}

[ExcludeFromCodeCoverage]
public readonly struct ExecuteState<TState>
{
	public String ExecutablePath { get; init; }
	public String? WorkingDirectory { get; init; }
	public TState ArgState { get; init; }
	public Action<TState, Collection<String>> AppendArgs { get; init; }
	public IExecutionNotifier? Notifier { get; init; }
}
namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public readonly struct QemuExecuteState
{
	public String QemuExecutable { get; init; }
	public String QemuRoot { get; init; }
	public String ExecutablePath { get; init; }
	public String? WorkingDirectory { get; init; }
	public Action<Collection<String>> AppendArgs { get; init; }
	public IExecutionNotifier? Notifier { get; init; }
}

public readonly struct QemuExecuteState<TState>
{
	public String QemuExecutable { get; init; }
	public String QemuRoot { get; init; }
	public String ExecutablePath { get; init; }
	public String? WorkingDirectory { get; init; }
	public TState ArgState { get; init; }
	public Action<TState, Collection<String>> AppendArgs { get; init; }
	public IExecutionNotifier? Notifier { get; init; }
}
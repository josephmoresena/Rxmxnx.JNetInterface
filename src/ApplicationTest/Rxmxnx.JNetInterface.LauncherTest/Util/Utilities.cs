using System.Diagnostics;

namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public static class Utilities
{
	public static async Task DownloadFileAsync(DownloadGetState state, CancellationToken cancellationToken = default)
	{
		using HttpClient httpClient = new();
		using HttpResponseMessage response =
			await httpClient.GetAsync(state.Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

		response.EnsureSuccessStatusCode();

		Int64? size = response.Content.Headers.ContentLength;
		Int64 current = 0;

		state.Notifier?.Begin(state.Url, size);
		File.Delete(state.Destination);

		await using FileStream fs = File.Create(state.Destination);
		Task copyTask = response.Content.CopyToAsync(fs, cancellationToken);

		if (state.Notifier is null)
		{
			await copyTask;
			return;
		}

		while (!copyTask.IsCompleted)
		{
			Int64 previous = current;
			await Task.WhenAny(Task.Delay(state.Notifier.RefreshTime, cancellationToken), copyTask);
			if (copyTask.IsCompleted) break;
			current = fs.Position;
			if (previous != current)
				state.Notifier.Progress(state.Url, size, current);
		}

		state.Notifier.End(state.Url, fs.Position, state.Destination);
	}
	public static async Task Execute(ExecuteState state, CancellationToken cancellationToken = default)
	{
		ProcessStartInfo info = new(state.ExecutablePath)
		{
			RedirectStandardError = true, RedirectStandardOutput = true, CreateNoWindow = false,
		};
		state.AppendArgs(info.ArgumentList);
		if (!String.IsNullOrEmpty(state.WorkingDirectory)) info.WorkingDirectory = state.WorkingDirectory;
		state.Notifier?.Begin(info);
		using Process prog = Process.Start(info)!;
		await prog.WaitForExitAsync(cancellationToken);
		state.Notifier?.End(info);
	}
	public static async Task Execute<TState>(ExecuteState<TState> state, CancellationToken cancellationToken = default)
	{
		ProcessStartInfo info = new(state.ExecutablePath)
		{
			RedirectStandardError = true, RedirectStandardOutput = true, CreateNoWindow = false,
		};
		state.AppendArgs(state.ArgState, info.ArgumentList);
		if (!String.IsNullOrEmpty(state.WorkingDirectory)) info.WorkingDirectory = state.WorkingDirectory;
		state.Notifier?.Begin(info);
		using Process prog = Process.Start(info)!;
		await prog.WaitForExitAsync(cancellationToken);
		state.Notifier?.End(info);
	}
	public static async Task QemuExecute(QemuExecuteState state, CancellationToken cancellationToken = default)
	{
		ProcessStartInfo info = new(state.QemuExecutable)
		{
			ArgumentList = { "-L", state.QemuRoot, state.ExecutablePath, },
			RedirectStandardError = true,
			RedirectStandardOutput = true,
			CreateNoWindow = false,
		};
		state.AppendArgs(info.ArgumentList);
		if (!String.IsNullOrEmpty(state.WorkingDirectory)) info.WorkingDirectory = state.WorkingDirectory;
		state.Notifier?.Begin(info);
		using Process prog = Process.Start(info)!;
		await prog.WaitForExitAsync(cancellationToken);
		state.Notifier?.End(info);
	}
	public static async Task QemuExecute<TState>(QemuExecuteState<TState> state,
		CancellationToken cancellationToken = default)
	{
		ProcessStartInfo info = new(state.QemuExecutable)
		{
			ArgumentList = { "-L", state.QemuRoot, state.ExecutablePath, },
			RedirectStandardError = true,
			RedirectStandardOutput = true,
			CreateNoWindow = false,
		};
		state.AppendArgs(state.ArgState, info.ArgumentList);
		if (!String.IsNullOrEmpty(state.WorkingDirectory)) info.WorkingDirectory = state.WorkingDirectory;
		state.Notifier?.Begin(info);
		using Process prog = Process.Start(info)!;
		await prog.WaitForExitAsync(cancellationToken);
		state.Notifier?.End(info);
	}
}
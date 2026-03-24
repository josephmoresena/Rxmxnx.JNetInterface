namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public static class Utilities
{
	private static readonly ConcurrentDictionary<Guid, Process> _processes = [];

	public static Boolean ShowDiagnostics
		=> Boolean.TryParse(Environment.GetEnvironmentVariable("SHOW_DIAGNOSTICS"), out Boolean showDiagnostics) &&
			showDiagnostics;
	public static Boolean IsNativeAotSupported(Architecture arch, NetVersion netVersion)
		=> netVersion >= NetVersion.Net70 && arch switch
		{
			Architecture.X64 => true,
			Architecture.Arm64 => netVersion >= NetVersion.Net80 || SystemInfo.IsWindows || SystemInfo.IsLinux,
			Architecture.X86 or Architecture.Arm or Architecture.Armv6 => netVersion >= NetVersion.Net90,
			_ => false,
		};
	public static Boolean IsReflectionFreeModeSupported(NetVersion netVersion) => netVersion < NetVersion.Net90;
	public static async Task DownloadFileAsync(DownloadGetState state, CancellationToken cancellationToken = default)
	{
		using HttpClient httpClient = new();

		httpClient.Timeout = TimeSpan.FromMinutes(5);

		using HttpResponseMessage response =
			await httpClient.GetAsync(state.Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

		try
		{
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

			Int32 cursorTop = -1;
			Int32 textLength = 0;

			while (!copyTask.IsCompleted)
			{
				Int64 previous = current;
				await Task.WhenAny(Task.Delay(state.Notifier.RefreshTime, cancellationToken), copyTask);
				if (copyTask.IsCompleted) break;
				current = fs.Position;
				if (previous != current)
					state.Notifier.Progress(state.Url, size, current, ref cursorTop, ref textLength);
			}

			state.Notifier.End(state.Url, fs.Position, state.Destination);
		}
		catch
		{
			state.Notifier?.Fail(state.Url);
			throw;
		}
	}
	public static async Task<Int32> Execute(ExecuteState state, CancellationToken cancellationToken = default)
	{
		ProcessStartInfo info = new(state.ExecutablePath)
		{
			RedirectStandardError = false, RedirectStandardOutput = false, CreateNoWindow = false,
		};
		state.AppendArgs(info.ArgumentList);
		if (!String.IsNullOrEmpty(state.WorkingDirectory)) info.WorkingDirectory = state.WorkingDirectory;
		state.Notifier?.Begin(info);
		Guid processId = Guid.CreateVersion7();
		using Process prog = Process.Start(info)!;
		Utilities._processes.TryAdd(processId, prog);
		try
		{
			await prog.WaitForExitAsync(cancellationToken);
			state.Notifier?.End(info);
			return prog.ExitCode;
		}
		finally
		{
			Utilities._processes.TryRemove(processId, out _);
		}
	}
	public static async Task<Int32> Execute<TState>(ExecuteState<TState> state,
		CancellationToken cancellationToken = default)
	{
		ProcessStartInfo info = new(state.ExecutablePath)
		{
			RedirectStandardError = false, RedirectStandardOutput = false, CreateNoWindow = false,
		};
		state.AppendArgs(state.ArgState, info.ArgumentList);
		if (!String.IsNullOrEmpty(state.WorkingDirectory)) info.WorkingDirectory = state.WorkingDirectory;
		state.Notifier?.Begin(info);
		Guid processId = Guid.CreateVersion7();
		using Process prog = Process.Start(info)!;
		Utilities._processes.TryAdd(processId, prog);
		try
		{
			await prog.WaitForExitAsync(cancellationToken);
			state.Notifier?.End(info);
			return prog.ExitCode;
		}
		finally
		{
			Utilities._processes.TryRemove(processId, out _);
		}
	}
	public static async Task<Int32> QemuExecute(QemuExecuteState state, CancellationToken cancellationToken = default)
	{
		ProcessStartInfo info = new(state.QemuExecutable)
		{
			ArgumentList = { "-L", state.QemuRoot, state.ExecutablePath, },
			RedirectStandardError = false,
			RedirectStandardOutput = false,
			CreateNoWindow = false,
		};
		state.AppendArgs(info.ArgumentList);
		if (!String.IsNullOrEmpty(state.WorkingDirectory)) info.WorkingDirectory = state.WorkingDirectory;
		state.Notifier?.Begin(info);
		Guid processId = Guid.CreateVersion7();
		using Process prog = Process.Start(info)!;
		Utilities._processes.TryAdd(processId, prog);
		try
		{
			await prog.WaitForExitAsync(cancellationToken);
			state.Notifier?.End(info);
			return prog.ExitCode;
		}
		finally
		{
			Utilities._processes.TryRemove(processId, out _);
		}
	}
	public static async Task<Int32> QemuExecute<TState>(QemuExecuteState<TState> state,
		CancellationToken cancellationToken = default)
	{
		ProcessStartInfo info = new(state.QemuExecutable)
		{
			ArgumentList = { "-L", state.QemuRoot, state.ExecutablePath, },
			RedirectStandardError = false,
			RedirectStandardOutput = false,
			CreateNoWindow = false,
		};
		state.AppendArgs(state.ArgState, info.ArgumentList);
		if (!String.IsNullOrEmpty(state.WorkingDirectory)) info.WorkingDirectory = state.WorkingDirectory;
		state.Notifier?.Begin(info);
		Guid processId = Guid.CreateVersion7();
		using Process prog = Process.Start(info)!;
		Utilities._processes.TryAdd(processId, prog);
		try
		{
			await prog.WaitForExitAsync(cancellationToken);
			state.Notifier?.End(info);
			return prog.ExitCode;
		}
		finally
		{
			Utilities._processes.TryRemove(processId, out _);
		}
	}
	public static async Task<String> ExecuteWithOutput(ExecuteState state,
		CancellationToken cancellationToken = default)
	{
		ProcessStartInfo info = new(state.ExecutablePath)
		{
			RedirectStandardError = true, RedirectStandardOutput = true, CreateNoWindow = true,
		};
		state.AppendArgs?.Invoke(info.ArgumentList);
		if (!String.IsNullOrEmpty(state.WorkingDirectory)) info.WorkingDirectory = state.WorkingDirectory;
		state.Notifier?.Begin(info);
		Guid processId = Guid.CreateVersion7();
		using Process prog = Process.Start(info)!;
		Utilities._processes.TryAdd(processId, prog);
		try
		{
			String result = await Utilities.ReadOutput(prog, cancellationToken);
			await prog.WaitForExitAsync(cancellationToken);
			state.Notifier?.End(info);
			return result;
		}
		finally
		{
			Utilities._processes.TryRemove(processId, out _);
		}
	}
	public static void KillRunningProcesses()
	{
		foreach (Guid processId in Utilities._processes.Keys.ToArray())
		{
			if (Utilities._processes.TryRemove(processId, out Process? process))
				process.Kill(true);
		}
	}

	private static async Task<String> ReadOutput(Process prog, CancellationToken cancellationToken)
	{
		OutputState state = new() { Builder = new(), Lock = new(), CancellationToken = cancellationToken, };
		await Task.WhenAll(Utilities.CopyOutput(state, prog.StandardOutput),
		                   Utilities.CopyOutput(state, prog.StandardError));
		return state.Builder.ToString();
	}
	private static async Task CopyOutput(OutputState state, StreamReader reader)
	{
		while (await reader.ReadLineAsync(state.CancellationToken) is { } line)
		{
			lock (state.Lock)
				state.Builder.AppendLine(line);
		}
	}

	private readonly struct OutputState
	{
		public StringBuilder Builder { get; init; }
		public Object Lock { get; init; }
		public CancellationToken CancellationToken { get; init; }
	}
}
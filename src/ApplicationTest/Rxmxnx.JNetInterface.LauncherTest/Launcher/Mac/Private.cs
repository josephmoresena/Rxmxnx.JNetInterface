using System.Runtime.InteropServices;
using System.Text;

using Rxmxnx.JNetInterface.ApplicationTest.Util;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	private sealed partial class Mac
	{
		private readonly Dictionary<Jdk.JdkVersion, Jdk> _amd64 = new();
		private readonly Dictionary<Jdk.JdkVersion, Jdk> _arm64 = new();

		private Mac(DirectoryInfo outputDirectory, out Task initialize) : base(outputDirectory, OSPlatform.OSX)
			=> initialize = this.Initialize();

		private async Task Initialize()
		{
			foreach (Jdk.JdkVersion version in Enum.GetValues<Jdk.JdkVersion>())
			{
				Task arm64Task = this.CurrentArch == Architecture.Arm64 ?
					this.AppendJdk(this._arm64, version, Architecture.Arm64) :
					Task.FromResult<Jdk?>(default);

				await this.AppendJdk(this._amd64, version, Architecture.X64);
				await arm64Task;
			}
		}

		private async Task AppendJdk(IDictionary<Jdk.JdkVersion, Jdk> jdks, Jdk.JdkVersion version, Architecture arch)
		{
			ConsoleNotifier.PlatformNotifier.JdkDetection(version, arch);
			if (await this.GetJdk(version, arch) is { } jdk)
				jdks.Add(version, jdk);
			else
				ConsoleNotifier.PlatformNotifier.JdkUnavailable(version, arch);
		}

		private static async Task ExtractFromDmgPkgAsync(String tempFileName, String jdkPath,
			CancellationToken cancellationToken = default)
		{
			DirectoryInfo mountDir = new(Path.Combine(Path.GetTempPath(), "jdk_1.6_dmg"));
			String fullName = new FileInfo(tempFileName).FullName;
			if (mountDir.Exists) mountDir.Delete(true);
			mountDir.Create();
			Task payLoadExtract;

			try
			{
				await Utilities.Execute<ValueTuple<String, String>>(new()
				{
					ExecutablePath = "hdiutil",
					ArgState = (fullName, mountDir.FullName),
					AppendArgs = (s, a) =>
					{
						a.Add("attach");
						a.Add(s.Item1);
						a.Add("-mountpoint");
						a.Add(s.Item2);
					},
					Notifier = ConsoleNotifier.Notifier,
				}, cancellationToken);

				String pkgFilePath = mountDir.GetFiles("*.pkg").First().FullName;

				payLoadExtract = await Mac.ExtractFromPkgAsyncTask(pkgFilePath, jdkPath, cancellationToken);

				// Attach .dmg
				await Utilities.Execute<String>(new()
				{
					ExecutablePath = "hdiutil",
					ArgState = mountDir.FullName,
					AppendArgs = (s, a) =>
					{
						a.Add("detach");
						a.Add(s);
					},
					Notifier = ConsoleNotifier.Notifier,
				}, cancellationToken);
			}
			finally
			{
				mountDir.Delete(true);
			}

			await payLoadExtract;
		}
		private static async Task<Task> ExtractFromPkgAsyncTask(String pkgFilePath, String jdkPath,
			CancellationToken cancellationToken)
		{
			DirectoryInfo extractDir = new(Path.Combine(Path.GetTempPath(), "jdk_1.6_pkg"));
			if (extractDir.Exists) extractDir.Delete(true);

			try
			{
				ExecuteState<ValueTuple<String, String>> state = new()
				{
					ExecutablePath = "pkgutil",
					ArgState = (pkgFilePath, extractDir.FullName),
					AppendArgs = (s, a) =>
					{
						a.Add("--expand");
						a.Add(s.Item1);
						a.Add(s.Item2);
					},
					Notifier = ConsoleNotifier.Notifier,
				};
				await Utilities.Execute(state, cancellationToken);
			}
			catch (Exception)
			{
				extractDir.Delete(true);
				throw;
			}

			return Mac.ExtractPayloadPkgAsync(jdkPath, extractDir);
		}
		private static async Task ExtractPayloadPkgAsync(String jdkPath, DirectoryInfo extractDir)
		{
			FileInfo? payloadFile = extractDir.GetFiles("Payload", SearchOption.AllDirectories).FirstOrDefault();
			try
			{
				if (payloadFile is null) return;
				ExecuteState<String> state = new()
				{
					ExecutablePath = "zsh",
					ArgState = payloadFile.FullName,
					WorkingDirectory = jdkPath,
					AppendArgs = (s, a) =>
					{
						StringBuilder command = new();
						command.Append("cat \"");
						command.Append(s);
						command.Append("\" | ");
						command.Append("gunzip | ");
						command.Append("cpio -i");

						a.Add("-c");
						a.Add(command.ToString());
					},
					Notifier = ConsoleNotifier.Notifier,
				};
				await Utilities.Execute(state);
			}
			finally
			{
				extractDir.Delete(true);
			}
		}
	}
}
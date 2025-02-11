namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	private partial class Windows
	{
		private readonly ConcurrentDictionary<JdkVersion, Jdk> _amd64 = new();
		private readonly ConcurrentDictionary<JdkVersion, Jdk> _arm64 = new();
		private readonly ConcurrentDictionary<JdkVersion, Jdk> _i686 = new();

		private Windows(DirectoryInfo outputDirectory, out Task initialize) : base(outputDirectory)
		{
			this.Architectures = Enum.GetValues<Architecture>()
			                         .Where(a => a == this.CurrentArch || a is Architecture.X86 ||
				                                (a is Architecture.X64 && this.CurrentArch is not Architecture.X86))
			                         .ToArray();
			initialize = this.Initialize();
		}

		private async Task Initialize()
		{
			List<Task> tasks = [];
			foreach (JdkVersion version in Enum.GetValues<JdkVersion>().AsSpan())
			{
				if (this.CurrentArch is Architecture.Arm64)
					tasks.Add(this.AppendJdk(this._arm64, version, Architecture.Arm64));
				if (this.CurrentArch is not Architecture.X86)
					tasks.Add(this.AppendJdk(this._amd64, version, Architecture.X64));
				tasks.Add(this.AppendJdk(this._i686, version, Architecture.X86));
			}
			await Task.WhenAll(tasks);
		}

		private static String? GetInstallationPath(Architecture arch, JdkVersion version)
		{
			if (arch is not Architecture.X86 and not Architecture.X64) return default;
			Environment.SpecialFolder programFilesFolder = arch is Architecture.X86 ?
				Environment.SpecialFolder.ProgramFilesX86 :
				Environment.SpecialFolder.ProgramFiles;
			DirectoryInfo programFilesDir = new(Windows.GetProgramFilesPath(programFilesFolder));
			String versionPattern = version switch
			{
				JdkVersion.Jdk6 => "1.6",
				JdkVersion.Jdk8 => "1.8",
				_ => $"{(Byte)version}",
			};

			return programFilesDir.Exists ?
				programFilesDir.GetDirectories($"*jdk*{versionPattern}*", SearchOption.AllDirectories)
				               .FirstOrDefault(d => Windows.IsJdkDirectory(d.Name, versionPattern))?.FullName :
				default;
		}
		private static Boolean IsJdkDirectory(ReadOnlySpan<Char> directoryName, ReadOnlySpan<Char> versionPattern)
		{
			const String jdkText = "jdk";
			Int32 jdkIndex = directoryName.IndexOf(jdkText.AsSpan());
			Int32 versionIndex = directoryName[(jdkIndex + jdkText.Length)..].IndexOf(versionPattern);
			return versionIndex <= 0;
		}
		private static String GetProgramFilesPath(Environment.SpecialFolder folder)
		{
			try
			{
				return Environment.GetFolderPath(folder);
			}
			catch (Exception)
			{
				return folder is Environment.SpecialFolder.ProgramFiles ?
					@"C:\Program Files" :
					@"C:\Program Files (x86)";
			}
		}
		private static async Task InstallFromExeAsync(String installerFileName)
		{
			ExecuteState state = new()
			{
				ExecutablePath = installerFileName,
				AppendArgs = a =>
				{
					a.Add("/s");
					a.Add("ADDLOCAL=\"ToolsFeature\"");
				},
				Notifier = ConsoleNotifier.Notifier,
			};
			await Utilities.Execute(state);
		}
	}
}
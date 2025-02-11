namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	private partial class Linux
	{
		private readonly ConcurrentDictionary<JdkVersion, Jdk> _amd64 = new();
		private readonly ConcurrentDictionary<JdkVersion, Jdk> _arm64 = new();
		private readonly ConcurrentDictionary<JdkVersion, Jdk> _armhf = new();
		private readonly Boolean _isArmHf;

		private Linux(DirectoryInfo outputDirectory, out Task initialize) : base(outputDirectory)
		{
			this._isArmHf = Linux.IsArmHf(this.CurrentArch);
			this.Architectures = Enum.GetValues<Architecture>()
			                         .Where(a => this.IsCurrentArch(a) || Linux.IsArmHf(a) ||
				                                (a is Architecture.X64 or Architecture.Arm64 &&
					                                !Linux.IsArmHf(this.CurrentArch))).ToArray();
			initialize = this.Initialize();
		}
		private async Task Initialize()
		{
			List<Task> tasks = [];
			foreach (JdkVersion version in Enum.GetValues<JdkVersion>().AsSpan())
			{
				tasks.Add(this.AppendJdk(this._arm64, version, Architecture.Arm64));
				tasks.Add(this.AppendJdk(this._amd64, version, Architecture.X64));
				tasks.Add(this.AppendJdk(this._armhf, version, Architecture.X86));
			}
			await Task.WhenAll(tasks);
		}
		private async Task SelfExtractBinary(String tempFileName, String jdkPath)
		{
			await Utilities.Execute<String>(new()
			{
				ExecutablePath = "chmod",
				ArgState = tempFileName,
				AppendArgs = (s, a) =>
				{
					a.Add("+x");
					a.Add(tempFileName);
				},
				Notifier = ConsoleNotifier.Notifier,
			});

			Task task = this.CurrentArch is Architecture.X64 ?
				Linux.RunSelfExtract(tempFileName, jdkPath) :
				Linux.RunSelfExtractQemu(tempFileName, jdkPath);
			await task;
		}
		private static async Task RunSelfExtract(String tempFileName, String jdkPath)
		{
			ExecuteState state = new()
			{
				ExecutablePath = tempFileName, AppendArgs = a => { }, WorkingDirectory = jdkPath,
			};
			await Utilities.Execute(state);
		}
		private static async Task RunSelfExtractQemu(String tempFileName, String jdkPath)
		{
			(String qemuExe, String qemuRoot) = Linux.qemu[Architecture.X64];
			QemuExecuteState state = new()
			{
				QemuExecutable = qemuExe,
				QemuRoot = qemuRoot,
				ExecutablePath = tempFileName,
				AppendArgs = a => { },
				WorkingDirectory = jdkPath,
			};
			await Utilities.QemuExecute(state);
		}
		private async Task RunAppQemu(FileInfo appFile, Jdk jdk)
		{
			(String qemuExe, String qemuRoot) = Linux.qemu[jdk.JavaArchitecture];
			QemuExecuteState<AppArgs> state = new()
			{
				QemuRoot = qemuRoot,
				QemuExecutable = qemuExe,
				ExecutablePath = appFile.FullName,
				ArgState = jdk,
				AppendArgs = AppArgs.Append,
				WorkingDirectory = this.OutputDirectory.FullName,
				Notifier = ConsoleNotifier.Notifier,
			};
			Int32 result = await Utilities.QemuExecute(state);
			ConsoleNotifier.Notifier.Result(result, appFile.Name);
		}
		private async Task<Int32> RunJarQemu(JarArgs jarArgs, Jdk jdk)
		{
			(String qemuExe, String qemuRoot) = Linux.qemu[jdk.JavaArchitecture];
			QemuExecuteState<JarArgs> state = new()
			{
				QemuRoot = qemuRoot,
				QemuExecutable = qemuExe,
				ExecutablePath = jdk.JavaExecutable,
				ArgState = jarArgs,
				AppendArgs = JarArgs.Append,
				WorkingDirectory = this.OutputDirectory.FullName,
				Notifier = ConsoleNotifier.Notifier,
			};
			return await Utilities.QemuExecute(state);
		}

		private Boolean IsCurrentArch(Architecture arch)
			=> arch == this.CurrentArch || (this._isArmHf && Linux.IsArmHf(arch));

		private static Boolean IsArmHf(Architecture arch) => arch is Architecture.Arm or Architecture.Armv6;
	}
}
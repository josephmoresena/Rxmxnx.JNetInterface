using System.Collections.Concurrent;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest.Util;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	private partial class Linux
	{
		private readonly ConcurrentDictionary<Jdk.JdkVersion, Jdk> _amd64 = new();
		private readonly ConcurrentDictionary<Jdk.JdkVersion, Jdk> _arm64 = new();
		private readonly ConcurrentDictionary<Jdk.JdkVersion, Jdk> _armhf = new();
		private readonly Boolean _isArmHf;

		private Linux(DirectoryInfo outputDirectory, out Task initialize) : base(outputDirectory, OSPlatform.Linux)
		{
			this._isArmHf = Linux.IsArmHf(this.CurrentArch);
			this.Architectures = Enum.GetValues<Architecture>()
			                         .Where(a => this.IsCurrentArch(a) || Linux.IsArmHf(a) ||
				                                (a is Architecture.X64 && Linux.IsArmHf(this.CurrentArch))).ToArray();
			initialize = this.Initialize();
		}
		private async Task Initialize()
		{
			List<Task> tasks = [];
			foreach (Jdk.JdkVersion version in Enum.GetValues<Jdk.JdkVersion>().AsSpan())
			{
				tasks.Add(this.AppendJdk(this._arm64, version, Architecture.Arm64));
				tasks.Add(this.AppendJdk(this._amd64, version, Architecture.X64));
				tasks.Add(this.AppendJdk(this._armhf, version, Architecture.X86));
			}
			await Task.WhenAll(tasks);
		}
		private async Task SelfExtractBinary(String tempFileName, String jdkPath)
		{
			Task task = this.CurrentArch is Architecture.X64 ?
				Linux.NoQemuExtract(tempFileName, jdkPath) :
				Linux.QemuExtract(tempFileName, jdkPath);
			await task;
		}
		private static async Task NoQemuExtract(String tempFileName, String jdkPath)
		{
			ExecuteState state = new()
			{
				ExecutablePath = tempFileName, AppendArgs = a => { }, WorkingDirectory = jdkPath,
			};
			await Utilities.Execute(state);
		}
		private static async Task QemuExtract(String tempFileName, String jdkPath)
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
		private Boolean IsCurrentArch(Architecture arch)
			=> arch == this.CurrentArch || (this._isArmHf && Linux.IsArmHf(arch));

		private static Boolean IsArmHf(Architecture arch) => arch is Architecture.Arm or Architecture.Armv6;
	}
}
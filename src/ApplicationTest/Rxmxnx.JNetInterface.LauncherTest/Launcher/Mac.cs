using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest.Util;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public abstract partial class Launcher
{
	private sealed partial class Mac : Launcher
	{
		public override String RuntimeIdentifierPrefix => "osx";
		public override IEnumerable<Jdk> this[Architecture arch]
			=> arch switch
			{
				Architecture.X64 => this._amd64.Values,
				Architecture.Arm64 => this._arm64.Values,
				_ => base[arch],
			};

		protected override String JavaArchiverName => "jar";
		protected override String JavaExecutableName => "java";
		protected override String JavaCompilerName => "javac";

		public override Jdk GetMinJdk() => this._amd64[Jdk.JdkVersion.Jdk6];

		protected override String GetJavaLibraryName(Jdk.JdkVersion version)
			=> version is Jdk.JdkVersion.Jdk6 ? "libserver.dylib" : "libjvm.dylib";
		public override async Task Execute()
		{
			Architecture[] architectures = Enum.GetValues<Architecture>()
			                                   .Where(a => a == this.CurrentArch || a == Architecture.X64).ToArray();
			Dictionary<Architecture, FileInfo[]> archFiles = architectures.ToDictionary(
				a => a, a => this.OutputDirectory.GetFiles($"ApplicationTest.osx-{Enum.GetName(a)!.ToLower()}.*"));
			FileInfo? jarFile = this.OutputDirectory.GetFiles("HelloJni.jar").FirstOrDefault();
			foreach (Jdk jdk in architectures.SelectMany(a => this[a]))
			{
				if (jarFile is not null)
					foreach (TestCompiler.NetVersion netVersion in Enum.GetValues<TestCompiler.NetVersion>())
					{
						IMutableWrapper<Boolean> noReflection = IMutableWrapper.Create(false);
						ExecuteState<ValueTuple<Byte, IWrapper<Boolean>, String>> state = new()
						{
							ExecutablePath = jdk.JavaExecutable,
							ArgState = ((Byte)netVersion, noReflection, jarFile.FullName),
							AppendArgs = (s, a) =>
							{
								a.Add($"-Ddotnet.runtime.version=net{s.Item1}.0");
								if (s.Item2.Value)
									a.Add("-Dno-reflection=true");
								a.Add("jar");
								a.Add(jarFile.FullName);
							},
							WorkingDirectory = this.OutputDirectory.FullName,
							Notifier = ConsoleNotifier.Notifier,
						};
						ConsoleNotifier.Notifier.Result(await Utilities.Execute(state),
						                                $"HelloJni.jar {jdk.JavaVersion} {jdk.JavaArchitecture} {netVersion} Reflection: {!noReflection.Value}");
						noReflection.Value = true;
						ConsoleNotifier.Notifier.Result(await Utilities.Execute(state),
						                                $"HelloJni.jar {jdk.JavaVersion} {jdk.JavaArchitecture} {netVersion} Reflection: {!noReflection.Value}");
					}

				foreach (FileInfo appFile in archFiles[jdk.JavaArchitecture])
				{
					ExecuteState<String> state = new()
					{
						ExecutablePath = appFile.FullName,
						ArgState = jdk.JavaLibrary,
						AppendArgs = (s, a) => a.Add(s),
						WorkingDirectory = this.OutputDirectory.FullName,
						Notifier = ConsoleNotifier.Notifier,
					};
					Int32 result = await Utilities.Execute(state);
					ConsoleNotifier.Notifier.Result(result, appFile.Name);
				}
			}
		}

		protected override async Task<Jdk> DownloadJdk(Jdk.JdkVersion version, Architecture arch)
		{
			String jdkPath = $"jdk_{arch}_{version}";
			if (this.GetJdk(version, arch, jdkPath) is { } result) return result;
			IReadOnlyDictionary<Jdk.JdkVersion, String> urls = arch is Architecture.X64 ? Mac.amd64Url : Mac.arm64Url;
			String tempFileName = Path.GetTempFileName();
			try
			{
				Directory.CreateDirectory(jdkPath);
				await Utilities.DownloadFileAsync(new()
				{
					Url = urls[version],
					Destination = tempFileName,
					Notifier = ConsoleNotifier.Notifier,
				});
				if (version is not Jdk.JdkVersion.Jdk6)
					await Launcher.ExtractTarGz(tempFileName, jdkPath);
				else
					await Mac.ExtractFromDmgPkgAsync(tempFileName, jdkPath);
			}
			finally
			{
				File.Delete(tempFileName);
			}
			result = this.GetJdk(version, arch, jdkPath, out DirectoryInfo jdkDirectory)!;
			ConsoleNotifier.PlatformNotifier.JdkDownload(version, arch, jdkDirectory.FullName);
			return result;
		}

		public static async Task<Launcher> CreateInstance(DirectoryInfo publishDirectory)
		{
			Mac result = new(publishDirectory, out Task initialize);
			await initialize;
			ConsoleNotifier.PlatformNotifier.Initialization(result.Platform, result.CurrentArch);
			return result;
		}
	}
}
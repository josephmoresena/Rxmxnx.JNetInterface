namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private sealed partial class FreeBsd : Launcher, ILauncher<FreeBsd>
	{
		static OSPlatform ILauncher<FreeBsd>.Platform => OSPlatform.FreeBSD;
		public override String RuntimeIdentifierPrefix => this._runtimeIdentifier;
		public override Architecture[] Architectures => FreeBsd.currentArch;
		public override NetVersion[] NetVersions => FreeBsd.netVersions;
		public override IEnumerable<Jdk> this[Architecture arch]
			=> arch == this.CurrentArch ? this._jdks.Values : base[arch];

		protected override String JavaArchiverName => "jar";
		protected override String JavaExecutableName => "java";
		protected override String JavaCompilerName => "javac";

		public override Jdk GetMinJdk() => this._jdks[this._jdks.Keys.Min()];

		protected override String GetJavaLibraryName(JdkVersion version) => "libjvm.so";
		protected override async Task<Jdk?> DownloadJdk(JdkVersion version, Architecture arch)
		{
			if (arch != this.Architectures[0]) return default;
			String? pkgName = version switch
			{
				JdkVersion.Jdk8 => "openjdk8",
				JdkVersion.Jdk11 => "openjdk11",
				JdkVersion.Jdk17 => "openjdk17",
				JdkVersion.Jdk21 => "openjdk21",
				JdkVersion.Jdk25 => "openjdk25",
				_ => default,
			};
			await FreeBsd.pkgSemaphore.WaitAsync();
			try
			{
				if (String.IsNullOrWhiteSpace(pkgName) || await Utilities.Execute<String>(new()
				    {
					    ExecutablePath = "pkg",
					    ArgState = pkgName,
					    AppendArgs = (s, a) =>
					    {
						    a.Add("install");
						    a.Add(s);
					    },
					    Notifier = ConsoleNotifier.Notifier,
				    }) != 0)
					return default;
			}
			finally
			{
				FreeBsd.pkgSemaphore.Release();
			}
			return this.GetJdk(version, arch, $"/usr/local/{pkgName}");
		}

		public static FreeBsd Create(DirectoryInfo outputDirectory, out Task initTask)
			=> new(outputDirectory, out initTask);
	}
}
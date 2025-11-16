namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	protected readonly struct JarArgs()
	{
		private readonly IMutableWrapper<Boolean> _noReflection = IMutableWrapper<Boolean>.Create();

		public NetVersion Version { get; init; }
		public String JarName { get; init; } = String.Empty;
		public Boolean NoReflection
		{
			get => this._noReflection.Value;
			set => this._noReflection.Value = value;
		}
		public JdkVersion JdkVersion { get; init; }

		public static void Append(JarArgs jarArgs, Collection<String> args)
		{
			args.Add("-Djava.library.path=.");
			args.Add($"-Ddotnet.runtime.version=net{(Byte)jarArgs.Version}.0");
			if (jarArgs.NoReflection)
				args.Add("-Ddotnet.reflection.disable=true");
			args.Add(jarArgs.JdkVersion > JdkVersion.Jdk6 ? "-XX:+ErrorFileToStdout" :
			         !SystemInfo.IsWindows ? "-XX:ErrorFile=/dev/stderr" : "");
			args.Add("-XX:+UnlockDiagnosticVMOptions");
			if (Boolean.TryParse(Environment.GetEnvironmentVariable("JNETINTERFACE_JNI_CHECK"),
			                     out Boolean useJniCheck) && useJniCheck)
				args.Add("-Xcheck:jni");
			args.Add("-jar");
			args.Add(jarArgs.JarName);
		}
	}
}
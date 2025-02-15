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

		public static void Append(JarArgs jarArgs, Collection<String> args)
		{
			args.Add("-Djava.library.path=.");
			args.Add($"-Ddotnet.runtime.version=net{(Byte)jarArgs.Version}.0");
			if (jarArgs.NoReflection)
				args.Add("-Ddotnet.reflection.disable=true");
			args.Add("-jar");
			args.Add(jarArgs.JarName);
		}
	}
}
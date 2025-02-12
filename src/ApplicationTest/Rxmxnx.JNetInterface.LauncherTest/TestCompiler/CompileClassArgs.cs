namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private readonly struct CompileClassArgs
	{
		public JdkVersion Version { get; init; }
		public JdkVersion Target { get; init; }
		public String JavaFilePath { get; init; }

		public static void Append(CompileClassArgs compileArgs, Collection<String> args)
		{
			if (compileArgs.Version > compileArgs.Target && compileArgs.Version < JdkVersion.Jdk17)
			{
				if (compileArgs.Version <= JdkVersion.Jdk8)
				{
					args.Add($"-target 1.{compileArgs.Target}");
					args.Add($"-source 1.{compileArgs.Target}");
				}
				else
				{
					args.Add($"-target {compileArgs.Target}");
					args.Add($"-source {compileArgs.Target}");
				}
			}
			args.Add("-verbose");
			args.Add(compileArgs.JavaFilePath);

			if (compileArgs.Version > compileArgs.Target && compileArgs.Version > JdkVersion.Jdk17)
				args.Add($"--release {compileArgs.Target}");
		}
	}
}
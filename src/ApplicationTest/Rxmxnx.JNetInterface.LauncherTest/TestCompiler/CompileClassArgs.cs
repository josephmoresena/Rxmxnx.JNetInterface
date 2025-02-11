namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private readonly struct CompileClassArgs
	{
		public String JavaFilePath { get; init; }
		public String? Target { get; init; }

		public static void Append(CompileClassArgs compileArgs, Collection<String> args)
		{
			args.Add(compileArgs.JavaFilePath);
			if (!String.IsNullOrEmpty(compileArgs.Target))
				args.Add($"-target {compileArgs.Target}");
		}
	}
}
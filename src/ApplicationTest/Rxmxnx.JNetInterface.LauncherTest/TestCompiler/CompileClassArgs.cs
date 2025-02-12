namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private readonly struct CompileClassArgs
	{
		public String JavaFilePath { get; init; }

		public static void Append(CompileClassArgs compileArgs, Collection<String> args)
		{
			args.Add("-verbose");
			args.Add(compileArgs.JavaFilePath);
		}
	}
}
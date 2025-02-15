namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private readonly struct JarCreationArgs
	{
		public String JarRoot { get; init; }
		public String JarFileName { get; init; }
		public String ManifestFileName { get; init; }

		public static void Append(JarCreationArgs jarCreationArgs, Collection<String> args)
		{
			args.Add("cfm");
			args.Add(jarCreationArgs.JarFileName);
			args.Add(jarCreationArgs.ManifestFileName);
			args.Add("-C");
			args.Add(jarCreationArgs.JarRoot);
			args.Add(".");
		}
	}
}
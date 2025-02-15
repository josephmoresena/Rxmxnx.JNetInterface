namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Launcher
{
	private readonly struct AppArgs
	{
		private readonly String _libraryPath;

		private AppArgs(String libraryPath) => this._libraryPath = libraryPath;

		public static implicit operator AppArgs(Jdk jdk) => new(jdk.JavaLibrary);

		public static void Append(AppArgs appArgs, Collection<String> args) => args.Add(appArgs._libraryPath);
	}
}
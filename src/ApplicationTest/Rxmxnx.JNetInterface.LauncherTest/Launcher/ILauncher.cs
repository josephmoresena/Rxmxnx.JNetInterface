namespace Rxmxnx.JNetInterface.ApplicationTest;

// ReSharper disable once ClassCannotBeInstantiated
public partial class Launcher
{
	// ReSharper disable once TypeParameterCanBeVariant
	protected interface ILauncher<TLauncher> where TLauncher : Launcher, ILauncher<TLauncher>
	{
		static abstract OSPlatform Platform { get; }
		static abstract TLauncher Create(DirectoryInfo outputDirectory, out Task initTask);
	}
}
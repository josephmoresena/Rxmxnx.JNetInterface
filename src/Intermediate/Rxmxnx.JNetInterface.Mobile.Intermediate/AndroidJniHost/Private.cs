namespace Rxmxnx.JNetInterface;

public sealed partial class AndroidJniHost
{
	/// <summary>
	/// Singleton instance.
	/// </summary>
	private static AndroidJniHost? instance;
	/// <summary>
	/// User main classes' dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, JDataTypeMetadata> userMainClasses =
		MainClasses.CreateMainClassesDictionary();

	/// <summary>
	/// <see cref="AndroidJniHost"/> cache.
	/// </summary>
	private readonly VirtualMachineCore<AndroidEnvironment> _core;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	private AndroidJniHost(JVirtualMachineRef vmRef)
	{
		this._core = new(this, vmRef);
		this._core.InitializeClasses();
		JTrace.VirtualMachineLoad(vmRef, true, $"{nameof(AndroidJniHost)}.ctor");
	}
}
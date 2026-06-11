namespace Rxmxnx.JNetInterface;

public sealed partial class AndroidJniHost
{
	/// <summary>
	/// Retrieves the current <see cref="AndroidJniContext"/> instance.
	/// </summary>
	/// <returns>The active <see cref="AndroidJniHost"/> instance.</returns>
	/// <exception cref="InvalidOperationException"></exception>
	internal static IVirtualMachineHost GetAndroidHost()
	{
		if (AndroidJniHost.instance is not null) return AndroidJniHost.instance;
		if (JniRuntime.CurrentRuntime.InvocationPointer == IntPtr.Zero)
			throw new InvalidOperationException(); //TODO: MESSAGE
		JVirtualMachineRef vmRef = default;
		Unsafe.As<JVirtualMachineRef, IntPtr>(ref vmRef) = JniRuntime.CurrentRuntime.InvocationPointer;
		return AndroidJniHost.instance = new(vmRef);
	}
}
namespace Rxmxnx.JNetInterface.Tests;

public static partial class NativeInterfaceHelper
{
	public static JEnvironmentRef InitializeProxy(NativeInterfaceProxy proxy)
	{
		IntPtr ptr = NativeInterfaceHelper.helper.Get();
		JEnvironmentRef result = NativeUtilities.Transform<IntPtr, JEnvironmentRef>(ptr);
		NativeInterfaceHelper.proxies[result] = proxy;
		return result;
	}
	public static void FinalizeProxy(JEnvironmentRef envRef) => NativeInterfaceHelper.proxies.TryRemove(envRef, out _);
}
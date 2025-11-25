namespace Rxmxnx.JNetInterface.Tests;

public abstract class LibraryBaseTests
{
	[ThreadStatic]
	private protected static Int32 Count;
	[ThreadStatic]
	private protected static Int32 JniVersion;
	[ThreadStatic]
	private protected static Exception? NativeException;
	[ThreadStatic]
	private protected static JVirtualMachineInitArg? Args;
	[ThreadStatic]
	private protected static JResult Result;
	[ThreadStatic]
	private protected static NativeInterfaceProxy? ProxyEnv;
	[ThreadStatic]
	private protected static ReadOnlyValPtr<VirtualMachineInitOptionValue> OptionsPtr;
	[ThreadStatic]
	private protected static NativeInterfaceProxy[]? Proxies;

	private protected static void CleanUp()
	{
		LibraryBaseTests.Count = default;
		LibraryBaseTests.JniVersion = default;
		LibraryBaseTests.NativeException = default;
		LibraryBaseTests.Result = default;
		LibraryBaseTests.ProxyEnv = default;
		LibraryBaseTests.OptionsPtr = default;
		LibraryBaseTests.Proxies = default;
	}
}
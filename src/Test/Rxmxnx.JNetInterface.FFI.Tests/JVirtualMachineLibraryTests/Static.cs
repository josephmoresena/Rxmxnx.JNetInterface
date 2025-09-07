namespace Rxmxnx.JNetInterface.Tests;

public sealed unsafe partial class JVirtualMachineLibraryTests
{
	[Flags]
	internal enum JvmProxyType : Byte
	{
		Complete,
		NoCreate = 0x1,
		NoVMs = 0x2,
		NoCreateAndNoVMs = JvmProxyType.NoCreate | JvmProxyType.NoVMs,
	}
	
	private const String NativeLibraryName = "JvmProxy";

	private static readonly String systemLibraryName = OperatingSystem.IsWindows() ? "user32" :
		OperatingSystem.IsMacOS() ? "/usr/lib/libSystem.B.dylib" : "libc";
	private static readonly String libraryPrefix = OperatingSystem.IsWindows() ? String.Empty : "lib";
	private static readonly String nativeExtension = OperatingSystem.IsWindows() ? ".dll" :
		OperatingSystem.IsMacOS() ? ".dylib" : ".so";
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	
	private static String GetProxyPath(JvmProxyType proxyType)
	{
		StringBuilder fileNameBuilder = new(50);
		fileNameBuilder.Append(JVirtualMachineLibraryTests.libraryPrefix);
		fileNameBuilder.Append(JVirtualMachineLibraryTests.NativeLibraryName);
		if (proxyType is not JvmProxyType.Complete) fileNameBuilder.Append($".{proxyType}");
		fileNameBuilder.Append($".{RuntimeInformation.ProcessArchitecture}".ToLowerInvariant());
		fileNameBuilder.Append(JVirtualMachineLibraryTests.nativeExtension);
		return fileNameBuilder.ToString();
	}
	private static CStringSequence CreateOptionsSequence()
	{
		Span<Int32> sizes = stackalloc Int32[4];
		sizes[0] = Random.Shared.Next(0, 5);
		sizes[1] = Random.Shared.Next(0, 5);
		sizes[2] = Random.Shared.Next(0, 5);
		sizes[3] = Random.Shared.Next(0, 5);
		return new(JVirtualMachineLibraryTests.fixture.CreateMany<String>(sizes[0])
		                                      .Concat(Enumerable.Repeat(String.Empty, sizes[1]))
		                                      .Concat(JVirtualMachineLibraryTests.fixture.CreateMany<String>(sizes[2]))
		                                      .Concat(Enumerable.Repeat(String.Empty, sizes[3])));
	}
	private static ReadOnlyValPtr<VirtualMachineInitOptionValue> GetOptionsPtr(
		Span<VirtualMachineInitOptionValue> optionsAlloc, CStringSequence options)
	{
		Int32 index = 0;
		foreach (ReadOnlySpan<Byte> value in options.CreateView())
		{
			optionsAlloc[index] = new(value.GetUnsafeValPtr());
			index++;
		}
		return optionsAlloc.GetUnsafeValPtr();
	}
	private static delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>,
		FuncPtr<CreateVirtualMachineDelegate>, FuncPtr<GetCreatedJavaVMsDelegate>, void> GetProxyMethods(
			JVirtualMachineLibrary library, out delegate* unmanaged<void> reset)
	{
		reset = (delegate* unmanaged<void>)NativeLibrary.GetExport(library.Handle, "Reset");
		return (delegate* unmanaged< FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>,
			FuncPtr<CreateVirtualMachineDelegate>, FuncPtr<GetCreatedJavaVMsDelegate>, void>)NativeLibrary.GetExport(
			library.Handle, "ArrangeInvocation");
	}
}
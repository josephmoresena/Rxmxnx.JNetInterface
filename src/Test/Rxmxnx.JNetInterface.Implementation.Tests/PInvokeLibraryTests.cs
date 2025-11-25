namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed partial class PInvokeLibraryTests : LibraryBaseTests, IVirtualMachineLibraryType
{
	static Boolean IVirtualMachineLibraryType.IsStatic => PInvokeLibraryTests.isStatic;
	static Boolean IVirtualMachineLibraryType.HasCreatedVmMethod => !PInvokeLibraryTests.missingCreatedVmMethod;

	[SkippableFact]
	internal void StaticTest()
	{
		try
		{
			PInvokeLibraryTests.isStatic = true;
			Skip.If(AotInfo.IsNativeAot);

			InvalidOperationException ex =
				Assert.Throws<InvalidOperationException>(JVirtualMachineLibrary.Create<PInvokeLibraryTests>);
			Assert.Contains("Native AOT", ex.Message);
		}
		finally
		{
			PInvokeLibraryTests.CleanUp();
		}
	}
	[Fact]
	internal void MissingCreatedVms()
	{
		Boolean enter = false;
		try
		{
			PInvokeLibraryTests.isStatic = false;
			PInvokeLibraryTests.missingCreatedVmMethod = true;
			JVirtualMachineLibrary library = JVirtualMachineLibrary.Create<PInvokeLibraryTests>();

			Assert.Empty(library.GetCreatedVirtualMachines());
			PInvokeLibraryTests.getCreatedVirtualMachines = Call;
			Assert.Empty(library.GetCreatedVirtualMachines());
			Assert.False(enter);
		}
		finally
		{
			PInvokeLibraryTests.CleanUp();
		}
		return;
		JResult Call(ValPtr<JVirtualMachineRef> arr, Int32 arrSize, out Int32 count)
		{
			count = Random.Shared.Next();
			enter = true;
			return JResult.Ok;
		}
	}

	private new static void CleanUp()
	{
		LibraryBaseTests.CleanUp();
		PInvokeLibraryTests.missingCreatedVmMethod = false;
		PInvokeLibraryTests.isStatic = false;
		PInvokeLibraryTests.getDefaultVirtualMachineInitArgs = default;
		PInvokeLibraryTests.createVirtualMachine = default;
		PInvokeLibraryTests.getCreatedVirtualMachines = default;
	}

	static JResult IVirtualMachineLibraryType.GetDefaultVirtualMachineInitArgs(
		ref VirtualMachineInitArgumentValue initArg)
		=> PInvokeLibraryTests.getDefaultVirtualMachineInitArgs?.Invoke(ref initArg) ?? JResult.DetachedThreadError;
	static JResult IVirtualMachineLibraryType.CreateVirtualMachine(out JVirtualMachineRef vmRef,
		out JEnvironmentRef envRef, in VirtualMachineInitArgumentValue initArg)
	{
		if (PInvokeLibraryTests.createVirtualMachine is not null)
			return PInvokeLibraryTests.createVirtualMachine(out vmRef, out envRef, in initArg);
		Unsafe.SkipInit(out vmRef);
		Unsafe.SkipInit(out envRef);
		return JResult.DetachedThreadError;
	}
	static JResult IVirtualMachineLibraryType.GetCreatedVirtualMachines(ValPtr<JVirtualMachineRef> arr, Int32 arrSize,
		out Int32 count)
	{
		if (PInvokeLibraryTests.getCreatedVirtualMachines is not null)
			return PInvokeLibraryTests.getCreatedVirtualMachines.Invoke(arr, arrSize, out count);
		Unsafe.SkipInit(out count);
		return JResult.DetachedThreadError;
	}
}
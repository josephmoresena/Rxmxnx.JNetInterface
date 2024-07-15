namespace Rxmxnx.JNetInterface.Tests;

public partial class JNativeCallAdapterTests
{
	[Fact]
	internal void UnknownParametersCallTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(ProxyFactory.Instance);
		JNativeCallAdapter adapter = default;
		List<JLocalObject?> parameters = [];
		try
		{
			proxyEnv.UseVirtualMachineRef = false;
			proxyEnv.When(e => e.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()))
			        .Do(c => ((ValPtr<JVirtualMachineRef>)c[0]).Reference = proxyEnv.VirtualMachine.Reference);
			JNativeCallAdapter.Builder builder = JNativeCallAdapter.Create(proxyEnv.Reference);
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			proxyEnv.Received(1).GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>());

			adapter = JNativeCallAdapterTests.CreateParameters(proxyEnv, builder, out parameters);
			Assert.Equal(vm.GetEnvironment(), adapter.Environment);
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeTest(proxyEnv, CallResult.Void, adapter);
			Assert.All(parameters, o => JObject.IsNullOrDefault(o));
		}
	}
}
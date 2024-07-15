namespace Rxmxnx.JNetInterface.Tests;

public partial class JNativeCallAdapterTests
{
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, CallResult.Primitive)]
	[InlineData(false, CallResult.Primitive)]
	[InlineData(true, CallResult.Object)]
	[InlineData(false, CallResult.Object)]
	[InlineData(true, CallResult.Class)]
	[InlineData(false, CallResult.Class)]
	[InlineData(true, CallResult.Throwable)]
	[InlineData(false, CallResult.Throwable)]
	[InlineData(true, CallResult.Global)]
	[InlineData(false, CallResult.Global)]
	[InlineData(true, CallResult.Array)]
	[InlineData(false, CallResult.Array)]
	[InlineData(true, CallResult.PrimitiveArray)]
	[InlineData(false, CallResult.PrimitiveArray)]
	[InlineData(true, CallResult.ObjectArray)]
	[InlineData(false, CallResult.ObjectArray)]
	[InlineData(true, CallResult.BooleanArray)]
	[InlineData(false, CallResult.BooleanArray)]
	[InlineData(true, CallResult.ByteArray)]
	[InlineData(false, CallResult.ByteArray)]
	[InlineData(true, CallResult.CharArray)]
	[InlineData(false, CallResult.CharArray)]
	[InlineData(true, CallResult.DoubleArray)]
	[InlineData(false, CallResult.DoubleArray)]
	[InlineData(true, CallResult.FloatArray)]
	[InlineData(false, CallResult.FloatArray)]
	[InlineData(true, CallResult.IntArray)]
	[InlineData(false, CallResult.IntArray)]
	[InlineData(true, CallResult.LongArray)]
	[InlineData(false, CallResult.LongArray)]
	[InlineData(true, CallResult.ShortArray)]
	[InlineData(false, CallResult.ShortArray)]
	[InlineData(true, CallResult.String)]
	[InlineData(false, CallResult.String)]
	[InlineData(true, CallResult.Nested)]
	[InlineData(false, CallResult.Nested)]
	internal void UnknownParameterlessCallTest(Boolean useVm, CallResult result = CallResult.Void)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(factory);
		JNativeCallAdapter adapter = default;
		try
		{
			proxyEnv.UseVirtualMachineRef = false;
			proxyEnv.When(e => e.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()))
			        .Do(c => ((ValPtr<JVirtualMachineRef>)c[0]).Reference = proxyEnv.VirtualMachine.Reference);
			adapter = useVm ?
				JNativeCallAdapter.Create(JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference),
				                          proxyEnv.Reference).Build() :
				JNativeCallAdapter.Create(proxyEnv.Reference).Build();
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			Assert.Equal(vm.GetEnvironment(), adapter.Environment);
			proxyEnv.Received(!useVm ? 1 : 0).GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>());
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeTest(proxyEnv, result, adapter);
		}
	}
}
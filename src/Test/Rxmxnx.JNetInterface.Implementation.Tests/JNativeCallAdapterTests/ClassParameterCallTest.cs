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
	[InlineData(true, CallResult.NestedStatic)]
	[InlineData(false, CallResult.NestedStatic)]
	[InlineData(true, CallResult.Parameter)]
	[InlineData(false, CallResult.Parameter)]
	internal void ClassParameterCall(Boolean useVm, CallResult result = CallResult.Void)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(ProxyFactory.Instance);
		JNativeCallAdapter adapter = default;
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef clsStrRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JClassTypeMetadata classTypeMetadata = IClassType.GetMetadata<JTestObject>();
		JClassTypeMetadata classClassTypeMetadata = IClassType.GetMetadata<JClassObject>();
		JClassObject? testClass = default;
		using IReadOnlyFixedContext<Char>.IDisposable classCtx =
			classClassTypeMetadata.Information.ToString().AsMemory().GetFixedContext();
		using IReadOnlyFixedContext<Char>.IDisposable ctx = classTypeMetadata.Information.ToString().AsMemory()
		                                                                     .GetFixedContext();
		try
		{
			proxyEnv.GetObjectClass(classRef.Value).Returns(proxyEnv.ClassLocalRef);
			proxyEnv.GetObjectRefType(classRef.Value).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetStringUtfLength(strRef).Returns(classTypeMetadata.ClassName.Length);
			proxyEnv.GetStringUtfLength(clsStrRef).Returns(classClassTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
			proxyEnv.CallObjectMethod(proxyEnv.ClassLocalRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(clsStrRef.Value);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>()).Returns((ReadOnlyValPtr<Byte>)ctx.Pointer);
			proxyEnv.GetStringUtfChars(clsStrRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)classCtx.Pointer);
			proxyEnv.UseVirtualMachineRef = false;
			proxyEnv.When(e => e.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()))
			        .Do(c => ((ValPtr<JVirtualMachineRef>)c[0]).Reference = proxyEnv.VirtualMachine.Reference);
			JNativeCallAdapter.Builder adapterBuilder = useVm ?
				JNativeCallAdapter.Create(JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference),
				                          proxyEnv.Reference) :
				JNativeCallAdapter.Create(proxyEnv.Reference);
			adapter = adapterBuilder.WithParameter(classRef, out testClass).Build();
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			Assert.Equal(vm.GetEnvironment(), adapter.Environment);
			proxyEnv.Received(!useVm ? 1 : 0).GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>());
			proxyEnv.Received(1).GetObjectClass(classRef.Value);
			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).CallObjectMethod(proxyEnv.ClassLocalRef.Value,
			                                      proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(1).GetStringUtfLength(strRef);
			proxyEnv.Received(1).GetStringUtfLength(clsStrRef);
			proxyEnv.Received(1).GetObjectRefType(classRef.Value);
			proxyEnv.Received(0).CallBooleanMethod(classRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(proxyEnv.ClassLocalRef.Value,
			                                       proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeTest(proxyEnv, result, adapter, testClass: testClass, classRef: classRef);
		}
	}
}
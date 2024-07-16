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
	[InlineData(true, CallResult.Void, true)]
	[InlineData(false, CallResult.Void, true)]
	[InlineData(true, CallResult.Primitive, true)]
	[InlineData(false, CallResult.Primitive, true)]
	[InlineData(true, CallResult.Object, true)]
	[InlineData(false, CallResult.Object, true)]
	[InlineData(true, CallResult.Class, true)]
	[InlineData(false, CallResult.Class, true)]
	[InlineData(true, CallResult.Throwable, true)]
	[InlineData(false, CallResult.Throwable, true)]
	[InlineData(true, CallResult.Global, true)]
	[InlineData(false, CallResult.Global, true)]
	[InlineData(true, CallResult.Array, true)]
	[InlineData(false, CallResult.Array, true)]
	[InlineData(true, CallResult.PrimitiveArray, true)]
	[InlineData(false, CallResult.PrimitiveArray, true)]
	[InlineData(true, CallResult.ObjectArray, true)]
	[InlineData(false, CallResult.ObjectArray, true)]
	[InlineData(true, CallResult.BooleanArray, true)]
	[InlineData(false, CallResult.BooleanArray, true)]
	[InlineData(true, CallResult.ByteArray, true)]
	[InlineData(false, CallResult.ByteArray, true)]
	[InlineData(true, CallResult.CharArray, true)]
	[InlineData(false, CallResult.CharArray, true)]
	[InlineData(true, CallResult.DoubleArray, true)]
	[InlineData(false, CallResult.DoubleArray, true)]
	[InlineData(true, CallResult.FloatArray, true)]
	[InlineData(false, CallResult.FloatArray, true)]
	[InlineData(true, CallResult.IntArray, true)]
	[InlineData(false, CallResult.IntArray, true)]
	[InlineData(true, CallResult.LongArray, true)]
	[InlineData(false, CallResult.LongArray, true)]
	[InlineData(true, CallResult.ShortArray, true)]
	[InlineData(false, CallResult.ShortArray, true)]
	[InlineData(true, CallResult.String, true)]
	[InlineData(false, CallResult.String, true)]
	[InlineData(true, CallResult.Nested, true)]
	[InlineData(false, CallResult.Nested, true)]
	[InlineData(true, CallResult.Parameter, true)]
	[InlineData(false, CallResult.Parameter, true)]
	internal void AgnosticTypedInstanceParameterlessCallTest(Boolean useVm, CallResult result = CallResult.Void,
		Boolean registerClass = false)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JNativeCallAdapter adapter = default;
		JObjectLocalRef localRef = JNativeCallAdapterTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JClassTypeMetadata classTypeMetadata = IClassType.GetMetadata<JTestObject>();
		JLocalObject? testObject = default;
		using IReadOnlyFixedContext<Char>.IDisposable ctx = classTypeMetadata.Information.ToString().AsMemory()
		                                                                     .GetFixedContext();
		if (registerClass)
			JVirtualMachine.Register<JTestObject>();
		registerClass |= MetadataHelper.GetMetadata(classTypeMetadata.Hash) is not null;
		try
		{
			proxyEnv.GetObjectClass(localRef).Returns(classRef);
			proxyEnv.GetObjectRefType(localRef).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetStringUtfLength(strRef).Returns(classTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>()).Returns((ReadOnlyValPtr<Byte>)ctx.Pointer);
			proxyEnv.UseVirtualMachineRef = false;
			proxyEnv.When(e => e.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()))
			        .Do(c => ((ValPtr<JVirtualMachineRef>)c[0]).Reference = proxyEnv.VirtualMachine.Reference);
			adapter = useVm ?
				JNativeCallAdapter
					.Create<JLocalObject>(JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference),
					                      proxyEnv.Reference, localRef, out testObject).Build() :
				JNativeCallAdapter.Create<JLocalObject>(proxyEnv.Reference, localRef, out testObject).Build();
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			Assert.Equal(vm.GetEnvironment(), adapter.Environment);
			proxyEnv.Received(!useVm ? 1 : 0).GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>());
			proxyEnv.Received(1).GetObjectClass(localRef);
			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(1).GetStringUtfLength(strRef);
			proxyEnv.Received(1).GetObjectRefType(localRef);
			proxyEnv.Received(0).CallBooleanMethod(localRef, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);
			if (registerClass)
				Assert.IsType<JTestObject>(testObject);
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeTest(proxyEnv, result, adapter, testObject, localRef);
		}
	}
}
namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JNativeCallAdapterTests(ProxyFactory<JNativeCallAdapterTests> factory)
	: IClassFixture<ProxyFactory<JNativeCallAdapterTests>>, IProxyRequest<JNativeCallAdapterTests>
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	public static UInt32 MaxThreads => 1024;

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
	internal void InstanceParameterlessCallTest(Boolean useVm, CallResult result = CallResult.Void,
		Boolean registerClass = false)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(factory);
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
				JNativeCallAdapter.Create(JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference),
				                          proxyEnv.Reference, localRef, out testObject).Build() :
				JNativeCallAdapter.Create(proxyEnv.Reference, localRef, out testObject).Build();
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
	internal void TypedInstanceParameterlessCallTest(Boolean useVm, CallResult result = CallResult.Void)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(factory);
		JNativeCallAdapter adapter = default;
		JObjectLocalRef localRef = JNativeCallAdapterTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JClassTypeMetadata classTypeMetadata = IClassType.GetMetadata<JTestObject>();
		JTestObject? testObject = default;
		using IReadOnlyFixedContext<Char>.IDisposable ctx = classTypeMetadata.Information.ToString().AsMemory()
		                                                                     .GetFixedContext();
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
					.Create<JTestObject>(JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference),
					                     proxyEnv.Reference, localRef, out testObject).Build() :
				JNativeCallAdapter.Create<JTestObject>(proxyEnv.Reference, localRef, out testObject).Build();
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
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeTest(proxyEnv, result, adapter, testObject, localRef);
		}
	}
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
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(factory);
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
	internal void StaticParameterlessCallTest(Boolean useVm, CallResult result = CallResult.Void)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(factory);
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
			adapter = useVm ?
				JNativeCallAdapter.Create(JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference),
				                          proxyEnv.Reference, classRef, out testClass).Build() :
				JNativeCallAdapter.Create(proxyEnv.Reference, classRef, out testClass).Build();
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
	internal void AgnosticStaticParameterlessCallTest(Boolean useVm, CallResult result = CallResult.Void)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(factory);
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
			adapter = useVm ?
				JNativeCallAdapter.Create(JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference),
				                          proxyEnv.Reference, classRef.Value, out JLocalObject jLocal).Build() :
				JNativeCallAdapter.Create(proxyEnv.Reference, classRef.Value, out jLocal).Build();
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			testClass = Assert.IsType<JClassObject>(jLocal);
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

	private static void NestedAdapterTest(NativeInterfaceProxy proxyEnv)
	{
		using JStringObject jString = JNativeCallAdapterTests.CreateString(proxyEnv, "Sample text");
		JNativeCallAdapter adapter = JNativeCallAdapter.Create(proxyEnv.Reference).Build();
		Assert.Equal(jString.Reference, adapter.FinalizeCall(jString));
	}
	private static void NestedStaticAdapterTest(NativeInterfaceProxy proxyEnv, JClassObject? jClass)
	{
		if (jClass is null) return;
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JClassLocalRef orClasRef = jClass.Reference;
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef clsStrRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JClassTypeMetadata classClassTypeMetadata = IClassType.GetMetadata<JClassObject>();
		using IReadOnlyFixedContext<Char>.IDisposable classCtx =
			classClassTypeMetadata.Information.ToString().AsMemory().GetFixedContext();
		using IReadOnlyFixedContext<Char>.IDisposable ctx = jClass.Hash.AsMemory().GetFixedContext();
		proxyEnv.GetObjectClass(classRef.Value).Returns(proxyEnv.ClassLocalRef);
		proxyEnv.GetObjectRefType(classRef.Value).Returns(JReferenceType.LocalRefType);
		proxyEnv.GetStringUtfLength(strRef).Returns(jClass.Name.Length);
		proxyEnv.GetStringUtfLength(clsStrRef).Returns(classClassTypeMetadata.ClassName.Length);
		proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
		                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
		proxyEnv.CallObjectMethod(proxyEnv.ClassLocalRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
		                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(clsStrRef.Value);
		proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>()).Returns((ReadOnlyValPtr<Byte>)ctx.Pointer);
		proxyEnv.GetStringUtfChars(clsStrRef, Arg.Any<ValPtr<JBoolean>>())
		        .Returns((ReadOnlyValPtr<Byte>)classCtx.Pointer);

		JNativeCallAdapter adapter =
			JNativeCallAdapter.Create(proxyEnv.Reference, classRef, out JClassObject jClass2).Build();
		Assert.Equal(!orClasRef.IsDefault && classRef != orClasRef, classRef != jClass.Reference);
		Assert.True(Object.ReferenceEquals(jClass, jClass2));
		Assert.Equal(jClass.Reference, adapter.FinalizeCall(jClass2));
		Assert.Equal(orClasRef.IsDefault, JObject.IsNullOrDefault(jClass));
	}
	private static JStringObject CreateString(NativeInterfaceProxy proxyEnv, String text)
	{
		IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
		JStringLocalRef stringRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		using IReadOnlyFixedMemory<Char>.IDisposable ctx = text.AsMemory().GetFixedContext();
		proxyEnv.NewString(ctx.ValuePointer, text.Length).Returns(stringRef);
		JStringObject jString = JStringObject.Create(vm.GetEnvironment()!, text);
		proxyEnv.Received(1).NewString(ctx.ValuePointer, text.Length);

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		return jString;
	}
	private static JArrayObject<JClassObject> CreateClassArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JObjectLocalRef localRef = JClassObject.GetClass<JClassObject>(env).Global.Reference.Value;
		JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
		JObjectArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JObjectArrayLocalRef>();
		proxyEnv.NewObjectArray(length, classRef, Arg.Any<JObjectLocalRef>()).Returns(arrayRef);
		JArrayObject<JClassObject> result = JArrayObject<JClassObject>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	private static JArrayObject<JBoolean> CreateBooleanArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JBooleanArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JBooleanArrayLocalRef>();
		proxyEnv.NewBooleanArray(length).Returns(arrayRef);
		JArrayObject<JBoolean> result = JArrayObject<JBoolean>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	private static JArrayObject<JByte> CreateByteArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JByteArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JByteArrayLocalRef>();
		proxyEnv.NewByteArray(length).Returns(arrayRef);
		JArrayObject<JByte> result = JArrayObject<JByte>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	private static JArrayObject<JChar> CreateCharArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JCharArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JCharArrayLocalRef>();
		proxyEnv.NewCharArray(length).Returns(arrayRef);
		JArrayObject<JChar> result = JArrayObject<JChar>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	private static JArrayObject<JDouble> CreateDoubleArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JDoubleArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JDoubleArrayLocalRef>();
		proxyEnv.NewDoubleArray(length).Returns(arrayRef);
		JArrayObject<JDouble> result = JArrayObject<JDouble>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	private static JArrayObject<JFloat> CreateFloatArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JFloatArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JFloatArrayLocalRef>();
		proxyEnv.NewFloatArray(length).Returns(arrayRef);
		JArrayObject<JFloat> result = JArrayObject<JFloat>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	private static JArrayObject<JInt> CreateIntArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JIntArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JIntArrayLocalRef>();
		proxyEnv.NewIntArray(length).Returns(arrayRef);
		JArrayObject<JInt> result = JArrayObject<JInt>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	private static JArrayObject<JLong> CreateLongArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JLongArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JLongArrayLocalRef>();
		proxyEnv.NewLongArray(length).Returns(arrayRef);
		JArrayObject<JLong> result = JArrayObject<JLong>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	private static JArrayObject<JShort> CreateShortArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JShortArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JShortArrayLocalRef>();
		proxyEnv.NewShortArray(length).Returns(arrayRef);
		JArrayObject<JShort> result = JArrayObject<JShort>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	private static JLocalObject CreateObject(NativeInterfaceProxy proxyEnv)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JConstructorDefinition.Parameterless constructor = new();
		JObjectLocalRef localRef = JNativeCallAdapterTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JMethodId methodId = JNativeCallAdapterTests.fixture.Create<JMethodId>();
		using IReadOnlyFixedMemory<Char>.IDisposable ctx = constructor.Information.ToString().AsMemory()
		                                                              .GetFixedContext();
		using IReadOnlyFixedMemory<Char>.IDisposable ctx2 = IDataType.GetMetadata<JLocalObject>().Information.ToString()
		                                                             .AsMemory().GetFixedContext();
		proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx2.Pointer).Returns(classRef);
		proxyEnv.GetMethodId(classRef, (ReadOnlyValPtr<Byte>)ctx.Pointer, Arg.Any<ReadOnlyValPtr<Byte>>())
		        .Returns(methodId);
		proxyEnv.NewObject(classRef, methodId, ReadOnlyValPtr<JValueWrapper>.Zero).Returns(localRef);

		JLocalObject jLocal = constructor.New<JLocalObject>(env);
		try
		{
			Assert.Equal(localRef, jLocal.Reference);
			proxyEnv.Received(1).FindClass((ReadOnlyValPtr<Byte>)ctx2.Pointer);
			proxyEnv.Received(1)
			        .GetMethodId(classRef, (ReadOnlyValPtr<Byte>)ctx.Pointer, Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(1).NewObject(classRef, methodId, ReadOnlyValPtr<JValueWrapper>.Zero);
			return jLocal;
		}
		finally
		{
			JClassObject.GetClass<JLocalObject>(env).Dispose(); // Avoid GC disposing.
		}
	}
	private static JThrowableObject CreateThrowable(NativeInterfaceProxy proxyEnv)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JConstructorDefinition constructor =
			JConstructorDefinition.Create([JArgumentMetadata.Create<JStringObject>(),]);
		JThrowableLocalRef throwableRef = JNativeCallAdapterTests.fixture.Create<JThrowableLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JMethodId methodId = JNativeCallAdapterTests.fixture.Create<JMethodId>();
		using IReadOnlyFixedMemory<Char>.IDisposable ctx = constructor.Information.ToString().AsMemory()
		                                                              .GetFixedContext();
		using IReadOnlyFixedMemory<Char>.IDisposable ctx2 = IDataType.GetMetadata<JErrorObject>().Information.ToString()
		                                                             .AsMemory().GetFixedContext();
		proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx2.Pointer).Returns(classRef);
		proxyEnv.GetMethodId(classRef, (ReadOnlyValPtr<Byte>)ctx.Pointer, Arg.Any<ReadOnlyValPtr<Byte>>())
		        .Returns(methodId);
		proxyEnv.NewObject(classRef, methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>()).Returns(throwableRef.Value);

		using JStringObject jString = JNativeCallAdapterTests.CreateString(proxyEnv, "Error message");
		JStringLocalRef stringRef = jString.Reference;

		proxyEnv.When(e => e.NewObject(classRef, methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>())).Do(c =>
		{
			JObjectLocalRef localRef = stringRef.Value;
			ReadOnlyValPtr<JValueWrapper> args = (ReadOnlyValPtr<JValueWrapper>)c[2];
			Assert.True(NativeUtilities.AsBytes(in args.Reference)[..IntPtr.Size]
			                           .SequenceEqual(NativeUtilities.AsBytes(in localRef)));
		});

		JThrowableObject jThrowable =
			JConstructorDefinition.New<JErrorObject>(constructor, JClassObject.GetClass<JErrorObject>(env), [jString,]);
		try
		{
			Assert.Equal(throwableRef, jThrowable.Reference);
			proxyEnv.Received(1).FindClass((ReadOnlyValPtr<Byte>)ctx2.Pointer);
			proxyEnv.Received(1)
			        .GetMethodId(classRef, (ReadOnlyValPtr<Byte>)ctx.Pointer, Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(1).NewObject(classRef, methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>());
			return jThrowable;
		}
		finally
		{
			JClassObject.GetClass<JErrorObject>(env).Dispose(); // Avoid GC disposing.
		}
	}
	private static JGlobal CreateGlobal(NativeInterfaceProxy proxyEnv, JLocalObject jLocal)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JGlobalRef globalRef = JNativeCallAdapterTests.fixture.Create<JGlobalRef>();
		proxyEnv.NewGlobalRef(jLocal.Reference).Returns(globalRef);
		proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);

		JGlobal jGlobal = jLocal.Global;

		Assert.Equal(globalRef, jGlobal.Reference);
		proxyEnv.Received(1).NewGlobalRef(jLocal.LocalReference);
		return jGlobal;
	}
	private static void FinalizeTest(NativeInterfaceProxy proxyEnv, CallResult result, JNativeCallAdapter adapter,
		JLocalObject? testObject = default, JObjectLocalRef localRef = default, JClassObject? testClass = default,
		JClassLocalRef classRef = default)
	{
		IEnvironment? env = adapter.Environment;
		switch (result)
		{
			case CallResult.Nested:
				JNativeCallAdapterTests.NestedAdapterTest(proxyEnv);
				adapter.FinalizeCall();
				break;
			case CallResult.NestedStatic when testClass is not null:
				JNativeCallAdapterTests.NestedStaticAdapterTest(proxyEnv, testClass);
				adapter.FinalizeCall();
				break;
			case CallResult.NestedStatic when testObject?.Class is not null:
				JNativeCallAdapterTests.NestedStaticAdapterTest(proxyEnv, testObject.Class);
				adapter.FinalizeCall();
				break;
			case CallResult.Array:
				using (JArrayObject jArray =
				       JNativeCallAdapterTests.CreateClassArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray));
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.PrimitiveArray:
				using (JArrayObject jArray =
				       JNativeCallAdapterTests.CreateBooleanArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray));
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.ObjectArray:
				using (JArrayObject<JClassObject> jArray =
				       JNativeCallAdapterTests.CreateClassArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.BooleanArray:
				using (JArrayObject<JBoolean> jArray =
				       JNativeCallAdapterTests.CreateBooleanArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.ByteArray:
				using (JArrayObject<JByte> jArray =
				       JNativeCallAdapterTests.CreateByteArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.CharArray:
				using (JArrayObject<JChar> jArray =
				       JNativeCallAdapterTests.CreateCharArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.DoubleArray:
				using (JArrayObject<JDouble> jArray =
				       JNativeCallAdapterTests.CreateDoubleArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.FloatArray:
				using (JArrayObject<JFloat> jArray =
				       JNativeCallAdapterTests.CreateFloatArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.IntArray:
				using (JArrayObject<JInt> jArray =
				       JNativeCallAdapterTests.CreateIntArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.LongArray:
				using (JArrayObject<JLong> jArray =
				       JNativeCallAdapterTests.CreateLongArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.ShortArray:
				using (JArrayObject<JShort> jArray =
				       JNativeCallAdapterTests.CreateShortArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.Class:
				Assert.Equal(proxyEnv.VirtualMachine.ClassGlobalRef.Value,
				             adapter.FinalizeCall(env.ClassFeature.ClassObject).Value);
				break;
			case CallResult.Object:
				using (JLocalObject jLocal = JNativeCallAdapterTests.CreateObject(proxyEnv))
				{
					Assert.Equal(jLocal.Reference, adapter.FinalizeCall(jLocal));
					Assert.True(jLocal.IsDefault);
				}
				break;
			case CallResult.Primitive:
				JDouble primitive = JNativeCallAdapterTests.fixture.Create<Double>();
				Assert.Equal(primitive, adapter.FinalizeCall(primitive));
				break;
			case CallResult.Throwable:
				using (JThrowableObject jThrowable = JNativeCallAdapterTests.CreateThrowable(proxyEnv))
				{
					Assert.Equal(jThrowable.Reference, adapter.FinalizeCall(jThrowable));
					Assert.True(jThrowable.IsDefault);
				}
				break;
			case CallResult.String:
				using (JStringObject jString = JNativeCallAdapterTests.CreateString(proxyEnv, "text"))
				{
					Assert.Equal(jString.Reference, adapter.FinalizeCall(jString));
					Assert.True(jString.IsDefault);
				}
				break;
			case CallResult.Global:
				using (JLocalObject jLocal = JNativeCallAdapterTests.CreateObject(proxyEnv))
				using (JGlobal jGlobal = JNativeCallAdapterTests.CreateGlobal(proxyEnv, jLocal))
				{
					Assert.Equal(jGlobal.Reference.Value, adapter.FinalizeCall(jGlobal.AsLocal<JLocalObject>(env)));
					Assert.False(jGlobal.IsDefault);
					Assert.True(jLocal.IsDefault);
				}
				break;
			case CallResult.Parameter when testClass is not null:
				Assert.Equal(classRef, adapter.FinalizeCall(testClass));
				break;
			case CallResult.Parameter when testObject is not null:
				Assert.Equal(localRef, adapter.FinalizeCall(testObject));
				break;
			case CallResult.Void:
			default:
				adapter.FinalizeCall();
				break;
		}
		Assert.True(JObject.IsNullOrDefault(testClass));
		Assert.True(JObject.IsNullOrDefault(testObject));
		Assert.True(JObject.IsNullOrDefault(testObject?.Class));
		JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
		JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
	}

	internal enum CallType
	{
		Instance,
		Static,
	}

	internal enum CallParameter
	{
		Object,
		Class,
		Throwable,
		ObjectArray,
		BooleanArray,
		ByteArray,
		CharArray,
		DoubleArray,
		FloatArray,
		IntArray,
		LongArray,
		ShortArray,
	}

	internal enum CallResult
	{
		Void,
		Primitive,
		Object,
		Class,
		Throwable,
		Global,
		Array,
		PrimitiveArray,
		ObjectArray,
		BooleanArray,
		ByteArray,
		CharArray,
		DoubleArray,
		FloatArray,
		IntArray,
		LongArray,
		ShortArray,
		String,
		Nested,
		NestedStatic,
		Parameter,
	}
}
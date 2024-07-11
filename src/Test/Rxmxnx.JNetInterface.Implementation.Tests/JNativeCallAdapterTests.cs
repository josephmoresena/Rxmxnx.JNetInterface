namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JNativeCallAdapterTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly WeakReference<ProxyFactory?> unknownParameterlessFactory = new(default);
	private static readonly WeakReference<ProxyFactory?> instanceParameterlessFactory = new(default);
	private static readonly WeakReference<ProxyFactory?> typedInstanceParameterlessFactory = new(default);
	private static readonly WeakReference<ProxyFactory?> staticParameterlessFactory = new(default);

	private static ProxyFactory UnknownParameterlessFactory
	{
		get
		{
			if (JNativeCallAdapterTests.unknownParameterlessFactory.TryGetTarget(out ProxyFactory? result))
				return result;
			result = new(50);
			JNativeCallAdapterTests.unknownParameterlessFactory.SetTarget(result);
			return result;
		}
	}
	private static ProxyFactory InstanceParameterlessFactory
	{
		get
		{
			if (JNativeCallAdapterTests.instanceParameterlessFactory.TryGetTarget(out ProxyFactory? result))
				return result;
			result = new(100);
			JNativeCallAdapterTests.instanceParameterlessFactory.SetTarget(result);
			return result;
		}
	}
	private static ProxyFactory TypedInstanceParameterlessFactory
	{
		get
		{
			if (JNativeCallAdapterTests.typedInstanceParameterlessFactory.TryGetTarget(out ProxyFactory? result))
				return result;
			result = new(100);
			JNativeCallAdapterTests.typedInstanceParameterlessFactory.SetTarget(result);
			return result;
		}
	}
	private static ProxyFactory StaticParameterlessFactory
	{
		get
		{
			if (JNativeCallAdapterTests.staticParameterlessFactory.TryGetTarget(out ProxyFactory? result))
				return result;
			result = new(50);
			JNativeCallAdapterTests.staticParameterlessFactory.SetTarget(result);
			return result;
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
	[InlineData(true, CallResult.String)]
	[InlineData(false, CallResult.String)]
	[InlineData(true, CallResult.Nested)]
	[InlineData(false, CallResult.Nested)]
	internal void UnknownParameterlessCallTest(Boolean useVm, CallResult result = CallResult.Void)
	{
		NativeInterfaceProxy proxyEnv =
			NativeInterfaceProxy.CreateProxy(JNativeCallAdapterTests.UnknownParameterlessFactory);
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
			IEnvironment? env = adapter.Environment;
			if (env is not null)
				switch (result)
				{
					case CallResult.Void:
						adapter.FinalizeCall();
						break;
					case CallResult.Nested:
						JNativeCallAdapterTests.NestedAdapterTest(proxyEnv);
						adapter.FinalizeCall();
						break;
					case CallResult.Array:
						using (JArrayObject jArray =
						       JNativeCallAdapterTests.CreateArray(proxyEnv, Random.Shared.Next(0, 10)))
						{
							Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray));
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
							Assert.Equal(jGlobal.Reference.Value,
							             adapter.FinalizeCall(jGlobal.AsLocal<JLocalObject>(env)));
							Assert.False(jGlobal.IsDefault);
							Assert.True(jLocal.IsDefault);
						}
						break;
				}
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
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
	[InlineData(true, CallResult.String, true)]
	[InlineData(false, CallResult.String, true)]
	[InlineData(true, CallResult.Nested, true)]
	[InlineData(false, CallResult.Nested, true)]
	[InlineData(true, CallResult.Parameter, true)]
	[InlineData(false, CallResult.Parameter, true)]
	internal void InstanceParameterlessCallTest(Boolean useVm, CallResult result = CallResult.Void,
		Boolean registerClass = false)
	{
		NativeInterfaceProxy proxyEnv =
			NativeInterfaceProxy.CreateProxy(JNativeCallAdapterTests.InstanceParameterlessFactory);
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
			IEnvironment? env = adapter.Environment;
			if (env is not null)
				switch (result)
				{
					case CallResult.Void:
						adapter.FinalizeCall();
						break;
					case CallResult.Nested:
						JNativeCallAdapterTests.NestedAdapterTest(proxyEnv);
						adapter.FinalizeCall();
						break;
					case CallResult.NestedStatic:
						JNativeCallAdapterTests.NestedStaticAdapterTest(proxyEnv, testObject?.Class);
						adapter.FinalizeCall();
						break;
					case CallResult.Array:
						using (JArrayObject jArray =
						       JNativeCallAdapterTests.CreateArray(proxyEnv, Random.Shared.Next(0, 10)))
						{
							Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray));
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
							Assert.Equal(jGlobal.Reference.Value,
							             adapter.FinalizeCall(jGlobal.AsLocal<JLocalObject>(env)));
							Assert.False(jGlobal.IsDefault);
							Assert.True(jLocal.IsDefault);
						}
						break;
					case CallResult.Parameter:
						Assert.Equal(localRef, adapter.FinalizeCall(testObject));
						break;
				}
			Assert.True(JObject.IsNullOrDefault(testObject));
			Assert.True(JObject.IsNullOrDefault(testObject?.Class));
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
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
		NativeInterfaceProxy proxyEnv =
			NativeInterfaceProxy.CreateProxy(JNativeCallAdapterTests.TypedInstanceParameterlessFactory);
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
			Assert.NotNull(MetadataHelper.GetMetadata(classTypeMetadata.Hash));
			IEnvironment? env = adapter.Environment;
			if (env is not null)
				switch (result)
				{
					case CallResult.Void:
						adapter.FinalizeCall();
						break;
					case CallResult.Nested:
						JNativeCallAdapterTests.NestedAdapterTest(proxyEnv);
						adapter.FinalizeCall();
						break;
					case CallResult.NestedStatic:
						JNativeCallAdapterTests.NestedStaticAdapterTest(proxyEnv, testObject?.Class);
						adapter.FinalizeCall();
						break;
					case CallResult.Array:
						using (JArrayObject jArray =
						       JNativeCallAdapterTests.CreateArray(proxyEnv, Random.Shared.Next(0, 10)))
						{
							Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray));
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
							Assert.Equal(jGlobal.Reference.Value,
							             adapter.FinalizeCall(jGlobal.AsLocal<JLocalObject>(env)));
							Assert.False(jGlobal.IsDefault);
							Assert.True(jLocal.IsDefault);
						}
						break;
					case CallResult.Parameter:
						Assert.Equal(localRef, adapter.FinalizeCall(testObject));
						break;
				}
			Assert.True(JObject.IsNullOrDefault(testObject));
			Assert.True(JObject.IsNullOrDefault(testObject?.Class));
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
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
		NativeInterfaceProxy proxyEnv =
			NativeInterfaceProxy.CreateProxy(JNativeCallAdapterTests.StaticParameterlessFactory);
		JNativeCallAdapter adapter = default;
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef clsStrRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JClassTypeMetadata classTypeMetadata = IClassType.GetMetadata<JTestObject>();
		JClassTypeMetadata classClassTypeMetadata = IClassType.GetMetadata<JClassObject>();
		JClassObject? jClass = default;
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
				                          proxyEnv.Reference, classRef, out jClass).Build() :
				JNativeCallAdapter.Create(proxyEnv.Reference, classRef, out jClass).Build();
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
			IEnvironment? env = adapter.Environment;
			if (env is not null)
				switch (result)
				{
					case CallResult.Void:
						adapter.FinalizeCall();
						break;
					case CallResult.Nested:
						JNativeCallAdapterTests.NestedAdapterTest(proxyEnv);
						adapter.FinalizeCall();
						break;
					case CallResult.NestedStatic:
						JNativeCallAdapterTests.NestedStaticAdapterTest(proxyEnv, jClass);
						adapter.FinalizeCall();
						break;
					case CallResult.Array:
						using (JArrayObject jArray =
						       JNativeCallAdapterTests.CreateArray(proxyEnv, Random.Shared.Next(0, 10)))
						{
							Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray));
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
							Assert.Equal(jGlobal.Reference.Value,
							             adapter.FinalizeCall(jGlobal.AsLocal<JLocalObject>(env)));
							Assert.False(jGlobal.IsDefault);
							Assert.True(jLocal.IsDefault);
						}
						break;
					case CallResult.Parameter:
						Assert.Equal(classRef, adapter.FinalizeCall(jClass));
						break;
				}
			Assert.True(JObject.IsNullOrDefault(jClass));
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
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
	private static JArrayObject CreateArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JObjectLocalRef localRef = JClassObject.GetClass<JClassObject>(env).Global.Reference.Value;
		JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
		JObjectArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JObjectArrayLocalRef>();
		proxyEnv.NewObjectArray(length, classRef, Arg.Any<JObjectLocalRef>()).Returns(arrayRef);
		JArrayObject result = JArrayObject<JClassObject>.Create(env, length);
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
		String,
		Nested,
		NestedStatic,
		Parameter,
	}
}
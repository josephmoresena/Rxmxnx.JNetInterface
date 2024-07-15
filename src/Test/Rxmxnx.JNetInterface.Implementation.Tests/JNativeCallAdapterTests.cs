namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed partial class JNativeCallAdapterTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	public static UInt32 MaxThreads => 0x800;

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
	private static void FinalizeFinalTypedObject(NativeInterfaceProxy proxyEnv, JObjectLocalRef localRef)
	{
		proxyEnv.Received(1).GetObjectRefType(localRef);
		proxyEnv.Received(0).GetObjectClass(Arg.Any<JObjectLocalRef>());
		proxyEnv.Received(0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
	}
	private static void FinalizeFinalArrayTypedParameterTest(NativeInterfaceProxy proxyEnv, JArrayLocalRef arrayRef)
	{
		JNativeCallAdapterTests.FinalizeFinalTypedObject(proxyEnv, arrayRef.Value);
		proxyEnv.Received(0).GetArrayLength(arrayRef);
	}
	private static JNativeCallAdapter CreateParameters(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, out List<JLocalObject?> parameters)
	{
		parameters = new(20);
		Span<JObjectLocalRef> refs = JNativeCallAdapterTests.fixture.CreateMany<JObjectLocalRef>(20).ToArray();

		parameters.Add(JNativeCallAdapterTests.CreateModule(proxyEnv, builder, refs[0]));
		parameters.Add(JNativeCallAdapterTests.CreateBooleanArray(proxyEnv, builder,
		                                                          refs[1]
			                                                          .Transform<JObjectLocalRef,
				                                                          JBooleanArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateByteArray(proxyEnv, builder,
		                                                       refs[2]
			                                                       .Transform<JObjectLocalRef, JByteArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateCharArray(proxyEnv, builder,
		                                                       refs[3]
			                                                       .Transform<JObjectLocalRef, JCharArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateDoubleArray(proxyEnv, builder,
		                                                         refs[4]
			                                                         .Transform<JObjectLocalRef,
				                                                         JDoubleArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateFloatArray(proxyEnv, builder,
		                                                        refs[5]
			                                                        .Transform<JObjectLocalRef,
				                                                        JFloatArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateIntArray(
			               proxyEnv, builder, refs[6].Transform<JObjectLocalRef, JIntArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateLongArray(proxyEnv, builder,
		                                                       refs[7]
			                                                       .Transform<JObjectLocalRef, JLongArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateShortArray(proxyEnv, builder,
		                                                        refs[8]
			                                                        .Transform<JObjectLocalRef,
				                                                        JShortArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateClassArray(proxyEnv, builder,
		                                                        refs[9]
			                                                        .Transform<JObjectLocalRef,
				                                                        JObjectArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateClassArray(proxyEnv, builder,
		                                                        refs[10]
			                                                        .Transform<JObjectLocalRef,
				                                                        JObjectArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateArray(proxyEnv, builder,
		                                                   refs[11].Transform<JObjectLocalRef, JArrayLocalRef>(),
		                                                   false));
		parameters.Add(JNativeCallAdapterTests.CreateArray(proxyEnv, builder,
		                                                   refs[12].Transform<JObjectLocalRef, JArrayLocalRef>(),
		                                                   true));
		parameters.Add(JNativeCallAdapterTests.CreateTestArray<JStringObject>(
			               proxyEnv, builder, refs[13].Transform<JObjectLocalRef, JArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateTestArray<JThrowableObject>(
			               proxyEnv, builder, refs[14].Transform<JObjectLocalRef, JArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateTestArray<JLong>(
			               proxyEnv, builder, refs[15].Transform<JObjectLocalRef, JArrayLocalRef>()));
		parameters.Add(JNativeCallAdapterTests.CreateString(
			               proxyEnv, builder, refs[16].Transform<JObjectLocalRef, JStringLocalRef>()));

		return builder.Build();
	}
	private static JModuleObject CreateModule(NativeInterfaceProxy proxyEnv, JNativeCallAdapter.Builder builder,
		in JObjectLocalRef localRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(localRef).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(localRef, out JModuleObject result);
			Assert.Equal(localRef, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JModuleObject>(), result.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalTypedObject(proxyEnv, localRef);
		}
	}
	private static JArrayObject<JBoolean> CreateBooleanArray(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JBooleanArrayLocalRef arrayRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(arrayRef, out JArrayObject<JBoolean> result);
			Assert.Equal(arrayRef.ArrayValue, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JArrayObject<JBoolean>>(), result.Object.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalArrayTypedParameterTest(proxyEnv, arrayRef.ArrayValue);
		}
	}
	private static JArrayObject<JByte> CreateByteArray(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JByteArrayLocalRef arrayRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(arrayRef, out JArrayObject<JByte> result);
			Assert.Equal(arrayRef.ArrayValue, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JArrayObject<JByte>>(), result.Object.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalArrayTypedParameterTest(proxyEnv, arrayRef.ArrayValue);
		}
	}
	private static JArrayObject<JChar> CreateCharArray(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JCharArrayLocalRef arrayRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(arrayRef, out JArrayObject<JChar> result);
			Assert.Equal(arrayRef.ArrayValue, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JArrayObject<JChar>>(), result.Object.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalArrayTypedParameterTest(proxyEnv, arrayRef.ArrayValue);
		}
	}
	private static JArrayObject<JDouble> CreateDoubleArray(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JDoubleArrayLocalRef arrayRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(arrayRef, out JArrayObject<JDouble> result);
			Assert.Equal(arrayRef.ArrayValue, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JArrayObject<JDouble>>(), result.Object.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalArrayTypedParameterTest(proxyEnv, arrayRef.ArrayValue);
		}
	}
	private static JArrayObject<JFloat> CreateFloatArray(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JFloatArrayLocalRef arrayRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(arrayRef, out JArrayObject<JFloat> result);
			Assert.Equal(arrayRef.ArrayValue, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JArrayObject<JFloat>>(), result.Object.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalArrayTypedParameterTest(proxyEnv, arrayRef.ArrayValue);
		}
	}
	private static JArrayObject<JInt> CreateIntArray(NativeInterfaceProxy proxyEnv, JNativeCallAdapter.Builder builder,
		JIntArrayLocalRef arrayRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(arrayRef, out JArrayObject<JInt> result);
			Assert.Equal(arrayRef.ArrayValue, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JArrayObject<JInt>>(), result.Object.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalArrayTypedParameterTest(proxyEnv, arrayRef.ArrayValue);
		}
	}
	private static JArrayObject<JLong> CreateLongArray(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JLongArrayLocalRef arrayRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(arrayRef, out JArrayObject<JLong> result);
			Assert.Equal(arrayRef.ArrayValue, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JArrayObject<JLong>>(), result.Object.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalArrayTypedParameterTest(proxyEnv, arrayRef.ArrayValue);
		}
	}
	private static JArrayObject<JShort> CreateShortArray(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JShortArrayLocalRef arrayRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(arrayRef, out JArrayObject<JShort> result);
			Assert.Equal(arrayRef.ArrayValue, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JArrayObject<JShort>>(), result.Object.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalArrayTypedParameterTest(proxyEnv, arrayRef.ArrayValue);
		}
	}
	private static JArrayObject<JClassObject> CreateClassArray(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JObjectArrayLocalRef arrayRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(arrayRef, out JArrayObject<JClassObject> result);
			Assert.Equal(arrayRef.ArrayValue, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JArrayObject<JClassObject>>(), result.Object.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalArrayTypedParameterTest(proxyEnv, arrayRef.ArrayValue);
		}
	}
	private static JArrayObject CreateArray(NativeInterfaceProxy proxyEnv, JNativeCallAdapter.Builder builder,
		JArrayLocalRef arrayRef, Boolean primitive)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JArrayTypeMetadata arrayTypeMetadata =
			primitive ? JArrayObject<JInt>.Metadata : JArrayObject<JClassObject>.Metadata;

		try
		{
			proxyEnv.GetObjectClass(arrayRef.Value).Returns(classRef);
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);
			using IReadOnlyFixedContext<Char>.IDisposable ctx = arrayTypeMetadata.Information.ToString().AsMemory()
				.GetFixedContext();
			proxyEnv.GetStringUtfLength(strRef).Returns(arrayTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>()).Returns((ReadOnlyValPtr<Byte>)ctx.Pointer);

			_ = builder.WithParameter(arrayRef, out JArrayObject result);
			Assert.Equal(arrayRef, result.Reference);
			Assert.Equal(arrayTypeMetadata.GetClass(result.Environment), result.Class);
			return result;
		}
		finally
		{
			proxyEnv.Received(1).GetObjectRefType(arrayRef.Value);
			proxyEnv.Received(1).GetObjectClass(Arg.Any<JObjectLocalRef>());
			proxyEnv.Received(1).GetStringUtfLength(strRef);
			proxyEnv.Received(1).GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(0).GetArrayLength(arrayRef);
		}
	}
	private static JArrayObject<TDataType> CreateTestArray<TDataType>(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JArrayLocalRef arrayRef) where TDataType : IObject, IDataType<TDataType>
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JArrayTypeMetadata arrayTypeMetadata = JArrayObject<TDataType>.Metadata;
		Boolean final = arrayTypeMetadata.Modifier == JTypeModifier.Final;

		try
		{
			proxyEnv.GetObjectClass(arrayRef.Value).Returns(classRef);
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);
			using IReadOnlyFixedContext<Char>.IDisposable ctx = arrayTypeMetadata.Information.ToString().AsMemory()
				.GetFixedContext();
			proxyEnv.GetStringUtfLength(strRef).Returns(arrayTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>()).Returns((ReadOnlyValPtr<Byte>)ctx.Pointer);

			_ = builder.WithParameter(arrayRef, out JArrayObject<TDataType> result);
			Assert.Equal(arrayRef, result.Reference);
			Assert.Equal(arrayTypeMetadata.GetClass(result.Environment), result.Object.Class);
			return result;
		}
		finally
		{
			proxyEnv.Received(1).GetObjectRefType(arrayRef.Value);
			proxyEnv.Received(final ? 0 : 1).GetObjectClass(Arg.Any<JObjectLocalRef>());
			proxyEnv.Received(final ? 0 : 1).GetStringUtfLength(strRef);
			proxyEnv.Received(final ? 0 : 1).GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(final ? 0 : 1).CallObjectMethod(classRef.Value,
			                                                  proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                                  ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).GetSuperclass(Arg.Any<JClassLocalRef>());
			proxyEnv.Received(0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(0).GetArrayLength(arrayRef);
		}
	}
	private static JStringObject CreateString(NativeInterfaceProxy proxyEnv, JNativeCallAdapter.Builder builder,
		JStringLocalRef stringRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(stringRef.Value).Returns(JReferenceType.LocalRefType);

			_ = builder.WithParameter(stringRef, out JStringObject result);
			Assert.Equal(stringRef, result.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JStringObject>(), result.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalTypedObject(proxyEnv, stringRef.Value);
			proxyEnv.Received(0).GetStringLength(stringRef);
		}
	}
}
namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed partial class JNativeCallAdapterTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	private static void NestedAdapterTest(NativeInterfaceProxy proxyEnv)
	{
		using JStringObject jString = TestUtilities.CreateString(proxyEnv, "Sample text");
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
		using IFixedPointer.IDisposable classCtx =
			classClassTypeMetadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameNameCtx);
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
		        .Returns((ReadOnlyValPtr<Byte>)nameNameCtx.Pointer);

		JNativeCallAdapter adapter =
			JNativeCallAdapter.Create(proxyEnv.Reference, classRef, out JClassObject jClass2).Build();
		Assert.Equal(!orClasRef.IsDefault && classRef != orClasRef, classRef != jClass.Reference);
		Assert.True(Object.ReferenceEquals(jClass, jClass2));
		Assert.Equal(jClass.Reference, adapter.FinalizeCall(jClass2));
		Assert.Equal(orClasRef.IsDefault, JObject.IsNullOrDefault(jClass));
		nameNameCtx.Dispose();
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
				using (JArrayObject jArray = TestUtilities.CreateClassArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray));
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.PrimitiveArray:
				using (JArrayObject jArray = TestUtilities.CreateBooleanArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray));
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.ObjectArray:
				using (JArrayObject<JClassObject> jArray =
				       TestUtilities.CreateClassArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.BooleanArray:
				using (JArrayObject<JBoolean> jArray =
				       TestUtilities.CreateBooleanArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.ByteArray:
				using (JArrayObject<JByte> jArray = TestUtilities.CreateByteArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.CharArray:
				using (JArrayObject<JChar> jArray = TestUtilities.CreateCharArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.DoubleArray:
				using (JArrayObject<JDouble> jArray =
				       TestUtilities.CreateDoubleArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.FloatArray:
				using (JArrayObject<JFloat> jArray =
				       TestUtilities.CreateFloatArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.IntArray:
				using (JArrayObject<JInt> jArray = TestUtilities.CreateIntArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.LongArray:
				using (JArrayObject<JLong> jArray = TestUtilities.CreateLongArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.ShortArray:
				using (JArrayObject<JShort> jArray =
				       TestUtilities.CreateShortArray(proxyEnv, Random.Shared.Next(0, 10)))
				{
					Assert.Equal(jArray.Reference, adapter.FinalizeCall(jArray).ArrayValue);
					Assert.True(jArray.IsDefault);
				}
				break;
			case CallResult.Class:
				Assert.Equal(proxyEnv.VirtualMachine.ClassGlobalRef.Value,
				             adapter.FinalizeCall(env.ClassFeature.ClassObject).Value);
				break;
			case CallResult.Serializable:
				Assert.Equal(proxyEnv.VirtualMachine.ClassGlobalRef.Value,
				             adapter.FinalizeCall(env.ClassFeature.ClassObject.CastTo<JSerializableObject>()));
				break;
			case CallResult.Object:
				using (JLocalObject jLocal = TestUtilities.CreateObject(proxyEnv))
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
				using (JThrowableObject jThrowable = TestUtilities.CreateThrowable(proxyEnv))
				{
					Assert.Equal(jThrowable.Reference, adapter.FinalizeCall(jThrowable));
					Assert.True(jThrowable.IsDefault);
				}
				break;
			case CallResult.String:
				using (JStringObject jString = TestUtilities.CreateString(proxyEnv, "text"))
				{
					Assert.Equal(jString.Reference, adapter.FinalizeCall(jString));
					Assert.True(jString.IsDefault);
				}
				break;
			case CallResult.Global:
				using (JLocalObject jLocal = TestUtilities.CreateObject(proxyEnv))
				using (JGlobal jGlobal = TestUtilities.CreateGlobal(proxyEnv, jLocal))
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
		GC.Collect();
		GC.WaitForPendingFinalizers();
		JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
		proxyEnv.FinalizeProxy(true);
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
		                                                          JBooleanArrayLocalRef.FromReference(in refs[1])));
		parameters.Add(JNativeCallAdapterTests.CreateByteArray(proxyEnv, builder,
		                                                       JByteArrayLocalRef.FromReference(in refs[2])));
		parameters.Add(JNativeCallAdapterTests.CreateCharArray(proxyEnv, builder,
		                                                       JCharArrayLocalRef.FromReference(in refs[3])));
		parameters.Add(JNativeCallAdapterTests.CreateDoubleArray(proxyEnv, builder,
		                                                         JDoubleArrayLocalRef.FromReference(in refs[4])));
		parameters.Add(JNativeCallAdapterTests.CreateFloatArray(proxyEnv, builder,
		                                                        JFloatArrayLocalRef.FromReference(in refs[5])));
		parameters.Add(JNativeCallAdapterTests.CreateIntArray(
			               proxyEnv, builder, JIntArrayLocalRef.FromReference(in refs[6])));
		parameters.Add(JNativeCallAdapterTests.CreateLongArray(proxyEnv, builder,
		                                                       JLongArrayLocalRef.FromReference(in refs[7])));
		parameters.Add(JNativeCallAdapterTests.CreateShortArray(proxyEnv, builder,
		                                                        JShortArrayLocalRef.FromReference(in refs[8])));
		parameters.Add(JNativeCallAdapterTests.CreateClassArray(proxyEnv, builder,
		                                                        JObjectArrayLocalRef.FromReference(in refs[9])));
		parameters.Add(JNativeCallAdapterTests.CreateClassArray(proxyEnv, builder,
		                                                        JObjectArrayLocalRef.FromReference(in refs[10])));
		parameters.Add(JNativeCallAdapterTests.CreateArray(proxyEnv, builder,
		                                                   JArrayLocalRef.FromReference(in refs[11]), false));
		parameters.Add(JNativeCallAdapterTests.CreateArray(proxyEnv, builder,
		                                                   JArrayLocalRef.FromReference(in refs[12]), true));
		parameters.Add(JNativeCallAdapterTests.CreateTestArray<JStringObject>(
			               proxyEnv, builder, JArrayLocalRef.FromReference(in refs[13])));
		parameters.Add(JNativeCallAdapterTests.CreateTestArray<JThrowableObject>(
			               proxyEnv, builder, JArrayLocalRef.FromReference(in refs[14])));
		parameters.Add(JNativeCallAdapterTests.CreateTestArray<JLong>(
			               proxyEnv, builder, JArrayLocalRef.FromReference(in refs[15])));
		parameters.Add(JNativeCallAdapterTests.CreateString(
			               proxyEnv, builder, JStringLocalRef.FromReference(in refs[16])));
		parameters.Add(
			JNativeCallAdapterTests.CreateThrowable(proxyEnv, builder, JThrowableLocalRef.FromReference(in refs[17])));
		parameters.Add(
			JNativeCallAdapterTests.CreateTypedThrowable<JRuntimeExceptionObject>(
				proxyEnv, builder, JThrowableLocalRef.FromReference(in refs[18])));

		Int32 actualCount = parameters.Count;
		for (Int32 i = 0; i < actualCount; i++)
		{
			JLocalObject? jLocal0 = parameters[i];
			JLocalObject jLocal1 =
				JNativeCallAdapterTests.CreateDuplicatedParameter(proxyEnv, builder, jLocal0?.Class, refs[i]);
			Assert.False(Object.ReferenceEquals(jLocal0, jLocal1));
			Assert.Equal(jLocal0?.Lifetime, jLocal1.Lifetime);
			Assert.Equal(jLocal0?.GetType(), jLocal1.GetType());
			parameters.Add(jLocal1);
		}

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

			_ = builder.WithParameter(localRef, out JModuleObject? result);
			Assert.Equal(localRef, result!.Reference);
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

			_ = builder.WithParameter(arrayRef, out JArrayObject<JBoolean>? result);
			Assert.Equal(arrayRef.ArrayValue, result!.Reference);
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

			_ = builder.WithParameter(arrayRef, out JArrayObject<JByte>? result);
			Assert.Equal(arrayRef.ArrayValue, result!.Reference);
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

			_ = builder.WithParameter(arrayRef, out JArrayObject<JChar>? result);
			Assert.Equal(arrayRef.ArrayValue, result!.Reference);
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

			_ = builder.WithParameter(arrayRef, out JArrayObject<JDouble>? result);
			Assert.Equal(arrayRef.ArrayValue, result!.Reference);
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

			_ = builder.WithParameter(arrayRef, out JArrayObject<JFloat>? result);
			Assert.Equal(arrayRef.ArrayValue, result!.Reference);
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

			_ = builder.WithParameter(arrayRef, out JArrayObject<JInt>? result);
			Assert.Equal(arrayRef.ArrayValue, result!.Reference);
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

			_ = builder.WithParameter(arrayRef, out JArrayObject<JLong>? result);
			Assert.Equal(arrayRef.ArrayValue, result!.Reference);
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

			_ = builder.WithParameter(arrayRef, out JArrayObject<JShort>? result);
			Assert.Equal(arrayRef.ArrayValue, result!.Reference);
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

			_ = builder.WithParameter(arrayRef, out JArrayObject<JClassObject>? result);
			Assert.Equal(arrayRef.ArrayValue, result!.Reference);
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
		using IFixedPointer.IDisposable ctx =
			arrayTypeMetadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);

		try
		{
			proxyEnv.GetObjectClass(arrayRef.Value).Returns(classRef);
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetStringUtfLength(strRef).Returns(arrayTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);

			_ = builder.WithParameter(arrayRef, out JArrayObject? result);
			Assert.Equal(arrayRef, result!.Reference);
			Assert.Equal(arrayTypeMetadata.GetClass(result.Environment), result.Class);
			return result;
		}
		finally
		{
			nameCtx.Dispose();
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
		JNativeCallAdapter.Builder builder, JArrayLocalRef arrayRef) where TDataType : IDataType<TDataType>
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JArrayTypeMetadata arrayTypeMetadata = JArrayObject<TDataType>.Metadata;
		Boolean final = arrayTypeMetadata.Modifier == JTypeModifier.Final;

		using IFixedPointer.IDisposable ctx =
			arrayTypeMetadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);

		try
		{
			proxyEnv.GetObjectClass(arrayRef.Value).Returns(classRef);
			proxyEnv.GetObjectRefType(arrayRef.Value).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetStringUtfLength(strRef).Returns(arrayTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);

			_ = builder.WithParameter(arrayRef, out JArrayObject<TDataType>? result);
			Assert.Equal(arrayRef, result!.Reference);
			Assert.Equal(arrayTypeMetadata.GetClass(result.Environment), result.Object.Class);
			return result;
		}
		finally
		{
			nameCtx.Dispose();
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

			_ = builder.WithParameter(stringRef, out JStringObject? result);
			Assert.Equal(stringRef, result!.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JStringObject>(), result.Class);
			return result;
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeFinalTypedObject(proxyEnv, stringRef.Value);
			proxyEnv.Received(0).GetStringLength(stringRef);
		}
	}
	private static JThrowableObject CreateThrowable(NativeInterfaceProxy proxyEnv, JNativeCallAdapter.Builder builder,
		JThrowableLocalRef throwableRef)
	{
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JClassTypeMetadata classTypeMetadata = IClassType.GetMetadata<JThrowableObject>();
		Boolean final = classTypeMetadata.Modifier == JTypeModifier.Final;
		using IFixedPointer.IDisposable ctx =
			classTypeMetadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(throwableRef.Value).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetObjectClass(throwableRef.Value).Returns(classRef);
			proxyEnv.GetStringUtfLength(strRef).Returns(classTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);

			_ = builder.WithParameter(throwableRef, out JThrowableObject? result);
			Assert.Equal(throwableRef, result!.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<JThrowableObject>(), result.Class);
			return result;
		}
		finally
		{
			nameCtx.Dispose();
			proxyEnv.Received(1).GetObjectRefType(throwableRef.Value);
			proxyEnv.Received(final ? 0 : 1).GetObjectClass(Arg.Any<JObjectLocalRef>());
			proxyEnv.Received(final ? 0 : 1).GetStringUtfLength(strRef);
			proxyEnv.Received(final ? 0 : 1).GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(final ? 0 : 1).CallObjectMethod(classRef.Value,
			                                                  proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                                  ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).GetSuperclass(Arg.Any<JClassLocalRef>());
			proxyEnv.Received(0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
		}
	}
	private static JThrowableObject CreateTypedThrowable<TThrowable>(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JThrowableLocalRef throwableRef)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JClassTypeMetadata classTypeMetadata = IClassType.GetMetadata<TThrowable>();
		Boolean final = classTypeMetadata.Modifier == JTypeModifier.Final;
		using IFixedPointer.IDisposable ctx =
			classTypeMetadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			proxyEnv.GetObjectRefType(throwableRef.Value).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetObjectClass(throwableRef.Value).Returns(classRef);
			proxyEnv.GetStringUtfLength(strRef).Returns(classTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);

			_ = builder.WithParameter(throwableRef, out TThrowable? result);
			Assert.Equal(throwableRef, result!.Reference);
			Assert.Equal(result.Environment.ClassFeature.GetClass<TThrowable>(), result.Class);
			return result;
		}
		finally
		{
			nameCtx.Dispose();
			proxyEnv.Received(1).GetObjectRefType(throwableRef.Value);
			proxyEnv.Received(final ? 0 : 1).GetObjectClass(Arg.Any<JObjectLocalRef>());
			proxyEnv.Received(final ? 0 : 1).GetStringUtfLength(strRef);
			proxyEnv.Received(final ? 0 : 1).GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(final ? 0 : 1).CallObjectMethod(classRef.Value,
			                                                  proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                                  ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).GetSuperclass(Arg.Any<JClassLocalRef>());
			proxyEnv.Received(0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
		}
	}
	private static JLocalObject CreateDuplicatedParameter(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, JClassObject? jClass, JObjectLocalRef localRef)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		try
		{
			_ = builder.WithParameter(localRef, out JLocalObject? result);
			Assert.Equal(localRef, result!.Reference);
			Assert.Equal(jClass, result.Class);
			return result;
		}
		finally
		{
			proxyEnv.Received(0).GetObjectRefType(localRef);
			proxyEnv.Received(0).GetObjectClass(Arg.Any<JObjectLocalRef>());
			proxyEnv.Received(0).GetSuperclass(Arg.Any<JClassLocalRef>());
			proxyEnv.Received(0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
		}
	}
}
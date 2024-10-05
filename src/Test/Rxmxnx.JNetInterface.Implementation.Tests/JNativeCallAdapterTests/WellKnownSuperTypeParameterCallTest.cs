namespace Rxmxnx.JNetInterface.Tests;

public partial class JNativeCallAdapterTests
{
	[Fact]
	internal void WellKnownSuperTypeParameterCallTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
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

			parameters.Add(JNativeCallAdapterTests.CreateErrorParameter(proxyEnv, builder, true));
			parameters.Add(JNativeCallAdapterTests.CreateErrorParameter(proxyEnv, builder, false));
			parameters.Add(JNativeCallAdapterTests.CreateProxyParameter(proxyEnv, builder, true));
			parameters.Add(JNativeCallAdapterTests.CreateProxyParameter(proxyEnv, builder, false));
			parameters.Add(JNativeCallAdapterTests.CreateErrorArrayParameter(proxyEnv, builder, true));
			parameters.Add(JNativeCallAdapterTests.CreateErrorArrayParameter(proxyEnv, builder, false));
			parameters.Add(JNativeCallAdapterTests.CreateInterfaceArrayParameter(proxyEnv, builder, true));
			parameters.Add(JNativeCallAdapterTests.CreateInterfaceArrayParameter(proxyEnv, builder, false));
			parameters.Add(JNativeCallAdapterTests.CreateEnumArrayParameter(proxyEnv, builder, true));
			parameters.Add(JNativeCallAdapterTests.CreateEnumArrayParameter(proxyEnv, builder, false));
			adapter = builder.Build();
			Assert.Equal(vm.GetEnvironment(), adapter.Environment);
		}
		finally
		{
			JNativeCallAdapterTests.FinalizeTest(proxyEnv, CallResult.Void, adapter);
			Assert.All(parameters, o => JObject.IsNullOrDefault(o));
		}
	}

	private static JErrorObject CreateErrorParameter(NativeInterfaceProxy proxyEnv, JNativeCallAdapter.Builder builder,
		Boolean initial)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JModifierObject.Modifiers modifiers = JModifierObject.Modifiers.Public | JModifierObject.Modifiers.Final;
		JObjectLocalRef localRef = JNativeCallAdapterTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef superClassRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef0 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef strRef1 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		TypeInfoSequence classInformation =
			MetadataHelper.GetClassInformation("rxmxnx/jnetinterface/test/SubError"u8, false);
		JClassTypeMetadata errorTypeMetadata = IClassType.GetMetadata<JErrorObject>();
		List<IFixedPointer.IDisposable> pointers =
		[
			classInformation.GetFixedPointer(),
			errorTypeMetadata.Information.GetFixedPointer(),
		];

		try
		{
			proxyEnv.GetObjectRefType(localRef).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetObjectClass(localRef).Returns(classRef);
			proxyEnv.GetSuperclass(classRef).Returns(superClassRef);
			proxyEnv.GetStringUtfLength(strRef0).Returns(classInformation.ClassName.Length);
			proxyEnv.GetStringUtfLength(strRef1).Returns(errorTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef0.Value);
			proxyEnv.CallObjectMethod(superClassRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef1.Value);
			proxyEnv.GetStringUtfChars(strRef0, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[0].Pointer);
			proxyEnv.GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[1].Pointer);
			proxyEnv.CallIntMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                       ReadOnlyValPtr<JValueWrapper>.Zero).Returns((Int32)modifiers);
			proxyEnv.CallBooleanMethod(classRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                           ReadOnlyValPtr<JValueWrapper>.Zero).Returns(false);

			_ = builder.WithParameter(localRef, out JLocalObject result);

			return Assert.IsAssignableFrom<JErrorObject>(result);
		}
		finally
		{
			proxyEnv.Received(1).GetObjectRefType(localRef);
			proxyEnv.Received(1).GetObjectClass(localRef);
			proxyEnv.Received(initial ? 1 : 0).GetSuperclass(classRef);
			proxyEnv.Received(1).GetStringUtfLength(strRef0);
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfLength(strRef1);
			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).CallObjectMethod(superClassRef.Value,
			                                                    proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                                    ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).GetStringUtfChars(strRef0, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).CallIntMethod(classRef.Value,
			                                                 proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                                 ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallIntMethod(superClassRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                   ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(classRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(superClassRef.Value,
			                                       proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);

			pointers.ForEach(f => f.Dispose());
		}
	}
	private static JProxyObject CreateProxyParameter(NativeInterfaceProxy proxyEnv, JNativeCallAdapter.Builder builder,
		Boolean initial)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JModifierObject.Modifiers modifiers = JModifierObject.Modifiers.Public | JModifierObject.Modifiers.Final;
		JObjectLocalRef localRef = JNativeCallAdapterTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef superClassRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef interfaceClassRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef0 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef strRef1 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef strRef2 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JObjectArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JObjectArrayLocalRef>();
		TypeInfoSequence classInformation = MetadataHelper.GetClassInformation("jdk/proxy2/$Proxy1000"u8, false);
		JClassTypeMetadata proxyTypeMetadata = IClassType.GetMetadata<JProxyObject>();
		JInterfaceTypeMetadata interfaceTypeMetadata = IInterfaceType.GetMetadata<JSerializableObject>();
		List<IFixedPointer.IDisposable> pointers =
		[
			classInformation.GetFixedPointer(),
			proxyTypeMetadata.Information.GetFixedPointer(),
			interfaceTypeMetadata.Information.GetFixedPointer(),
		];

		try
		{
			proxyEnv.GetObjectRefType(localRef).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetObjectClass(localRef).Returns(classRef);
			proxyEnv.GetSuperclass(classRef).Returns(superClassRef);
			proxyEnv.GetStringUtfLength(strRef0).Returns(classInformation.ClassName.Length);
			proxyEnv.GetStringUtfLength(strRef1).Returns(proxyTypeMetadata.ClassName.Length);
			proxyEnv.GetStringUtfLength(strRef2).Returns(interfaceTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef0.Value);
			proxyEnv.CallObjectMethod(superClassRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef1.Value);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetInterfacesMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(arrayRef.Value);
			proxyEnv.GetArrayLength(arrayRef.ArrayValue).Returns(1);
			proxyEnv.GetObjectArrayElement(arrayRef, 0).Returns(interfaceClassRef.Value);
			proxyEnv.CallObjectMethod(interfaceClassRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef2.Value);
			proxyEnv.GetStringUtfChars(strRef0, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[0].Pointer);
			proxyEnv.GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[1].Pointer);
			proxyEnv.GetStringUtfChars(strRef2, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[2].Pointer);
			proxyEnv.CallIntMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                       ReadOnlyValPtr<JValueWrapper>.Zero).Returns((Int32)modifiers);
			proxyEnv.CallBooleanMethod(classRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                           ReadOnlyValPtr<JValueWrapper>.Zero).Returns(false);

			_ = builder.WithParameter(localRef, out JLocalObject result);

			return Assert.IsAssignableFrom<JProxyObject>(
				Assert.IsAssignableFrom<IInterfaceObject<JSerializableObject>>(result));
		}
		finally
		{
			proxyEnv.Received(1).GetObjectRefType(localRef);
			proxyEnv.Received(1).GetObjectClass(localRef);
			proxyEnv.Received(initial ? 1 : 0).GetSuperclass(classRef);
			proxyEnv.Received(1).GetStringUtfLength(strRef0);
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfLength(strRef1);
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfLength(strRef2);
			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).CallObjectMethod(superClassRef.Value,
			                                                    proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                                    ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).CallObjectMethod(classRef.Value,
			                                                    proxyEnv.VirtualMachine.ClassGetInterfacesMethodId,
			                                                    ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).GetArrayLength(arrayRef.ArrayValue);
			proxyEnv.Received(initial ? 1 : 0).GetObjectArrayElement(arrayRef, 0);
			proxyEnv.Received(initial ? 1 : 0).CallObjectMethod(interfaceClassRef.Value,
			                                                    proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                                    ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).GetStringUtfChars(strRef0, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfChars(strRef2, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).CallIntMethod(classRef.Value,
			                                                 proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                                 ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallIntMethod(superClassRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                   ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(classRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(superClassRef.Value,
			                                       proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(superClassRef.Value,
			                                       proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);

			pointers.ForEach(f => f.Dispose());
		}
	}
	private static JArrayObject CreateErrorArrayParameter(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, Boolean initial)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JModifierObject.Modifiers arrayModifier = JModifierObject.Modifiers.Public | JModifierObject.Modifiers.Final |
			JModifierObject.Modifiers.Abstract;
		JModifierObject.Modifiers elementModifier = JModifierObject.Modifiers.Public | JModifierObject.Modifiers.Final;
		JObjectLocalRef localRef = JNativeCallAdapterTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef elementClassRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef elementSuperClassRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef0 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef strRef1 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		CString arrayClassName = new(() => "[[[[Lrxmxnx/jnetinterface/test/SubErrorA;"u8);
		CString elementName = arrayClassName[5..^1];
		TypeInfoSequence classInformation = MetadataHelper.GetClassInformation(arrayClassName, false);
		JClassTypeMetadata errorTypeMetadata = IClassType.GetMetadata<JErrorObject>();
		List<IFixedPointer.IDisposable> pointers =
		[
			classInformation.GetFixedPointer(),
			errorTypeMetadata.Information.GetFixedPointer(),
		];

		try
		{
			proxyEnv.GetObjectRefType(localRef).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetObjectClass(localRef).Returns(classRef);
			proxyEnv.FindClass(Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
			{
				ReadOnlyValPtr<Byte> ptr = (ReadOnlyValPtr<Byte>)c[0];
				ReadOnlySpan<Byte> className = ptr.GetByteSpan();
				return className.SequenceEqual(elementName) ? elementClassRef : default;
			});
			proxyEnv.GetSuperclass(elementClassRef).Returns(elementSuperClassRef);
			proxyEnv.GetStringUtfLength(strRef0).Returns(classInformation.ClassName.Length);
			proxyEnv.GetStringUtfLength(strRef1).Returns(errorTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef0.Value);
			proxyEnv.CallObjectMethod(elementSuperClassRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef1.Value);
			proxyEnv.GetStringUtfChars(strRef0, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[0].Pointer);
			proxyEnv.GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[1].Pointer);
			proxyEnv.CallIntMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                       ReadOnlyValPtr<JValueWrapper>.Zero).Returns((Int32)arrayModifier);
			proxyEnv.CallIntMethod(elementClassRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                       ReadOnlyValPtr<JValueWrapper>.Zero).Returns((Int32)elementModifier);
			proxyEnv.CallBooleanMethod(elementClassRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                           ReadOnlyValPtr<JValueWrapper>.Zero).Returns(false);
			proxyEnv.Received(0).CallBooleanMethod(elementSuperClassRef.Value,
			                                       proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);

			_ = builder.WithParameter(localRef, out JLocalObject result);
			JArrayObject jArray = Assert.IsAssignableFrom<JArrayObject>(result);
			Assert.Equal(JArrayObject<JArrayObject<JArrayObject<JArrayObject<JErrorObject>>>>.Metadata,
			             jArray.TypeMetadata);
			return jArray;
		}
		finally
		{
			proxyEnv.Received(1).GetObjectRefType(localRef);
			proxyEnv.Received(1).GetObjectClass(localRef);
			proxyEnv.Received(initial ? 1 : 0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(initial ? 1 : 0).GetSuperclass(elementClassRef);
			proxyEnv.Received(1).GetStringUtfLength(strRef0);
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfLength(strRef1);
			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallObjectMethod(elementClassRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).CallObjectMethod(elementSuperClassRef.Value,
			                                                    proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                                    ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).CallIntMethod(classRef.Value,
			                                                 proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                                 ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).CallIntMethod(elementClassRef.Value,
			                                                 proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                                 ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallIntMethod(elementSuperClassRef.Value,
			                                   proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                   ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(classRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(elementClassRef.Value,
			                                       proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);

			pointers.ForEach(f => f.Dispose());
		}
	}
	private static JArrayObject CreateInterfaceArrayParameter(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, Boolean initial)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JModifierObject.Modifiers arrayModifier = JModifierObject.Modifiers.Public | JModifierObject.Modifiers.Final |
			JModifierObject.Modifiers.Abstract;
		JModifierObject.Modifiers elementModifier = JModifierObject.Modifiers.Public |
			JModifierObject.Modifiers.Interface | JModifierObject.Modifiers.Abstract;
		JObjectLocalRef localRef = JNativeCallAdapterTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef elementClassRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef interfaceClassRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef0 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef strRef1 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JObjectArrayLocalRef arrayRef = JNativeCallAdapterTests.fixture.Create<JObjectArrayLocalRef>();
		CString arrayClassName = new(() => "[[[[Lrxmxnx/jnetinterface/test/InterfaceTest;"u8);
		CString elementName = arrayClassName[5..^1];
		TypeInfoSequence classInformation = MetadataHelper.GetClassInformation(arrayClassName, false);
		JInterfaceTypeMetadata serializableTypeMetadata = IInterfaceType.GetMetadata<JSerializableObject>();
		List<IFixedPointer.IDisposable> pointers =
		[
			classInformation.GetFixedPointer(),
			serializableTypeMetadata.Information.GetFixedPointer(),
		];

		try
		{
			proxyEnv.GetObjectRefType(localRef).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetObjectClass(localRef).Returns(classRef);
			proxyEnv.FindClass(Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
			{
				ReadOnlyValPtr<Byte> ptr = (ReadOnlyValPtr<Byte>)c[0];
				ReadOnlySpan<Byte> className = ptr.GetByteSpan();
				return className.SequenceEqual(elementName) ? elementClassRef : default;
			});
			proxyEnv.CallObjectMethod(elementClassRef.Value, proxyEnv.VirtualMachine.ClassGetInterfacesMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(arrayRef.Value);
			proxyEnv.GetArrayLength(arrayRef.ArrayValue).Returns(1);
			proxyEnv.GetObjectArrayElement(arrayRef, 0).Returns(interfaceClassRef.Value);
			proxyEnv.GetStringUtfLength(strRef0).Returns(classInformation.ClassName.Length);
			proxyEnv.GetStringUtfLength(strRef1).Returns(serializableTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef0.Value);
			proxyEnv.CallObjectMethod(interfaceClassRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef1.Value);
			proxyEnv.GetStringUtfChars(strRef0, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[0].Pointer);
			proxyEnv.GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[1].Pointer);
			proxyEnv.CallIntMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                       ReadOnlyValPtr<JValueWrapper>.Zero).Returns((Int32)arrayModifier);
			proxyEnv.CallIntMethod(elementClassRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                       ReadOnlyValPtr<JValueWrapper>.Zero).Returns((Int32)elementModifier);
			proxyEnv.CallBooleanMethod(elementClassRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                           ReadOnlyValPtr<JValueWrapper>.Zero).Returns(false);
			proxyEnv.Received(0).CallBooleanMethod(interfaceClassRef.Value,
			                                       proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);

			_ = builder.WithParameter(localRef, out JLocalObject result);
			JArrayObject jArray = Assert.IsAssignableFrom<JArrayObject>(result);
			Assert.Equal(JArrayObject<JArrayObject<JArrayObject<JArrayObject<JSerializableObject>>>>.Metadata,
			             jArray.TypeMetadata);
			return jArray;
		}
		finally
		{
			proxyEnv.Received(1).GetObjectRefType(localRef);
			proxyEnv.Received(1).GetObjectClass(localRef);
			proxyEnv.Received(initial ? 1 : 0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(0).GetSuperclass(elementClassRef);
			proxyEnv.Received(initial ? 1 : 0).CallObjectMethod(elementClassRef.Value,
			                                                    proxyEnv.VirtualMachine.ClassGetInterfacesMethodId,
			                                                    ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).GetArrayLength(arrayRef.ArrayValue);
			proxyEnv.Received(initial ? 1 : 0).GetObjectArrayElement(arrayRef, 0);
			proxyEnv.Received(1).GetStringUtfLength(strRef0);
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfLength(strRef1);
			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallObjectMethod(elementClassRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).CallObjectMethod(interfaceClassRef.Value,
			                                                    proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                                    ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).CallIntMethod(classRef.Value,
			                                                 proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                                 ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).CallIntMethod(elementClassRef.Value,
			                                                 proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                                 ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallIntMethod(interfaceClassRef.Value,
			                                   proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                   ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(classRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(elementClassRef.Value,
			                                       proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);

			pointers.ForEach(f => f.Dispose());
		}
	}
	private static JArrayObject CreateEnumArrayParameter(NativeInterfaceProxy proxyEnv,
		JNativeCallAdapter.Builder builder, Boolean initial)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JModifierObject.Modifiers arrayModifier = JModifierObject.Modifiers.Public | JModifierObject.Modifiers.Final |
			JModifierObject.Modifiers.Abstract;
		JModifierObject.Modifiers elementModifier = JModifierObject.Modifiers.Public | JModifierObject.Modifiers.Final;
		JObjectLocalRef localRef = JNativeCallAdapterTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef elementClassRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef elementSuperClassRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef0 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef strRef1 = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		CString arrayClassName = new(() => "[[[[Lrxmxnx/jnetinterface/test/EnumTest;"u8);
		CString elementName = arrayClassName[5..^1];
		TypeInfoSequence classInformation = MetadataHelper.GetClassInformation(arrayClassName, false);
		JClassTypeMetadata enumTypeMetadata = IClassType.GetMetadata<JEnumObject>();
		List<IFixedPointer.IDisposable> pointers =
		[
			classInformation.GetFixedPointer(),
			enumTypeMetadata.Information.GetFixedPointer(),
		];

		try
		{
			proxyEnv.GetObjectRefType(localRef).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetObjectClass(localRef).Returns(classRef);
			proxyEnv.FindClass(Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
			{
				ReadOnlyValPtr<Byte> ptr = (ReadOnlyValPtr<Byte>)c[0];
				ReadOnlySpan<Byte> className = ptr.GetByteSpan();
				return className.SequenceEqual(elementName) ? elementClassRef : default;
			});
			proxyEnv.GetSuperclass(elementClassRef).Returns(elementSuperClassRef);
			proxyEnv.GetStringUtfLength(strRef0).Returns(classInformation.ClassName.Length);
			proxyEnv.GetStringUtfLength(strRef1).Returns(enumTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef0.Value);
			proxyEnv.CallObjectMethod(elementSuperClassRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef1.Value);
			proxyEnv.GetStringUtfChars(strRef0, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[0].Pointer);
			proxyEnv.GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)pointers[1].Pointer);
			proxyEnv.CallIntMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                       ReadOnlyValPtr<JValueWrapper>.Zero).Returns((Int32)arrayModifier);
			proxyEnv.CallIntMethod(elementClassRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                       ReadOnlyValPtr<JValueWrapper>.Zero).Returns((Int32)elementModifier);
			proxyEnv.CallBooleanMethod(elementClassRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                           ReadOnlyValPtr<JValueWrapper>.Zero).Returns(false);
			proxyEnv.Received(0).CallBooleanMethod(elementSuperClassRef.Value,
			                                       proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);

			_ = builder.WithParameter(localRef, out JLocalObject result);
			JArrayObject jArray = Assert.IsAssignableFrom<JArrayObject>(result);
			Assert.Equal(JArrayObject<JArrayObject<JArrayObject<JArrayObject<JEnumObject>>>>.Metadata,
			             jArray.TypeMetadata);
			return jArray;
		}
		finally
		{
			proxyEnv.Received(1).GetObjectRefType(localRef);
			proxyEnv.Received(1).GetObjectClass(localRef);
			proxyEnv.Received(initial ? 1 : 0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(initial ? 1 : 0).GetSuperclass(elementClassRef);
			proxyEnv.Received(1).GetStringUtfLength(strRef0);
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfLength(strRef1);
			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallObjectMethod(elementClassRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).CallObjectMethod(elementSuperClassRef.Value,
			                                                    proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                                    ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).GetStringUtfChars(strRef1, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(initial ? 1 : 0).CallIntMethod(classRef.Value,
			                                                 proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                                 ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(initial ? 1 : 0).CallIntMethod(elementClassRef.Value,
			                                                 proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                                 ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallIntMethod(elementSuperClassRef.Value,
			                                   proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                                   ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(classRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).CallBooleanMethod(elementClassRef.Value,
			                                       proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);

			pointers.ForEach(f => f.Dispose());
		}
	}
}
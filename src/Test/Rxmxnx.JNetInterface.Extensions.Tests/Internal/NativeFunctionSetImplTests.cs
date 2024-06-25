namespace Rxmxnx.JNetInterface.Tests.Internal;

[ExcludeFromCodeCoverage]
public sealed class NativeFunctionSetImplTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void GetEnumNameTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JEnumObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JFunctionDefinition definition = JFunctionDefinition<JStringObject>.Create("name"u8);
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = NativeFunctionSetImplTests.fixture.Create<JClassLocalRef>();
		String enumName = NativeFunctionSetImplTests.fixture.Create<String>();
		using JClassObject jClass = new(env);
		using JClassObject jEnumClass = new(jClass, typeMetadata, classRef);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringEnumName = new(jStringClass, default, enumName);
		using JEnumObject jEnum = Assert.IsType<JEnumObject>(typeMetadata.CreateInstance(jEnumClass, localRef, true));

		env.ClassFeature.GetClass<JEnumObject>().Returns(jEnumClass);
		env.AccessFeature.CallFunction<JStringObject>(jEnum, jEnumClass, definition, false, [])
		   .Returns(jStringEnumName);

		Assert.Equal(jStringEnumName, NativeFunctionSetImpl.Instance.GetName(jEnum));

		env.ClassFeature.Received(1).GetClass<JEnumObject>();
		env.AccessFeature.Received(1).CallFunction<JStringObject>(jEnum, jEnumClass, definition, false, []);
	}
	[Fact]
	internal void GetEnumOrdinalTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JEnumObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JFunctionDefinition definition = JFunctionDefinition<JInt>.Create("ordinal"u8);
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = NativeFunctionSetImplTests.fixture.Create<JClassLocalRef>();
		Int32 ordinal = NativeFunctionSetImplTests.fixture.Create<Int32>();
		using JClassObject jClass = new(env);
		using JClassObject jEnumClass = new(jClass, typeMetadata, classRef);
		using JEnumObject jEnum = Assert.IsType<JEnumObject>(typeMetadata.CreateInstance(jEnumClass, localRef, true));

		env.ClassFeature.GetClass<JEnumObject>().Returns(jEnumClass);
		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(), Arg.Any<JEnumObject>(),
		                                                    Arg.Any<JClassObject>(), definition, false, [])).Do(c =>
		{
			Int32 ordinalValue = ordinal;
			ordinalValue.AsBytes().CopyTo((c[0] as IFixedMemory)!.Bytes);
		});

		Assert.Equal(ordinal, NativeFunctionSetImpl.Instance.GetOrdinal(jEnum));

		env.ClassFeature.Received(1).GetClass<JEnumObject>();
		env.AccessFeature.Received(1)
		   .CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jEnum, jEnumClass, definition, false, []);
	}

	[Fact]
	internal void GetStackTraceClassNameTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStackTraceElementObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		StackTraceInfo info = NativeFunctionSetImplTests.fixture.Create<StackTraceInfo>();
		JFunctionDefinition definition = JFunctionDefinition<JStringObject>.Create("getClassName"u8);
		using JClassObject jClass = new(env);
		using JClassObject jStackTraceElementClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringClassName = new(jStringClass, default, info.ClassName);
		using JStackTraceElementObject jStackTraceElement =
			Assert.IsType<JStackTraceElementObject>(
				typeMetadata.CreateInstance(jStackTraceElementClass, localRef, true));

		env.ClassFeature.GetClass<JStackTraceElementObject>().Returns(jStackTraceElementClass);
		env.AccessFeature.CallFunction<JStringObject>(jStackTraceElement, jStackTraceElementClass, definition, false,
		                                              []).Returns(jStringClassName);

		Assert.Equal(jStringClassName, NativeFunctionSetImpl.Instance.GetClassName(jStackTraceElement));

		env.ClassFeature.Received(1).GetClass<JStackTraceElementObject>();
		env.AccessFeature.Received(1)
		   .CallFunction<JStringObject>(jStackTraceElement, jStackTraceElementClass, definition, false, []);
	}
	[Fact]
	internal void GetNumberLineTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStackTraceElementObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		StackTraceInfo info = NativeFunctionSetImplTests.fixture.Create<StackTraceInfo>();
		JFunctionDefinition definition = JFunctionDefinition<JInt>.Create("getLineNumber"u8);
		using JClassObject jClass = new(env);
		using JClassObject jStackTraceElementClass = new(jClass, typeMetadata);
		using JStackTraceElementObject jStackTraceElement =
			Assert.IsType<JStackTraceElementObject>(
				typeMetadata.CreateInstance(jStackTraceElementClass, localRef, true));

		env.ClassFeature.GetClass<JStackTraceElementObject>().Returns(jStackTraceElementClass);
		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(),
		                                                    Arg.Any<JStackTraceElementObject>(),
		                                                    Arg.Any<JClassObject>(), definition, false, [])).Do(c =>
		{
			Int32 lineNumber = info.LineNumber;
			lineNumber.AsBytes().CopyTo((c[0] as IFixedMemory)!.Bytes);
		});

		Assert.Equal(info.LineNumber, NativeFunctionSetImpl.Instance.GetLineNumber(jStackTraceElement));

		env.ClassFeature.Received(1).GetClass<JStackTraceElementObject>();
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jStackTraceElement,
		                                                    jStackTraceElementClass, definition, false, []);
	}
	[Fact]
	internal void GetFileNameTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStackTraceElementObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		StackTraceInfo info = NativeFunctionSetImplTests.fixture.Create<StackTraceInfo>();
		JFunctionDefinition definition = JFunctionDefinition<JStringObject>.Create("getFileName"u8);
		using JClassObject jClass = new(env);
		using JClassObject jStackTraceElementClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringFileName = new(jStringClass, default, info.FileName);
		using JStackTraceElementObject jStackTraceElement =
			Assert.IsType<JStackTraceElementObject>(
				typeMetadata.CreateInstance(jStackTraceElementClass, localRef, true));

		env.ClassFeature.GetClass<JStackTraceElementObject>().Returns(jStackTraceElementClass);
		env.AccessFeature.CallFunction<JStringObject>(jStackTraceElement, jStackTraceElementClass, definition, false,
		                                              []).Returns(jStringFileName);

		Assert.Equal(jStringFileName, NativeFunctionSetImpl.Instance.GetFileName(jStackTraceElement));

		env.ClassFeature.Received(1).GetClass<JStackTraceElementObject>();
		env.AccessFeature.Received(1)
		   .CallFunction<JStringObject>(jStackTraceElement, jStackTraceElementClass, definition, false, []);
	}
	[Fact]
	internal void GetMethodNameTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStackTraceElementObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		StackTraceInfo info = NativeFunctionSetImplTests.fixture.Create<StackTraceInfo>();
		JFunctionDefinition definition = JFunctionDefinition<JStringObject>.Create("getMethodName"u8);
		using JClassObject jClass = new(env);
		using JClassObject jStackTraceElementClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringMethodName = new(jStringClass, default, info.MethodName);
		using JStackTraceElementObject jStackTraceElement =
			Assert.IsType<JStackTraceElementObject>(
				typeMetadata.CreateInstance(jStackTraceElementClass, localRef, true));

		env.ClassFeature.GetClass<JStackTraceElementObject>().Returns(jStackTraceElementClass);
		env.AccessFeature.CallFunction<JStringObject>(jStackTraceElement, jStackTraceElementClass, definition, false,
		                                              []).Returns(jStringMethodName);

		Assert.Equal(jStringMethodName, NativeFunctionSetImpl.Instance.GetMethodName(jStackTraceElement));

		env.ClassFeature.Received(1).GetClass<JStackTraceElementObject>();
		env.AccessFeature.Received(1)
		   .CallFunction<JStringObject>(jStackTraceElement, jStackTraceElementClass, definition, false, []);
	}
	[Fact]
	internal void IsNativeMethodTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStackTraceElementObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		StackTraceInfo info = NativeFunctionSetImplTests.fixture.Create<StackTraceInfo>();
		JFunctionDefinition definition = JFunctionDefinition<JBoolean>.Create("isNativeMethod"u8);
		using JClassObject jClass = new(env);
		using JClassObject jStackTraceElementClass = new(jClass, typeMetadata);
		using JStackTraceElementObject jStackTraceElement =
			Assert.IsType<JStackTraceElementObject>(
				typeMetadata.CreateInstance(jStackTraceElementClass, localRef, true));

		env.ClassFeature.GetClass<JStackTraceElementObject>().Returns(jStackTraceElementClass);
		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(),
		                                                    Arg.Any<JStackTraceElementObject>(),
		                                                    Arg.Any<JClassObject>(), definition, false, [])).Do(c =>
		{
			Boolean isNativeMethod = info.NativeMethod;
			isNativeMethod.AsBytes().CopyTo((c[0] as IFixedMemory)!.Bytes);
		});

		Assert.Equal(info.NativeMethod, NativeFunctionSetImpl.Instance.IsNativeMethod(jStackTraceElement));

		env.ClassFeature.Received(1).GetClass<JStackTraceElementObject>();
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jStackTraceElement,
		                                                    jStackTraceElementClass, definition, false, []);
	}

	[Fact]
	internal void GetPrimitiveValueTest()
	{
		NativeFunctionSetImplTests.GetPrimitiveNumberTest<JByte>("byteValue"u8);
		NativeFunctionSetImplTests.GetPrimitiveNumberTest<JDouble>("doubleValue"u8);
		NativeFunctionSetImplTests.GetPrimitiveNumberTest<JFloat>("floatValue"u8);
		NativeFunctionSetImplTests.GetPrimitiveNumberTest<JInt>("intValue"u8);
		NativeFunctionSetImplTests.GetPrimitiveNumberTest<JLong>("longValue"u8);
		NativeFunctionSetImplTests.GetPrimitiveNumberTest<JShort>("shortValue"u8);
	}

	[Fact]
	internal void GetMessageTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JThrowableLocalRef throwableRef = NativeFunctionSetImplTests.fixture.Create<JThrowableLocalRef>();
		String message = NativeFunctionSetImplTests.fixture.Create<String>();
		JFunctionDefinition definition = JFunctionDefinition<JStringObject>.Create("getMessage"u8);
		using JClassObject jClass = new(env);
		using JClassObject jThrowableClass = new(jClass, IClassType.GetMetadata<JThrowableObject>());
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringMessage = new(jStringClass, default, message);
		using JThrowableObject jThrowable = (JThrowableObject)IClassType.GetMetadata<JThrowableObject>()
		                                                                .CreateInstance(
			                                                                jThrowableClass, throwableRef.Value);

		env.ClassFeature.GetClass<JThrowableObject>().Returns(jThrowableClass);
		env.AccessFeature.CallFunction<JStringObject>(jThrowable, jThrowableClass, definition, false, [])
		   .Returns(jStringMessage);

		Assert.Equal(jStringMessage, NativeFunctionSetImpl.Instance.GetMessage(jThrowable));

		env.ClassFeature.Received(1).GetClass<JThrowableObject>();
		env.AccessFeature.Received(1).CallFunction<JStringObject>(jThrowable, jThrowableClass, definition, false, []);
	}
	[Fact]
	internal void GetStackTraceTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JThrowableLocalRef throwableRef = NativeFunctionSetImplTests.fixture.Create<JThrowableLocalRef>();
		JArrayLocalRef arrayRef = NativeFunctionSetImplTests.fixture.Create<JArrayLocalRef>();
		JFunctionDefinition definition =
			JFunctionDefinition<JArrayObject<JStackTraceElementObject>>.Create("getStackTrace"u8);
		using JClassObject jClass = new(env);
		using JClassObject jThrowableClass = new(jClass, IClassType.GetMetadata<JThrowableObject>());
		using JClassObject jStackTraceElementArrayClass =
			new(jClass, IClassType.GetMetadata<JStackTraceElementObject>().GetArrayMetadata()!);
		using JThrowableObject jThrowable = (JThrowableObject)IClassType.GetMetadata<JThrowableObject>()
		                                                                .CreateInstance(
			                                                                jThrowableClass, throwableRef.Value);
		using JArrayObject<JStackTraceElementObject> stackTraceElements =
			new(jStackTraceElementArrayClass, arrayRef, 0);

		env.ClassFeature.GetClass<JThrowableObject>().Returns(jThrowableClass);
		env.AccessFeature
		   .CallFunction<JArrayObject<JStackTraceElementObject>>(jThrowable, jThrowableClass, definition, false, [])
		   .Returns(stackTraceElements);

		Assert.Equal(stackTraceElements, NativeFunctionSetImpl.Instance.GetStackTrace(jThrowable));

		env.ClassFeature.Received(1).GetClass<JThrowableObject>();
		env.AccessFeature.Received(1)
		   .CallFunction<JArrayObject<JStackTraceElementObject>>(jThrowable, jThrowableClass, definition, false, []);
	}

	[Fact]
	internal void GetClassClassNameTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		ITypeInformation information = IDataType.GetMetadata<JClassObject>();
		JFunctionDefinition definition = JFunctionDefinition<JStringObject>.Create("getName"u8);
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringClassName = new(jStringClass, default, information.ClassName.ToString());

		env.ClassFeature.GetClass<JClassObject>().Returns(jClass);
		env.AccessFeature.CallFunction<JStringObject>(jClass, jClass, definition, false, []).Returns(jStringClassName);

		Assert.Equal(jStringClassName, NativeFunctionSetImpl.Instance.GetClassName(jClass));

		env.ClassFeature.Received(1).GetClass<JClassObject>();
		env.AccessFeature.Received(1).CallFunction<JStringObject>(jClass, jClass, definition, false, []);
	}
	[Fact]
	internal void IsPrimitiveClassTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JFunctionDefinition definition = JFunctionDefinition<JBoolean>.Create("isPrimitive"u8);
		using JClassObject jClass = new(env);
		Boolean result = NativeFunctionSetImplTests.fixture.Create<Boolean>();

		env.ClassFeature.GetClass<JClassObject>().Returns(jClass);
		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(), Arg.Any<JClassObject>(),
		                                                    Arg.Any<JClassObject>(), definition, false, [])).Do(c =>
		{
			JBoolean primitive = result;
			primitive.AsBytes().CopyTo((c[0] as IFixedMemory)!.Bytes);
		});

		Assert.Equal(result, NativeFunctionSetImpl.Instance.IsPrimitiveClass(jClass));

		env.ClassFeature.Received(1).GetClass<JClassObject>();
		env.AccessFeature.Received(1)
		   .CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jClass, jClass, definition, false, []);
	}
	[Fact]
	internal void IsFinalClassTest()
	{
		NativeFunctionSetImplTests.FinalClassTest<JBoolean>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JBoolean>>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JArrayObject<JBoolean>>>();

		NativeFunctionSetImplTests.FinalClassTest<JStringObject>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JStringObject>>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JArrayObject<JStringObject>>>();

		NativeFunctionSetImplTests.FinalClassTest<JSerializableObject>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JSerializableObject>>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JArrayObject<JSerializableObject>>>();

		NativeFunctionSetImplTests.FinalClassTest<JThrowableObject>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JThrowableObject>>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JArrayObject<JThrowableObject>>>();

		NativeFunctionSetImplTests.FinalClassTest<JBufferObject>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JBufferObject>>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JArrayObject<JBufferObject>>>();

		NativeFunctionSetImplTests.FinalClassTest<JElementTypeObject>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JElementTypeObject>>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JArrayObject<JElementTypeObject>>>();

		NativeFunctionSetImplTests.FinalClassTest<JTargetObject>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JTargetObject>>();
		NativeFunctionSetImplTests.FinalClassTest<JArrayObject<JArrayObject<JTargetObject>>>();
	}
	[Fact]
	internal void GetInterfacesTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JFunctionDefinition definition = JFunctionDefinition<JArrayObject<JClassObject>>.Create("getInterfaces"u8);
		JArrayLocalRef arrayRef = NativeFunctionSetImplTests.fixture.Create<JArrayLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jClassArrayClass = new(jClass, IClassType.GetMetadata<JClassObject>().GetArrayMetadata()!);
		using JArrayObject<JClassObject> interfaces = new(jClassArrayClass, arrayRef, default);

		env.ClassFeature.GetClass<JClassObject>().Returns(jClass);
		env.AccessFeature.CallFunction<JArrayObject<JClassObject>>(jClass, jClass, definition, false, [])
		   .Returns(interfaces);

		Assert.Equal(interfaces, NativeFunctionSetImpl.Instance.GetInterfaces(jClass));

		env.ClassFeature.Received(1).GetClass<JClassObject>();
		env.AccessFeature.Received(1).CallFunction<JArrayObject<JClassObject>>(jClass, jClass, definition, false, []);
	}

	[Theory]
	[InlineData]
	[InlineData(true)]
	[InlineData(false)]
	internal void IsDirectBufferTest(Boolean? directBuffer = default)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JFunctionDefinition definition = JFunctionDefinition<JBoolean>.Create("isDirect"u8);
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jBufferClass = new(jClass, IDataType.GetMetadata<JBufferObject>());
		using JClassObject jDirectByteBufferClass = new(jClass, IDataType.GetMetadata<JDirectByteBufferObject>());
		using IFixedMemory.IDisposable mem = NativeFunctionSetImplTests.fixture.CreateMany<Byte>(10).ToArray()
		                                                               .AsMemory().GetFixedContext();
		using JBufferObject jBuffer = directBuffer.HasValue ?
			new JBufferObject(jClass, localRef) :
			new JDirectByteBufferObject(jDirectByteBufferClass, mem, localRef);

		env.ClassFeature.GetClass<JBufferObject>().Returns(jBufferClass);
		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(), Arg.Any<JBufferObject>(),
		                                                    Arg.Any<JClassObject>(), definition, false, [])).Do(c =>
		{
			Boolean isDirect = directBuffer ?? true;
			isDirect.AsBytes().CopyTo((c[0] as IFixedMemory)!.Bytes);
		});

		Assert.Equal(directBuffer ?? true, NativeFunctionSetImpl.Instance.IsDirectBuffer(jBuffer));

		env.ClassFeature.Received(1).GetClass<JBufferObject>();
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jBuffer,
		                                                    jBufferClass, definition, false, []);
	}
	[Theory]
	[InlineData]
	[InlineData(true)]
	[InlineData(false)]
	internal void BufferCapacityTest(Boolean? directBuffer = default)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JFunctionDefinition definition = JFunctionDefinition<JLong>.Create("capacity"u8);
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		JLong capacity = NativeFunctionSetImplTests.fixture.Create<Int64>();
		using JClassObject jClass = new(env);
		using JClassObject jBufferClass = new(jClass, IDataType.GetMetadata<JBufferObject>());
		using JClassObject jDirectByteBufferClass = new(jClass, IDataType.GetMetadata<JDirectByteBufferObject>());
		using IFixedMemory.IDisposable mem = NativeFunctionSetImplTests.fixture.CreateMany<Byte>(10).ToArray()
		                                                               .AsMemory().GetFixedContext();
		using JBufferObject jBuffer = directBuffer.HasValue ?
			new JBufferObject(jClass, localRef) :
			new JDirectByteBufferObject(jDirectByteBufferClass, mem, localRef);

		env.ClassFeature.GetClass<JBufferObject>().Returns(jBufferClass);
		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(), Arg.Any<JBufferObject>(),
		                                                    Arg.Any<JClassObject>(), definition, false, [])).Do(c =>
		{
			Int64 localCapacity = capacity.Value;
			localCapacity.AsBytes().CopyTo((c[0] as IFixedMemory)!.Bytes);
		});

		Assert.Equal(capacity, NativeFunctionSetImpl.Instance.BufferCapacity(jBuffer));

		env.ClassFeature.Received(1).GetClass<JBufferObject>();
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jBuffer,
		                                                    jBufferClass, definition, false, []);
	}

	[Fact]
	internal void GetMemberNameTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JExecutableObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		String message = NativeFunctionSetImplTests.fixture.Create<String>();
		JFunctionDefinition definition = JFunctionDefinition<JStringObject>.Create("getName"u8);
		using JClassObject jClass = new(env);
		using JClassObject jMemberClass = new(jClass, IDataType.GetMetadata<JMemberObject>());
		using JClassObject jExecutableClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringName = new(jStringClass, default, message);
		using JExecutableObject jExecutable = (JExecutableObject)typeMetadata.CreateInstance(jMemberClass, localRef);

		env.ClassFeature.GetClass<JMemberObject>().Returns(jMemberClass);
		env.AccessFeature.CallFunction<JStringObject>(jExecutable, jMemberClass, definition, false, [])
		   .Returns(jStringName);

		Assert.Equal(jStringName, NativeFunctionSetImpl.Instance.GetName(jExecutable));

		env.ClassFeature.Received(1).GetClass<JMemberObject>();
		env.AccessFeature.Received(1).CallFunction<JStringObject>(jExecutable, jMemberClass, definition, false, []);
	}
	[Fact]
	internal void GetDeclaringClassTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JFieldObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		JFunctionDefinition definition = JFunctionDefinition<JClassObject>.Create("getDeclaringClass"u8);
		using JClassObject jClass = new(env);
		using JClassObject jMemberClass = new(jClass, IDataType.GetMetadata<JMemberObject>());
		using JClassObject jFieldClass = new(jClass, typeMetadata);
		using JFieldObject jField = (JFieldObject)typeMetadata.CreateInstance(jMemberClass, localRef, true);

		env.ClassFeature.GetClass<JMemberObject>().Returns(jMemberClass);
		env.AccessFeature.CallFunction<JClassObject>(jField, jMemberClass, definition, false, []).Returns(jClass);

		Assert.Equal(jClass, NativeFunctionSetImpl.Instance.GetDeclaringClass(jField));

		env.ClassFeature.Received(1).GetClass<JMemberObject>();
		env.AccessFeature.Received(1).CallFunction<JClassObject>(jField, jMemberClass, definition, false, []);
	}
	[Fact]
	internal void GetParameterTypesTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JConstructorObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		JArrayLocalRef arrayRef = NativeFunctionSetImplTests.fixture.Create<JArrayLocalRef>();
		JFunctionDefinition definition = JFunctionDefinition<JArrayObject<JClassObject>>.Create("getParameterTypes"u8);
		using JClassObject jClass = new(env);
		using JClassObject jExecutableClass = new(jClass, IClassType.GetMetadata<JExecutableObject>());
		using JClassObject jConstructorClass = new(jClass, typeMetadata);
		using JClassObject jClassArrayClass = new(jClass, IDataType.GetMetadata<JArrayObject<JClassObject>>());
		using JConstructorObject jConstructor =
			(JConstructorObject)typeMetadata.CreateInstance(jConstructorClass, localRef);
		using JArrayObject<JClassObject> parametersTypes = new(jClassArrayClass, arrayRef, 0);

		env.ClassFeature.GetClass<JExecutableObject>().Returns(jExecutableClass);
		env.AccessFeature
		   .CallFunction<JArrayObject<JClassObject>>(jConstructor, jExecutableClass, definition, false, [])
		   .Returns(parametersTypes);

		Assert.Equal(parametersTypes, NativeFunctionSetImpl.Instance.GetParameterTypes(jConstructor));

		env.ClassFeature.Received(1).GetClass<JExecutableObject>();
		env.AccessFeature.Received(1)
		   .CallFunction<JArrayObject<JClassObject>>(jConstructor, jExecutableClass, definition, false, []);
	}
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	[InlineData(4)]
	internal void GetReturnTypeTest(Byte nCase)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JExecutableObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		JFunctionDefinition definition = JFunctionDefinition<JClassObject>.Create("getReturnType"u8);
		using JClassObject jClass = new(env);
		using JClassObject jExecutableClass = new(jClass, typeMetadata);
		using JClassObject jConstructorClass = new(jClass, IClassType.GetMetadata<JConstructorObject>());
		using JClassObject jMethodClass = new(jClass, IClassType.GetMetadata<JMethodObject>());
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JClassObject jVoidClass = new(jClass, JPrimitiveTypeMetadata.VoidMetadata);
		using JExecutableObject jExecutable = (JExecutableObject)(nCase switch
		{
			0 => typeMetadata.CreateInstance(jExecutableClass, localRef),
			1 => typeMetadata.CreateInstance(jExecutableClass, localRef),
			2 => IClassType.GetMetadata<JConstructorObject>().CreateInstance(jMethodClass, localRef),
			_ => IClassType.GetMetadata<JMethodObject>().CreateInstance(jMethodClass, localRef),
		});

		env.ClassFeature.IsInstanceOf<JMethodObject>(jExecutable).Returns(nCase != 1 && nCase != 2);
		env.ClassFeature.GetClass<JExecutableObject>().Returns(jExecutableClass);
		env.AccessFeature.CallFunction<JClassObject>(jExecutable, jExecutableClass, definition, false, [])
		   .Returns(nCase == 3 ? jStringClass : jVoidClass);

		Assert.Equal(nCase == 3 ? jStringClass :
		             nCase != 1 && nCase != 2 ? jVoidClass : default,
		             NativeFunctionSetImpl.Instance.GetReturnType(jExecutable));

		env.ClassFeature.Received(nCase < 2 ? 1 : 0).IsInstanceOf<JMethodObject>(jExecutable);
		env.ClassFeature.Received(nCase != 1 && nCase != 2 ? 1 : 0).GetClass<JExecutableObject>();
		env.AccessFeature.Received(nCase != 1 && nCase != 2 ? 1 : 0)
		   .CallFunction<JClassObject>(jExecutable, jExecutableClass, definition, false, []);
	}
	[Fact]
	internal void GetFieldTypeTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JFieldObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		JFunctionDefinition definition = JFunctionDefinition<JClassObject>.Create("getType"u8);
		using JClassObject jClass = new(env);
		using JClassObject jIntClass = new(jClass, IDataType.GetMetadata<JInt>());
		using JClassObject jFieldClass = new(jClass, typeMetadata);
		using JFieldObject jField = (JFieldObject)typeMetadata.CreateInstance(jFieldClass, localRef);

		env.ClassFeature.GetClass<JFieldObject>().Returns(jFieldClass);
		env.AccessFeature.CallFunction<JClassObject>(jField, jFieldClass, definition, false, []).Returns(jIntClass);

		Assert.Equal(jIntClass, NativeFunctionSetImpl.Instance.GetFieldType(jField));

		env.ClassFeature.Received(1).GetClass<JFieldObject>();
		env.AccessFeature.Received(1).CallFunction<JClassObject>(jField, jFieldClass, definition, false, []);
	}

	private static void GetPrimitiveNumberTest<TPrimitive>(ReadOnlySpan<Byte> functionName)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IPrimitiveNumericType<TPrimitive>,
		IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JNumberObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NativeFunctionSetImplTests.fixture.Create<JObjectLocalRef>();
		TPrimitive value = (TPrimitive)NativeFunctionSetImplTests.fixture.Create<Double>();
		JFunctionDefinition<TPrimitive>.Parameterless definition = new(functionName);
		using JClassObject jClass = new(env);
		using JClassObject jNumberClass = new(jClass, typeMetadata);
		using JNumberObject jNumber =
			Assert.IsType<JNumberObject>(typeMetadata.CreateInstance(jNumberClass, localRef, true));

		env.ClassFeature.GetClass<JNumberObject>().Returns(jNumberClass);
		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(), Arg.Any<JNumberObject>(),
		                                                    Arg.Any<JClassObject>(), definition, false, [])).Do(c =>
		{
			TPrimitive primitive = value;
			primitive.AsBytes().CopyTo((c[0] as IFixedMemory)!.Bytes);
		});

		Assert.Equal(value, NativeFunctionSetImpl.Instance.GetPrimitiveValue<TPrimitive>(jNumber));

		env.ClassFeature.Received(1).GetClass<JNumberObject>();
		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jNumber,
		                                                    jNumberClass, definition, false, []);
	}
	private static void FinalClassTest<TDataType>() where TDataType : IDataType<TDataType>
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JFunctionDefinition definition = JFunctionDefinition<JInt>.Create("getModifiers"u8);
		JClassLocalRef classRef0 = NativeFunctionSetImplTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef classRef1 = NativeFunctionSetImplTests.fixture.Create<JClassLocalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, IDataType.GetMetadata<TDataType>(), classRef0);
		using JClassObject? jClassElement = jClass.IsArray ?
			new(jClassClass, NativeFunctionSetImplTests.GetElementMetadata<TDataType>(), classRef1) :
			default;

		env.ClassFeature.GetClass<JClassObject>().Returns(jClassClass);
		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(), Arg.Any<JClassObject>(),
		                                                    Arg.Any<JClassObject>(), definition, false, [])).Do(c =>
		{
			JModifierObject.Modifiers modifiers = NativeFunctionSetImplTests.GetModifiers((c[1] as JClassObject)!);
			modifiers.AsBytes().CopyTo((c[0] as IFixedMemory)!.Bytes);
		});
		if (jClassElement is not null) env.ClassFeature.GetClass(jClassElement.Name).Returns(jClassElement);

		env.WithFrame(Arg.Any<Int32>(), Arg.Any<JClassObject>(), Arg.Any<Func<JClassObject, Boolean>>())
		   .Returns(c => (c[2] as Func<JClassObject, Boolean>)!((c[1] as JClassObject)!));

		Assert.Equal(jClass.IsFinal, NativeFunctionSetImpl.Instance.IsFinal(jClass, out _));
		Boolean nonPrimitive = !(jClassElement ?? jClass).IsPrimitive;
		Int32 count = nonPrimitive ? 1 : 0;

		if (!nonPrimitive)
			env.ClassFeature.Received(0).GetClass<JClassObject>();
		else
			env.ClassFeature.Received().GetClass<JClassObject>();
		env.AccessFeature.Received(count)
		   .CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jClass, jClassClass, definition, false, []);
		if (jClassElement is null) return;
		env.ClassFeature.Received(count).GetClass(jClassElement.Name);
		env.AccessFeature.Received(count)
		   .CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jClassElement, jClassClass, definition, false, []);
	}
	private static JDataTypeMetadata GetElementMetadata<TDataType>() where TDataType : IDataType<TDataType>
	{
		JDataTypeMetadata result = IDataType.GetMetadata<TDataType>();
		while ((result as JArrayTypeMetadata)?.ElementMetadata is { } elementMetadata)
			result = elementMetadata;
		return result;
	}
	private static JModifierObject.Modifiers GetModifiers(JClassObject jClass)
	{
		JModifierObject.Modifiers modifiers = jClass.IsPrimitive ? JModifierObject.PrimitiveModifiers : default;
		if (jClass.IsPrimitive) return modifiers;
		if (jClass.IsFinal)
			modifiers |= JModifierObject.Modifiers.Final;
		if (jClass.IsArray || jClass.IsInterface)
			modifiers |= JModifierObject.Modifiers.Abstract;
		if (jClass.IsInterface)
			modifiers |= JModifierObject.Modifiers.Interface;
		if (jClass.IsAnnotation)
			modifiers |= JModifierObject.Modifiers.Annotation;
		if (jClass.IsEnum)
			modifiers |= JModifierObject.Modifiers.Enum;
		return modifiers;
	}
}
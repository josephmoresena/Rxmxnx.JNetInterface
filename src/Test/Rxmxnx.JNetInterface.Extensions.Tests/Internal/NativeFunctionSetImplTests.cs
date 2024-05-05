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
		Int32 ordinal = NativeFunctionSetImplTests.fixture.Create<Int32>();
		using JClassObject jClass = new(env);
		using JClassObject jEnumClass = new(jClass, typeMetadata, classRef);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringEnumName = new(jStringClass, default, enumName);
		using JEnumObject jEnum = Assert.IsType<JEnumObject>(typeMetadata.CreateInstance(jEnumClass, localRef, true));

		ILocalObject.ProcessMetadata(
			jEnum, new EnumObjectMetadata(new(jEnumClass)) { Ordinal = ordinal, Name = enumName, });
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
		String enumName = NativeFunctionSetImplTests.fixture.Create<String>();
		Int32 ordinal = NativeFunctionSetImplTests.fixture.Create<Int32>();
		using JClassObject jClass = new(env);
		using JClassObject jEnumClass = new(jClass, typeMetadata, classRef);
		using JEnumObject jEnum = Assert.IsType<JEnumObject>(typeMetadata.CreateInstance(jEnumClass, localRef, true));

		ILocalObject.ProcessMetadata(
			jEnum, new EnumObjectMetadata(new(jEnumClass)) { Ordinal = ordinal, Name = enumName, });
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
	internal void GetClassNameTest()
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

		env.AccessFeature.CallFunction<JStringObject>(jStackTraceElement, jStackTraceElementClass, definition, false,
		                                              []).Returns(jStringClassName);

		Assert.Equal(jStringClassName, NativeFunctionSetImpl.Instance.GetClassName(jStackTraceElement));

		env.AccessFeature.Received(1)
		   .CallFunction<JStringObject>(jStackTraceElement, jStackTraceElementClass, definition, false, []);
	}
	[Fact]
	internal void GetNumberTest()
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

		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(),
		                                                    Arg.Any<JStackTraceElementObject>(),
		                                                    Arg.Any<JClassObject>(), definition, false, [])).Do(c =>
		{
			Int32 lineNumber = info.LineNumber;
			lineNumber.AsBytes().CopyTo((c[0] as IFixedMemory)!.Bytes);
		});

		Assert.Equal(info.LineNumber, NativeFunctionSetImpl.Instance.GetLineNumber(jStackTraceElement));

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

		env.AccessFeature.CallFunction<JStringObject>(jStackTraceElement, jStackTraceElementClass, definition, false,
		                                              []).Returns(jStringFileName);

		Assert.Equal(jStringFileName, NativeFunctionSetImpl.Instance.GetFileName(jStackTraceElement));

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

		env.AccessFeature.CallFunction<JStringObject>(jStackTraceElement, jStackTraceElementClass, definition, false,
		                                              []).Returns(jStringMethodName);

		Assert.Equal(jStringMethodName, NativeFunctionSetImpl.Instance.GetMethodName(jStackTraceElement));

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

		env.AccessFeature.When(a => a.CallPrimitiveFunction(Arg.Any<IFixedMemory>(),
		                                                    Arg.Any<JStackTraceElementObject>(),
		                                                    Arg.Any<JClassObject>(), definition, false, [])).Do(c =>
		{
			Boolean isNativeMethod = info.NativeMethod;
			isNativeMethod.AsBytes().CopyTo((c[0] as IFixedMemory)!.Bytes);
		});

		Assert.Equal(info.NativeMethod, NativeFunctionSetImpl.Instance.IsNativeMethod(jStackTraceElement));

		env.AccessFeature.Received(1).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), jStackTraceElement,
		                                                    jStackTraceElementClass, definition, false, []);
	}
}
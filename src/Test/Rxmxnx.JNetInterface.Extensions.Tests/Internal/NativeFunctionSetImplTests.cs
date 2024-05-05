namespace Rxmxnx.JNetInterface.Tests.Internal;

[ExcludeFromCodeCoverage]
public sealed class NativeFunctionSetImplTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void GetEnumName()
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
	internal void GetEnumOrdinal()
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
}
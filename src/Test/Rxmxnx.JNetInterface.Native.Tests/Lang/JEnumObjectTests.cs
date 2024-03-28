namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public class JEnumObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.EnumObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JEnumObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JEnumObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JEnumObjectTests.className, JEnumObjectTests.classSignature,
	                                                   JEnumObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void CreateMetadataTest(Boolean initEnum)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JEnumObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JEnumObjectTests.fixture.Create<JObjectLocalRef>();
		String enumName = JEnumObjectTests.fixture.Create<String>();
		Int32 ordinal = JEnumObjectTests.fixture.Create<Int32>();
		using JClassObject jClass = new(env);
		using JClassObject jEnumClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringEnumName = new(jStringClass, default, enumName);
		using JEnumObject jEnum = Assert.IsType<JEnumObject>(typeMetadata.CreateInstance(jEnumClass, localRef, true));
		EnumObjectMetadata enumMetadata = new(new(jEnumClass))
		{
			Ordinal = initEnum ? ordinal : null, Name = initEnum ? enumName : default,
		};

		env.FunctionSet.GetName(jEnum).Returns(jStringEnumName);
		env.FunctionSet.GetOrdinal(jEnum).Returns(ordinal);

		ILocalObject.ProcessMetadata(jEnum, enumMetadata);

		EnumObjectMetadata objectMetadata = Assert.IsType<EnumObjectMetadata>(ILocalObject.CreateMetadata(jEnum));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(ordinal, objectMetadata.Ordinal);
		Assert.Equal(enumName, objectMetadata.Name);
		Assert.Equal(objectMetadata, new(objectMetadata));

		JSerializableObject jSerializable = jEnum.CastTo<JSerializableObject>();
		Assert.Equal(jEnum.Id, jSerializable.Id);
		Assert.Equal(jEnum, jSerializable.Object);

		Assert.True(Object.ReferenceEquals(jEnum, jEnum.CastTo<JLocalObject>()));

		env.FunctionSet.Received(initEnum ? 0 : 1).GetName(jEnum);
		env.FunctionSet.Received(initEnum ? 0 : 1).GetOrdinal(jEnum);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JEnumObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JEnumObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JEnumObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JEnumObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jEnumClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jEnumClass);
		using JGlobal jGlobal = new(vm, new(jEnumClass, IClassType.GetMetadata<JEnumObject>()), !env.NoProxy,
		                            globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Abstract, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JEnumObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JEnumObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JEnumObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JEnumObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JEnumObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JEnumObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JEnumObjectTests.hash.ToString(), IDataType.GetHash<JEnumObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JEnumObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, Array.Empty<JArgumentMetadata>()));
		Assert.IsType<JFieldDefinition<JEnumObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JEnumObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JEnumObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JEnumObject>().Returns(jEnumClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jEnumClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jEnumClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JEnumObject jEnum0 = Assert.IsType<JEnumObject>(typeMetadata.CreateInstance(jEnumClass, localRef, true));
		using JEnumObject jEnum1 = Assert.IsType<JEnumObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JEnumObject jEnum2 = Assert.IsType<JEnumObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(1).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JEnumObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}
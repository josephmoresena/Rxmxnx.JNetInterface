namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public class JNumberObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.NumberObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JNumberObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JNumberObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JNumberObjectTests.className, JNumberObjectTests.classSignature,
	                                                   JNumberObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JNumberObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JNumberObjectTests.fixture.Create<JObjectLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jNumberClass = new(jClass, typeMetadata);
		using JNumberObject jNumber =
			Assert.IsType<JNumberObject>(typeMetadata.CreateInstance(jNumberClass, localRef, true));

		ObjectMetadata objectMetadata = ILocalObject.CreateMetadata(jNumber);

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);

		JSerializableObject jSerializable = jNumber.CastTo<JSerializableObject>();
		Assert.Equal(jNumber.Id, jSerializable.Id);
		Assert.Equal(jNumber, jSerializable.Object);

		Assert.True(Object.ReferenceEquals(jNumber, jNumber.CastTo<JLocalObject>()));
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JNumberObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JNumberObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JNumberObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JNumberObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jNumberClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jNumberClass);
		using JGlobal jGlobal = new(vm, new(jNumberClass, IClassType.GetMetadata<JNumberObject>()), globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Abstract, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JNumberObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JNumberObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JNumberObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JNumberObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JNumberObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JNumberObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JNumberObjectTests.hash.ToString(), IDataType.GetHash<JNumberObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JNumberObject>>(typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JNumberObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JNumberObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JNumberObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JNumberObject>().Returns(jNumberClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jNumberClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jNumberClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JNumberObject jNumber0 =
			Assert.IsType<JNumberObject>(typeMetadata.CreateInstance(jNumberClass, localRef, true));
		using JNumberObject jNumber1 = Assert.IsType<JNumberObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JNumberObject jNumber2 = Assert.IsType<JNumberObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JNumberObject>(Arg.Any<JReferenceObject>());

		Assert.True(typeMetadata.IsInstance(jNumber0));
		Assert.True(typeMetadata.IsInstance(jNumber1));
		Assert.True(typeMetadata.IsInstance(jNumber2));
	}
}
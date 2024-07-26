namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public class JModuleObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new("java/lang/Module"u8);
	private static readonly CString classSignature = CString.Concat("L"u8, JModuleObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JModuleObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JModuleObjectTests.className, JModuleObjectTests.classSignature,
	                                                   JModuleObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JModuleObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JModuleObjectTests.fixture.Create<JObjectLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jModuleClass = new(jClass, typeMetadata);
		using JModuleObject jModule =
			Assert.IsType<JModuleObject>(typeMetadata.CreateInstance(jModuleClass, localRef, true));

		ObjectMetadata objectMetadata = ILocalObject.CreateMetadata(jModule);

		Assert.True(Object.ReferenceEquals(jModule, jModule.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jModule, (jModule as ILocalObject).CastTo<JLocalObject>()));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);

		JAnnotatedElementObject jAnnotatedElement = jModule.CastTo<JAnnotatedElementObject>();
		Assert.Equal(jModule.Id, jAnnotatedElement.Id);
		Assert.Equal(jModule, jAnnotatedElement.Object);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JModuleObject>();
		String? textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JModuleObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JModuleObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JModuleObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jModuleClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jModuleClass);
		using JGlobal jGlobal = new(vm, new(jModuleClass), globalRef);

		Assert.StartsWith("{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.ToPrintableHash()} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JModuleObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JModuleObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JModuleObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JModuleObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JModuleObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JModuleObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JModuleObjectTests.hash.ToString(), IDataType.GetHash<JModuleObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JModuleObject>.Parameterless>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFunctionDefinition<JModuleObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, [JArgumentMetadata.Get<JStringObject>(),]));
		Assert.IsType<JFieldDefinition<JModuleObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JModuleObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JModuleObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JModuleObject>().Returns(jModuleClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jModuleClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jModuleClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JModuleObject jModule0 =
			Assert.IsType<JModuleObject>(typeMetadata.CreateInstance(jModuleClass, localRef, true));
		using JModuleObject jModule1 = Assert.IsType<JModuleObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JModuleObject jModule2 = Assert.IsType<JModuleObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JModuleObject>(Arg.Any<JReferenceObject>());

		Assert.True(typeMetadata.IsInstance(jModule0));
		Assert.True(typeMetadata.IsInstance(jModule1));
		Assert.True(typeMetadata.IsInstance(jModule2));
	}
}
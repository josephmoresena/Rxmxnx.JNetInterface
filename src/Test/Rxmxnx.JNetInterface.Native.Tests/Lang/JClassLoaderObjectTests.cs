namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public class JClassLoaderObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new("java/lang/ClassLoader"u8);
	private static readonly CString classSignature = CString.Concat("L"u8, JClassLoaderObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JClassLoaderObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JClassLoaderObjectTests.className,
	                                                   JClassLoaderObjectTests.classSignature,
	                                                   JClassLoaderObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JClassLoaderObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JClassLoaderObjectTests.fixture.Create<JObjectLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jClassLoaderClass = new(jClass, typeMetadata);
		using JClassLoaderObject jClassLoader =
			Assert.IsType<JClassLoaderObject>(typeMetadata.CreateInstance(jClassLoaderClass, localRef, true));

		ObjectMetadata objectMetadata = ILocalObject.CreateMetadata(jClassLoader);

		Assert.True(Object.ReferenceEquals(jClassLoader, jClassLoader.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jClassLoader, (jClassLoader as ILocalObject).CastTo<JLocalObject>()));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JClassLoaderObject>();
		String? textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JClassLoaderObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JClassLoaderObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JClassLoaderObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jClassLoaderClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jClassLoaderClass);
		using JGlobal jGlobal = new(vm, new(jClassLoaderClass), globalRef);

		Assert.StartsWith("{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.ToPrintableHash()} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JClassLoaderObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JClassLoaderObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JClassLoaderObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JClassLoaderObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JClassLoaderObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JClassLoaderObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JClassLoaderObjectTests.hash.ToString(), IDataType.GetHash<JClassLoaderObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JClassLoaderObject>.Parameterless>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFunctionDefinition<JClassLoaderObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, [JArgumentMetadata.Get<JStringObject>(),]));
		Assert.IsType<JFieldDefinition<JClassLoaderObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JClassLoaderObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JClassLoaderObject>());
		Assert.Empty(typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JClassLoaderObject>().Returns(jClassLoaderClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jClassLoaderClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jClassLoaderClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JClassLoaderObject jClassLoader0 =
			Assert.IsType<JClassLoaderObject>(typeMetadata.CreateInstance(jClassLoaderClass, localRef, true));
		using JClassLoaderObject jClassLoader1 =
			Assert.IsType<JClassLoaderObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JClassLoaderObject jClassLoader2 =
			Assert.IsType<JClassLoaderObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JClassLoaderObject>(Arg.Any<JReferenceObject>());

		Assert.True(typeMetadata.IsInstance(jClassLoader0));
		Assert.True(typeMetadata.IsInstance(jClassLoader1));
		Assert.True(typeMetadata.IsInstance(jClassLoader2));
	}
}